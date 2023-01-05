--PROCEDURES

CREATE OR ALTER PROCEDURE Vendors.AddVendor(@vendorName AS VARCHAR(25), @isMpps AS BIT)
AS
BEGIN

IF NOT EXISTS (SELECT VendorName FROM Vendors.Vendor WHERE VendorName = @vendorName)

INSERT INTO Vendors.Vendor(VendorName,IsMPPS)
VALUES (@vendorName, @isMpps);

END
GO

CREATE OR ALTER PROCEDURE Materials.MaterialInsert
    (@materialNumber AS INT,
    @materialName AS VARCHAR(50),
    @nameAbreviation AS VARCHAR(10),
    @permitNumber AS VARCHAR(25),
    @rawMaterialCode AS VARCHAR(3),
    @productCode AS VARCHAR(3),
    @carbonDrumRequired AS BIT,
    @carbonDrumDaysAllowed AS INT = NULL,
    @carbonDrumWeightAllowed AS INT = NULL,
    @batchManaged AS BIT,
    @requiresProcessOrder AS BIT,
    @unitOfIssue AS CHAR(2),
    @isRawMaterial AS BIT,
    @vendorName AS VARCHAR(25),
    @sequenceNumber AS INT)
AS
BEGIN TRAN MaterialInsert
BEGIN TRY 
INSERT INTO Materials.Material
    (MaterialNumber,MaterialName, MaterialNameAbreviation, PermitNumber, RawMaterialCode, ProductCode, CarbonDrumRequired, CarbonDrumDaysAllowed, CarbonDrumWeightAllowed)
VALUES(@materialNumber,@materialName, @nameAbreviation, @permitNumber, @rawMaterialCode, @productCode, @carbonDrumRequired, @carbonDrumDaysAllowed, @carbonDrumWeightAllowed);

DECLARE @parentMaterialNumber AS INT
SET @parentMaterialNumber = (SELECT MaterialNumber
FROM Materials.Material
WHERE MaterialName = @materialName);

INSERT INTO Materials.MaterialNumber
    (MaterialNumber, ParentMaterialNumber,  BatchManaged, RequiresProcessOrder, UnitOfIssue, IsRawMaterial)
VALUES(@materialNumber, @parentMaterialNumber,  @batchManaged, @requiresProcessOrder, @unitOfIssue, @isRawMaterial);

IF NOT EXISTS(SELECT VendorName
FROM Vendors.Vendor
WHERE VendorName = @vendorName)

BEGIN
    INSERT INTO Vendors.Vendor
        (VendorName)
    VALUES(@vendorName);
END

DECLARE @sequenceId AS INT
SET @sequenceId =(SELECT sequenceId
FROM Distillation.ProductNumberSequence
WHERE sequenceIdStart = @sequenceNumber);

DECLARE @currentSequenceId AS INT
SET @currentSequenceId =(SELECT sequenceIdStart
FROM Distillation.ProductNumberSequence
WHERE sequenceId = @sequenceId);

INSERT INTO Materials.MaterialId
    (MaterialNumber, VendorName, SequenceId, CurrentSequenceId)
VALUES(@materialNumber, @vendorName, @sequenceId, @currentSequenceId);
COMMIT;

END TRY
BEGIN CATCH
    THROW;
    ROLLBACK;
END CATCH
GO

CREATE OR ALTER PROCEDURE Vendors.AddVendorBatch(@materialNumber INT,
@vendorName AS VARCHAR(25), @batchNumber AS VARCHAR(50), @qty INT = 1)
AS
BEGIN TRAN VendorBatch
BEGIN TRY

INSERT INTO Vendors.VendorBatch(VendorName,VendorBatchNumber,Quantity,MaterialNumber)
VALUES(@vendorName,@batchNumber,@qty,@materialNumber);

COMMIT TRAN
END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH 
GO


CREATE OR ALTER PROCEDURE Distillation.RawMaterialUpdate
    (@materialNumber AS INT,
    @vendorBatchNumber AS VARCHAR(25) = NULL,
    @inspectionLotNumber NUMERIC = NULL,
    @sapBatchNumber INT = NULL,
    @sampleSubmitNumber CHAR(8) = NULL,
    @containerNumber CHAR(7) = NULL,
    @quantity AS INT = 1,
    @drumWeight AS DECIMAL(6,2) = NULL
    )
    AS
    BEGIN TRAN EnterRawMaterial
    BEGIN TRY

    DECLARE @vendor VARCHAR(50)
    SET @vendor = (SELECT VendorName FROM Vendors.VendorBatch
                    WHERE VendorBatchNumber = @vendorBatchNumber)
                    
    WHILE(@quantity > 0)
        BEGIN
        INSERT INTO Distillation.RawMaterial
            (DrumLotNumber, MaterialNumber, DrumWeight, SapBatchNumber, ContainerNumber, VendorBatchNumber)
        VALUES
            (Distillation.SetDrumId(@materialNumber,@vendor), @materialNumber, @drumWeight, @sapBatchNumber, @containerNumber, @vendorBatchNumber)

          SET @quantity=@quantity-1
        END
        
    COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
    END CATCH
