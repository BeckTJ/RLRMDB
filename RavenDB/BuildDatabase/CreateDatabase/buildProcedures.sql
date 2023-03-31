--PROCEDURES

CREATE OR ALTER PROCEDURE Materials.InsertMaterialNumber(@materialNumber INT,@parentMaterialNumber INT,@batchManaged BIT,@requiresProcessOrder BIT,@unitOfIssue CHAR(2),@isRawMaterial BIT)
AS
BEGIN

    IF NOT EXISTS(SELECT 1 FROM Materials.MaterialNumber WHERE MaterialNumber.MaterialNumber = @materialNumber)
    INSERT INTO Materials.MaterialNumber(MaterialNumber,ParentMaterialNumber,BatchManaged,RequiresProcessOrder,UnitOfIssue,IsRawMaterial)
    VALUES(@materialNumber,@parentMaterialNumber,@batchManaged,@requiresProcessOrder,@unitOfIssue,@isRawMaterial)

END
GO

CREATE OR ALTER PROCEDURE Materials.AddVendor(@vendorName AS VARCHAR(25))
AS
BEGIN

IF NOT EXISTS (SELECT VendorName FROM Materials.Vendor WHERE VendorName = @vendorName)

INSERT INTO Materials.Vendor(VendorName)
VALUES (@vendorName);

END
GO

CREATE OR ALTER PROCEDURE Distillation.UpdateProduction(@materialNumber INT, @batchNumber INT, @processOrder NUMERIC, @inspectionLotNumber NUMERIC = NULL, @sampleNumber CHAR(8) = NULL)
AS

    DECLARE @productId VARCHAR(10)
    SET @productId = Distillation.SetProductID(@materialNumber,'Finish Product')



    INSERT INTO Distillation.Production(ProductLotNumber,MaterialNumber, ProductBatchNumber, ProcessOrder, InspectionLotNumber, SampleSubmitNumber)
    VALUES(@productId,
        (SELECT MaterialNumber FROM Materials.Material WHERE MaterialNumber = @materialNumber),
        @batchNumber,
        @processOrder,
        @inspectionLotNumber,
        (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmitNumber = @sampleNumber))

GO

CREATE OR ALTER PROCEDURE QualityControl.SubmitSample
    (@sampleNumber AS CHAR(8), @lotNumber AS NUMERIC, @sampleDate DATE)
AS

DECLARE @id VARCHAR(10)
DECLARE @productId VARCHAR(10)

SET @id = (SELECT ProductLotNumber FROM Distillation.Production
            WHERE InspectionLotNumber = @lotNumber)

set @productId = Distillation.UpdateProductId(@id,@sampleDate)

INSERT INTO QualityControl.SampleSubmit(SampleSubmitNumber, InspectionLotNumber, SampleDate)
VALUES
    (@sampleNumber, @lotNumber, @sampleDate);

UPDATE Distillation.Production
SET SampleSubmitNumber = (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit
                            WHERE SampleSubmitNumber = @sampleNumber),
    ProductLotNumber = @productId
WHERE InspectionLotNumber = @lotNumber
GO

CREATE OR ALTER PROCEDURE Materials.AddVendorBatch(@vendorName AS VARCHAR(25), @batchNumber AS VARCHAR(25),@materialNumber INT, @qty INT = 1)
AS
BEGIN TRAN VendorBatch
BEGIN TRY

    INSERT INTO Materials.VendorBatch(VendorBatchNumber,VendorName,Quantity,MaterialNumber)
    VALUES(@batchNumber,(SELECT VendorName FROM Materials.Vendor WHERE VendorName = @vendorName), @qty, (SELECT MaterialNumber FROM Materials.MaterialNumber WHERE MaterialNumber = @materialNumber))

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
    @sampleDate DATE = NULL,
    @containerNumber CHAR(7) = NULL,
    @quantity INT = 1,
    @drumWeight DECIMAL(6,2) = NULL
    )
    AS
    BEGIN TRAN EnterRawMaterial
    BEGIN TRY

    DECLARE @vendor VARCHAR(50)
    SET @vendor = (SELECT VendorName FROM Materials.VendorBatch
                    WHERE VendorBatchNumber = @vendorBatchNumber)
                    
    WHILE(@quantity > 0)
        BEGIN
        INSERT INTO Distillation.RawMaterial
            (DrumLotNumber, MaterialNumber, DrumWeight, SapBatchNumber, ContainerNumber, VendorBatchNumber, SampleSubmitNumber)
        VALUES
            (Distillation.SetDrumId(@materialNumber,@vendor,@sampleDate), @materialNumber, @drumWeight, @sapBatchNumber, @containerNumber, @vendorBatchNumber,@sampleSubmitNumber)

          SET @quantity=@quantity-1
        END
        
    COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
    END CATCH
