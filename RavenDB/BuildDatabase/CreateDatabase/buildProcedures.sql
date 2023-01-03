--PROCEDURES

CREATE OR ALTER PROCEDURE Vendors.AddVendor(@vendorName AS VARCHAR(25), @isMpps AS BIT)
AS
BEGIN

IF NOT EXISTS (SELECT VendorName FROM Vendors.Vendor WHERE VendorName = @vendorName)

INSERT INTO Vendors.Vendor(VendorName,IsMPPS)
VALUES (@vendorName, @isMpps);

END
GO

CREATE OR ALTER PROCEDURE Vendors.AddVendorBatch(@vendorName AS VARCHAR(25), @batchNumber AS VARCHAR(50))
AS
BEGIN TRAN VendorBatch
BEGIN TRY

INSERT INTO Vendors.VendorBatch(VendorName,VendorBatchNumber)
VALUES(@vendorName,@batchNumber);
COMMIT TRAN
END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH
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


CREATE OR ALTER PROCEDURE Distillation.RawMaterialUpdate
    (@materialNumber AS INT,
    @vendorName AS VARCHAR(25),
    @vendorBatchNumber AS VARCHAR(25),
    @drumWeight INT = NULL,
    @sapBatchNumber INT = NULL,
    @containerNumber CHAR(7)=NULL,
    @quantity AS INT = 1
)
AS
BEGIN TRAN EnterRawMaterial
BEGIN TRY

DECLARE @drumId AS CHAR(10)
SET @drumId = (Distillation.setDrumId(@materialNumber, @vendorName));

INSERT INTO Distillation.RawMaterial
    (DrumLotNumber, MaterialNumber, DrumWeight, SapBatchNumber, ContainerNumber, VendorBatchNumber)
VALUES
    (@drumId, @materialNumber, @drumWeight, @sapBatchNumber, @containerNumber, @vendorBatchNumber);

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
CREATE or ALTER FUNCTION Distillation.SetDrumId
    (@materialNumber AS INT,
    @vendorName AS CHAR(20))
    RETURNS CHAR(10) 
    AS 
BEGIN

    DECLARE @alphabeticDate AS CHAR(1)
    SET @alphabeticDate = (SELECT AlphabeticCode
    FROM AlphabeticDate
    WHERE MonthNumber = MONTH(GETDATE()));

    DECLARE @drumId AS CHAR(10)
    SET @drumId =(
    SELECT CONCAT(FORMAT(MaterialId.CurrentSequenceId,'000'), Material.RawMaterialCode,RIGHT(YEAR(GETDATE()),1),@alphabeticDate,FORMAT(GETDATE(),'dd')) AS 'Drum ID'
    FROM Materials.MaterialNumber
        JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
        JOIN Materials.Material ON Material.MaterialNumber = MaterialNumber.ParentMaterialNumber
        JOIN Vendors.Vendor ON Vendor.VendorName = MaterialId.VendorName
    WHERE MaterialNumber.MaterialNumber = @materialNumber
        AND Vendor.VendorName = @vendorName)

    RETURN @drumId
END
GO

CREATE OR ALTER FUNCTION HumanResources.EmployeeInitials(@employeeId AS CHAR(7) = 'NA')
RETURNS CHAR(2)
AS
BEGIN
DECLARE @employeeInit AS CHAR(2)
SET @employeeInit = (CONCAT(Left(1,(SELECT FirstName FROM HumanResources.Employee WHERE EmployeeId = @employeeId)),Left(1,(SELECT LastName FROM HumanResources.Employee WHERE EmployeeId = @employeeId))))

RETURN @employeeInit;
END
GO

CREATE OR ALTER FUNCTION Distillation.UpdateProductId(@id VARCHAR(10))
RETURNS VARCHAR(10)
AS
BEGIN

DECLARE @sampleDate DATE
SET @sampleDate = (SELECT SampleDate FROM QualityControl.SampleSubmit
                    JOIN Distillation.Production ON SampleSubmit.SampleSubmitNumber = Production.SampleSubmitNumber
                    WHERE SampleSubmit.SampleSubmitNumber = (SELECT SampleSubmitNumber FROM Distillation.Production
                    WHERE ProductLotNumber = @id))

DECLARE @alphabeticDate AS CHAR(1)
    SET @alphabeticDate = (SELECT AlphabeticCode
    FROM AlphabeticDate
    WHERE MonthNumber = MONTH(@SampleDate));


DECLARE @productId AS VARCHAR(10)
SET @productId = CONCAT(@id,RIGHT(YEAR(@sampleDate),1),@alphabeticDate, FORMAT(@sampleDate,'dd'))


RETURN @productId
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
            SELECT Distillation.UpdateProductID(ProductLotNumber),MaterialNumber,ProductionBatchNumber,ProcessOrder,ReceiverId,InspectionLotNumber
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