GO

--TRIGGERS
CREATE OR ALTER TRIGGER Distillation.UpdateProductLotNumber
ON Distillation.Production
AFTER INSERT,UPDATE
AS

IF(UPDATE(SampleSubmitNumber))
BEGIN
DECLARE @product VARCHAR(10)
SET @product = (SELECT top(1) inserted.ProductLotNumber FROM inserted)

DECLARE @id INT
SET @id = (SELECT ProductId From Distillation.Production WHERE ProductLotNumber = @product)

UPDATE Distillation.Production
SET ProductLotNumber = Distillation.UpdateProductId(@product)
WHERE ProductLotNumber = @product
END
GO

--FUNCTIONS

--Sets Lot Number with date code.
CREATE or ALTER FUNCTION Distillation.SetDrumId
    (@materialNumber AS INT, @vendorName VARCHAR(50) = NULL)
    RETURNS CHAR(10) 
    AS 
BEGIN

    DECLARE @newDrumId INT
    DECLARE @drumLotNumber AS VARCHAR(10)
    DECLARE @alphabeticDate AS CHAR(1)
    DECLARE @drumId VARCHAR(10)
    DECLARE @sequenceId INT

    SET @alphabeticDate = (SELECT AlphabeticCode
    FROM AlphabeticDate
    WHERE MonthNumber = MONTH(GETDATE()));

    SET @drumId = (SELECT TOP(1) DrumLotNumber from Distillation.RawMaterial
    WHERE materialnumber = @materialNumber
    ORDER BY DrumLotNumber DESC)

    IF(@drumId != NULL)
        BEGIN
        IF (LEN(@drumId)=10 OR LEN(@drumId)=6)
            BEGIN
            SET @newDrumId = CAST(LEFT(@drumId,4)AS INT)+1
            END
        ELSE
            BEGIN
            SET @newDrumId = CAST(LEFT(@drumId,3)AS INT)+1
            END

        SET @drumLotNumber = (SELECT CONCAT(@newdrumId, Material.RawMaterialCode,RIGHT(YEAR(GETDATE()),1),@alphabeticDate,FORMAT(GETDATE(),'dd')) AS 'Drum ID'
                                FROM Materials.MaterialNumber
                                    JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
                                    JOIN Materials.Material ON Material.MaterialNumber = MaterialNumber.ParentMaterialNumber
                                WHERE MaterialNumber.MaterialNumber = @materialNumber)
        END
    ELSE
        BEGIN
        SET @sequenceId = (SELECT SequenceIdStart FROM Distillation.ProductNumberSequence 
                            JOIN Materials.MaterialId ON ProductNumberSequence.SequenceId = MaterialId.SequenceId
                            WHERE MaterialId.MaterialNumber = @materialNumber AND MaterialId.VendorName = @vendorName)

        SET @drumLotNumber = (SELECT CONCAT(@sequenceId, Material.RawMaterialCode,RIGHT(YEAR(GETDATE()),1),@alphabeticDate,FORMAT(GETDATE(),'dd')) AS 'Drum ID'
        FROM Materials.MaterialNumber
            JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
            JOIN Materials.Material ON Material.MaterialNumber = MaterialNumber.ParentMaterialNumber
        WHERE MaterialNumber.MaterialNumber = @materialNumber)
        END

    RETURN @drumLotNumber
END


GO

CREATE OR ALTER FUNCTION Materials.SpecificGravity(@materialName AS CHAR(20), @WeightLiters AS DECIMAL(5,2))
RETURNS DECIMAL 
AS
BEGIN

DECLARE @weightKG AS DECIMAL(5,3)
SET @weightKG = (@weightLiters * (SELECT SpecificGravity 
                                    FROM Materials.Material
                                    WHERE MaterialNameAbreviation = @materialName ));

RETURN @weightKG;
END
GO

CREATE OR ALTER PROCEDURE SystemDataInsertDB
AS
BEGIN

Create Table #tempSystemTbl(
    IndicatorType VARCHAR(50),
    IsRequired BIT,
    MaterialName VARCHAR(25),
    Nomenclature VARCHAR(50),
    Indicator VARCHAR(10),
    SetPoint DECIMAL(6,2),
    Variance DECIMAL(6,2)
);