GO

CREATE OR ALTER PROCEDURE Distillation.SetRawMaterial(
    @materialNumber INT,
    @vendor CHAR(25),
    @vendorBatchNumber VARCHAR(25) = NULL,
    @inspectionLotNumber NUMERIC = NULL,
    @sapBatchNumber INT = NULL,
    @sampleSubmitNumber CHAR(8),
    @containerNumber CHAR(7) = NULL,
    @numberOfDrums INT = 1,
    @drumWeight DECIMAL(6,2) = NULL,
    @sampleDate DATE    
)
AS

    EXEC QualityControl.SubmitSample @sampleSubmitNumber,@inspectionlotNumber,@sampleDate;
    EXEC Materials.AddVendorBatch @vendor, @vendorBatchNumber, @materialNumber, @numberOfDrums;
    EXEC Distillation.RawMaterialUpdate @materialNumber, @vendorBatchNumber,@inspectionLotNumber,@sapBatchNumber,@sampleSubmitNumber,@sampleDate,@containerNumber,@numberOfDrums,@drumWeight
GO

--FUNCTIONS

--Sets Lot Number with date code.
CREATE OR ALTER FUNCTION Distillation.SetProductId(@materialNumber INT, @vendor VARCHAR(25))
RETURNs VARCHAR(6)
AS
BEGIN

    DECLARE @id VARCHAR(6)
    DECLARE @sequenceId INT
    DECLARE @materialCode VARCHAR(3)

    SET @materialCode = (SELECT MaterialCode FROM Materials.MaterialId WHERE MaterialNumber = @materialNumber)

    IF @id = NULL OR NOT EXISTS(SELECT TOP(1) ProductLotNumber FROM Distillation.Production WHERE MaterialNumber = @materialNumber ORDER BY ProductLotNumber DESC)
        BEGIN
        SET @sequenceId = (SELECT SequenceId FROM Materials.MaterialId 
                            WHERE MaterialId.MaterialNumber = @materialNumber AND MaterialId.VendorName = @vendor)
            SET @id = (CONCAT(@sequenceId,@materialCode))
        END
    ELSE
        BEGIN
        SET @id = (SELECT TOP(1) ProductLotNumber FROM Distillation.Production WHERE MaterialNumber = @materialNumber ORDER BY ProductLotNumber DESC)
        IF(LEN(@id)=10 OR LEN(@id)=6) 
            BEGIN
            SET @sequenceId = LEFT(@id,4)+1
            SET @id = CONCAT(@sequenceId,@materialCode)
            END
        ELSE
            BEGIN 
            SET @sequenceId = LEFT(@id,3)+1
            SET @id = CONCAT(@sequenceId,@materialCode)
            END
        END

        RETURN @id
END
GO

CREATE OR ALTER FUNCTION Distillation.UpdateProductId(@id VARCHAR(10),@sampleDate DATE = NULL)
RETURNS VARCHAR(10)
AS
BEGIN

IF @sampleDate = NULL
    BEGIN
    SET @sampleDate = GETDATE()
    END

DECLARE @alphabeticDate AS CHAR(1)
    SET @alphabeticDate = (SELECT AlphabeticCode
    FROM AlphabeticDate
    WHERE MonthNumber = MONTH(@sampleDate));
    
DECLARE @productId AS VARCHAR(10)
SET @productId = CONCAT(@id,RIGHT(YEAR(@sampleDate),1),@alphabeticDate, FORMAT(@sampleDate,'dd'))


RETURN @productId
END
GO

CREATE or ALTER FUNCTION Distillation.SetDrumId (@materialNumber AS INT, @vendorName VARCHAR(50) = NULL, @sampleDate DATE = NULL)
    RETURNS CHAR(10) 
    AS 