BULK INSERT #tempSystemTbl FROM '..\..\usr\dbfiles\BuildFiles\SystemData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );
BEGIN TRAN
    BEGIN TRY

        INSERT INTO Engineering.SystemIndicator(IndicatorType)
        SELECT DISTINCT IndicatorType FROM #tempSystemTbl
    
        INSERT INTO Engineering.SystemNomenclature(Nomenclature)
        SELECT DISTINCT Nomenclature FROM #tempSystemTbl

        INSERT INTO Engineering.IndicatorSetPoint(IndicatorType,IsRequired,MaterialNumber,Nomenclature,Indicator,SetPoint,Variance)
        SELECT (SELECT IndicatorType FROM Engineering.SystemIndicator WHERE IndicatorType = #tempSystemTbl.IndicatorType),
            IsRequired, 
            (SELECT MaterialNumber FROM Materials.Material WHERE Material.MaterialName = #tempSystemTbl.MaterialName),
            (SELECT Nomenclature FROM Engineering.SystemNomenclature WHERE Nomenclature = #tempSystemTbl.Nomenclature),
            Indicator,
            SetPoint,
            Variance
         FROM #tempSystemTbl

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK;
    END CATCH
END
GO

CREATE OR ALTER PROCEDURE MaterialInsertDB
AS
BEGIN
CREATE TABLE #tempTbl(
    MaterialName VARCHAR(50),
    MaterialNameAbreviation VARCHAR(15),
    MaterialNumber INT,
    PermitNumber VARCHAR(25),
    RawMaterialCode VARCHAR(3),
    ProductCode VARCHAR(3),
    CarbonDrumRequired BIT,
    CarbonDrumWeight INT, 
    CarbonDrumDays INT,
    VacuumTrapRequired BIT,
    VacuumTrapDaysAllowed INT,
    SpecificGravity DECIMAL(3,2),
    PrefractionRefluxRatio VARCHAR(5),
    CollectRefluxRatio VARCHAR(5),
    NumberOfRuns INT,
    BatchManaged BIT,
    RequiresProcessOrder BIT,
    UnitOfIssue VARCHAR(2),
    IsRawMaterial BIT,
    Vendor VARCHAR(25),
    IsMPPS BIT,
    SequenceId INT);

BULK INSERT #tempTbl FROM '..\..\usr\dbfiles\BuildFiles\MaterialData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );
    BEGIN TRAN 
        BEGIN TRY
            
            INSERT INTO Materials.Material(MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,RawMaterialCode,ProductCode,CarbonDrumRequired,CarbonDrumDaysAllowed,CarbonDrumWeightAllowed,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns)
            SELECT TOP(6) MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,RawMaterialCode,ProductCode,CarbonDrumRequired,CarbonDrumDays,CarbonDrumWeight,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns
            FROM #tempTbl
            WHERE NOT EXISTS(SELECT * FROM Materials.Material WHERE Material.MaterialName = #tempTbl.MaterialName)
            
            INSERT INTO Materials.MaterialNumber(MaterialNumber,ParentMaterialNumber,BatchManaged,RequiresProcessOrder,UnitOfIssue,IsRawMaterial)
            SELECT MaterialNumber,(Select MaterialNumber FROM Materials.Material WHERE Material.MaterialName = #tempTbl.MaterialName),BatchManaged,RequiresProcessOrder,UnitOfIssue,IsRawMaterial
            FROM #tempTbl
            WHERE NOT EXISTS(SELECT * FROM Materials.MaterialNumber WHERE MaterialNumber.MaterialNumber = #tempTbl.MaterialNumber)

        
            INSERT INTO Vendors.Vendor(VendorName,IsMPPS)
            SELECT DISTINCT Vendor,IsMPPS
            FROM #tempTbl
            WHERE NOT EXISTS(Select * FROM Vendors.Vendor WHERE Vendor.VendorName = #tempTbl.Vendor)

            INSERT INTO Materials.MaterialId(MaterialNumber, VendorName, CurrentSequenceId, SequenceId)
            SELECT MaterialNumber,(SELECT VendorName FROM Vendors.Vendor WHERE VendorName = #tempTbl.Vendor),
            (SELECT SequenceIdStart FROM Distillation.ProductNumberSequence WHERE SequenceId = #tempTbl.SequenceId),
            #tempTbl.SequenceId
            FROM #tempTbl


            COMMIT TRAN;
        END TRY
        BEGIN CATCH
           ROLLBACK;
        END CATCH
END
GO
CREATE OR ALTER PROCEDURE InsertProduct
AS
BEGIN
CREATE TABLE #product(
    ProductLotNumber VARCHAR(10),
    MaterialNumber INT,
    ProductionBatchNumber INT,
    ProcessOrder NUMERIC,
    ReceiverId INT,
    SampleSubmitNumber CHAR(8),
    InspectionLotNumber BIGINT,
    SampleDate DATE
);

BULK INSERT #product FROM '..\..\usr\dbfiles\BuildFiles\ProductData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

    BEGIN TRAN
        BEGIN TRY

            INSERT INTO Distillation.Production(ProductLotNumber,MaterialNumber,ProductBatchNumber,ProcessOrder,ReceiverId,InspectionLotNumber)
            SELECT ProductLotNumber,MaterialNumber,ProductionBatchNumber,ProcessOrder,ReceiverId,InspectionLotNumber
            FROM #product

            INSERT INTo QualityControl.SampleSubmit(SampleSubmitNumber,InspectionLotNumber,SampleDate)
            SELECT SampleSubmitNumber,InspectionLotNumber,SampleDate FROM #product

            UPDATE Distillation.Production
            SET SampleSubmitNumber = (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmit.InspectionLotNumber = Production.InspectionLotNumber)

        COMMIT TRAN;
        END TRY
        BEGIN CATCH
            THROW;
            ROLLBACK;
        END CATCH

END

GO
EXEC MaterialInsertDB
GO
EXEC SystemDataInsertDB
GO
EXEC InsertProduct