BEGIN

    DECLARE @newDrumId INT
    DECLARE @drumLotNumber AS VARCHAR(10)
    DECLARE @alphabeticDate AS CHAR(1)
    DECLARE @drumId VARCHAR(10)
    DECLARE @sequenceId INT

    SET @alphabeticDate = (SELECT AlphabeticCode
    FROM Distillation.AlphabeticDate
    WHERE MonthNumber = MONTH(GETDATE()));

    SET @drumId = (SELECT TOP(1) DrumLotNumber from Distillation.RawMaterial
    WHERE materialnumber = @materialNumber
    ORDER BY DrumLotNumber DESC)

        IF (@sampleDate = NULL)
    BEGIN
        SET @sampleDate = GETDATE()
    END

    IF (@drumId = NULL) OR (NOT EXISTS (SELECT TOP(1) DrumLotNumber from Distillation.RawMaterial
    WHERE materialnumber = @materialNumber
    ORDER BY DrumLotNumber DESC))
        BEGIN
        SET @sequenceId = (SELECT SequenceId FROM Materials.MaterialId 
                            WHERE MaterialId.MaterialNumber = @materialNumber AND MaterialId.VendorName = @vendorName)

        SET @drumLotNumber = (SELECT CONCAT(@sequenceId, MaterialId.MaterialCode,RIGHT(YEAR(@sampleDate),1),@alphabeticDate,FORMAT(@sampleDate,'dd'))
        FROM Materials.MaterialNumber
            JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
            JOIN Materials.Material ON Material.MaterialNumber = MaterialNumber.ParentMaterialNumber
        WHERE MaterialNumber.MaterialNumber = @materialNumber)
        END

    ELSE
        BEGIN
        IF (LEN(@drumId)=10 OR LEN(@drumId)=6)
            BEGIN
            SET @newDrumId = CAST(LEFT(@drumId,4)AS INT)+1
            END
        ELSE
            BEGIN
            SET @newDrumId = CAST(LEFT(@drumId,3)AS INT)+1
            END

        SET @drumLotNumber = (SELECT CONCAT(@newdrumId, MaterialId.MaterialCode,RIGHT(YEAR(@sampleDate),1),@alphabeticDate,FORMAT(@sampleDate,'dd')) AS 'Drum ID'
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

CREATE OR ALTER PROCEDURE Distillation.InsertProductLevels
AS
BEGIN

CREATE TABLE #tmpRun
(
    LotNumber VARCHAR(10),
    RunNumber INT,
    SystemStatus VARCHAR(2),
    VisualVerification BIT,
    ReceiverLevel INT,
    PrefractionLevel INT,
    ReboilerLevel INT,
    ReadTime TIME,
)

BULK INSERT #tmpRun FROM '..\..\usr\dbfiles\BuildFiles\RunData.csv'
    WITH
    (
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

    INSERT INTO Distillation.ProductLevels(ProductLotNumber,RunNumber,SystemStatus,VisualVerification,ReboilerLevel,PrefractionLevel,ReceiverLevel,ReadTime)
    SELECT LotNumber,RunNumber,SystemStatus,VisualVerification,ReboilerLevel,PrefractionLevel,ReceiverLevel,ReadTime
    FROM #tmpRun
END
GO

CREATE OR ALTER PROCEDURE Distillation.InsertProductLot
AS
BEGIN

CREATE TABLE #tmpProduct
(
LotNumber VARCHAR(10),
MaterialNumber INT,
ReceiverName VARCHAR(5),
ProcessOrder NUMERIC,
BatchNumber INT,
InspectionLotNumber NUMERIC,
RunNumber INT,
DrumLotNumber VARCHAR(10),
RawMaterialUsed INT,
StartDate DATE
)

BULK INSERT #tmpProduct FROM '..\..\usr\dbfiles\BuildFiles\ProductLotData.csv'
    WITH
    (
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

    INSERT INTO Distillation.Production(ProductLotNumber,MaterialNumber,ProductBatchNumber,ProcessOrder,ReceiverName)
    SELECT DISTINCT LotNumber,MaterialNumber,BatchNumber,ProcessOrder,ReceiverName FROM #tmpProduct
    WHERE NOT EXISTS(SELECT 1 FROM Distillation.Production WHERE Production.ProductLotNumber = #tmpProduct.LotNumber)

    INSERT INTO Distillation.ProductRun(ProductLotNumber,RunNumber,DrumLotNumber,RawMaterialUsed,RunStartDate)
    SELECT 
        (SELECT ProductLotNumber FROM Distillation.Production WHERE Production.ProductLotNumber = #tmpProduct.LotNumber),
        RunNumber,
        (SELECT DrumLotNumber FROM Distillation.RawMaterial WHERE RawMaterial.DrumLotNumber = #tmpProduct.DrumLotNumber),
        RawMaterialUsed,
        StartDate
      FROM #tmpProduct
END
GO