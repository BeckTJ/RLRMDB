use raven
go
--Triggers
CREATE OR ALTER TRIGGER Materials.UpdateMaterialVendorCurrentSequenceId
ON Distillation.RawMaterial
AFTER INSERT 
AS

DECLARE @materialNumber INT
DECLARE @startId INT
DECLARE @currentId INT
DECLARE @totalRecords INT
DECLARE @newId INT

SET @materialNumber = (SELECT inserted.MaterialNumber FROM inserted);
SET @startId = (SELECT SequenceId FROM Materials.MaterialVendor
                    WHERE MaterialNumber = @materialNumber);
SET @currentId = (SELECT CurrentSequenceId FROM Materials.MaterialVendor
                    WHERE MaterialNumber = @materialNumber);
SET @totalRecords = (SELECT TotalRecords FROM Materials.MaterialVendor
                    WHERE MaterialNumber = @materialNumber);
SET @newId = @currentId + 1;

IF(@newId = (@startId + @totalRecords))
    BEGIN
        UPDATE Materials.MaterialVendor
        SET CurrentSequenceId = @startId
        WHERE MaterialNumber = @materialNumber;
    END
ELSE
    BEGIN
        UPDATE Materials.MaterialVendor
        SET CurrentSequenceId = @currentId + 1
        WHERE MaterialNumber = @materialNumber;
    END
GO

CREATE OR ALTER TRIGGER QualityControl.SetSampleDates
ON QualityControl.SampleSubmit
AFTER UPDATE
AS

DECLARE @rejected AS BIT
SET @rejected = (SELECT inserted.Rejected
                    FROM inserted)

DECLARE @approved AS BIT
SET @approved = (SELECT inserted.Approved 
                    FROM inserted)

IF(@approved = 1)
    UPDATE QualityControl.SampleSubmit
    SET ReviewDate = GETDATE(),
        ExperiationDate = DATEADD(YEAR,1,GETDATE())
    WHERE sampleId = (SELECT inserted.sampleId FROM inserted);    

ELSE IF(@rejected = 1)

    UPDATE QualityControl.SampleSubmit
    SET ReviewDate = GETDATE()
    WHERE sampleId = (SELECT inserted.sampleId FROM inserted);    

GO
--PROCEDURES

CREATE OR ALTER PROCEDURE QualityControl.SubmitSample
    (@sampleType AS CHAR(3),@sampleNumber AS INT, @lotNumber AS NUMERIC, @sampleDate DATE)
AS

DECLARE @id VARCHAR(10)
DECLARE @productId VARCHAR(10)

SET @id = (SELECT ProductLotNumber FROM Distillation.Production
            WHERE InspectionLotNumber = @lotNumber)

set @productId = Distillation.UpdateProductId(@id,@sampleDate)

INSERT INTO QualityControl.SampleSubmit(SampleType ,SampleId, InspectionLotNumber, SampleDate)
VALUES
    (@sampleType, @sampleNumber, @lotNumber, @sampleDate);

UPDATE Distillation.Production
SET sampleId = (SELECT sampleId FROM QualityControl.SampleSubmit
                            WHERE sampleId = @sampleNumber),
    ProductLotNumber = @productId
WHERE InspectionLotNumber = @lotNumber
GO

CREATE OR ALTER PROCEDURE Materials.AddVendorBatch(@batchNumber AS VARCHAR(25),@materialNumber INT,@sampleId VARCHAR(8), @qty INT = 1)
AS
BEGIN TRAN VendorBatch

    INSERT INTO Materials.VendorLot(VendorLotNumber,Quantity,sampleId,MaterialNumber)
    VALUES(@batchNumber,@qty,(SELECT sampleId FROM QualityControl.SampleSubmit Where sampleId = @sampleId), (SELECT MaterialNumber FROM Materials.MaterialVendor WHERE MaterialNumber = @materialNumber))

COMMIT TRAN
GO

CREATE OR ALTER PROCEDURE Distillation.RawMaterialUpdate
    (@materialNumber AS INT,
    @vendorBatchNumber AS VARCHAR(25) = NULL,
    @inspectionLotNumber NUMERIC = NULL,
    @sapBatchNumber INT = NULL,
    @sampleId INT = 0,
    @sampleDate DATE = NULL,
    @containerNumber CHAR(7) = NULL,
    @quantity INT = 1,
    @drumWeight DECIMAL(6,2) = NULL
    )
    AS
    BEGIN TRAN EnterRawMaterial
                    
    WHILE(@quantity > 0)
        BEGIN
        INSERT INTO Distillation.RawMaterial
            (DrumLotNumber, MaterialNumber, DrumWeight, SapBatchNumber, ContainerNumber, VendorLotNumber, sampleId)
        VALUES
            (Distillation.SetDrumId(@materialNumber,@sampleDate),(SELECT MaterialNumber FROM Materials.MaterialVendor WHERE MaterialNumber = @materialNumber), @drumWeight, @sapBatchNumber, @containerNumber, (SELECT VendorLotNumber FROM Materials.VendorLot WHERE VendorLotNumber = @vendorBatchNumber),(SELECT sampleId FROM QualityControl.SampleSubmit WHERE sampleId = @sampleId))

          SET @quantity=@quantity-1
        END
    COMMIT TRAN;
GO
CREATE OR ALTER PROCEDURE Distillation.SetRawMaterial(
    @materialNumber INT,
    @vendorBatchNumber VARCHAR(25) = NULL,
    @inspectionLotNumber NUMERIC = NULL,
    @sapBatchNumber INT = NULL,
    @sampleType CHAR(3),
    @sampleId INT,
    @containerNumber CHAR(7) = NULL,
    @numberOfDrums INT = 1,
    @drumWeight DECIMAL(6,2) = NULL,
    @sampleDate DATE    
)
AS

    EXEC QualityControl.SubmitSample @sampleType,@sampleId,@inspectionlotNumber,@sampleDate;
    EXEC Materials.AddVendorBatch @vendorBatchNumber, @materialNumber, @sampleId, @numberOfDrums;
    EXEC Distillation.RawMaterialUpdate @materialNumber, @vendorBatchNumber,@inspectionLotNumber,@sapBatchNumber,@sampleId,@sampleDate,@containerNumber,@numberOfDrums,@drumWeight
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

    SET @materialCode = (SELECT MaterialCode FROM Materials.Material WHERE MaterialNumber = @materialNumber)

    IF @id = NULL OR NOT EXISTS(SELECT TOP(1) ProductLotNumber FROM Distillation.Production WHERE MaterialNumber = @materialNumber ORDER BY ProductLotNumber DESC)
        BEGIN
        SET @sequenceId = (SELECT SequenceId FROM Materials.Material
                            WHERE Material.MaterialNumber = @materialNumber )
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

CREATE or ALTER FUNCTION Distillation.SetDrumId (@materialNumber AS INT, @sampleDate DATE = NULL)
    RETURNS CHAR(10) 
    AS 
BEGIN

    DECLARE @newDrumId INT
    DECLARE @drumLotNumber AS VARCHAR(10)
    DECLARE @alphabeticDate AS CHAR(1)
    DECLARE @drumId VARCHAR(10)

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

        SET @drumLotNumber = (SELECT CONCAT(MaterialVendor.SequenceId, MaterialVendor.MaterialCode,RIGHT(YEAR(@sampleDate),1),@alphabeticDate,FORMAT(@sampleDate,'dd'))
        FROM Materials.MaterialVendor
        WHERE MaterialVendor.MaterialNumber = @materialNumber)
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

        SET @drumLotNumber = (SELECT CONCAT(@newdrumId, MaterialVendor.MaterialCode,RIGHT(YEAR(@sampleDate),1),@alphabeticDate,FORMAT(@sampleDate,'dd')) AS 'Drum ID'
                                FROM Materials.MaterialVendor
                                WHERE MaterialVendor.MaterialNumber = @materialNumber)
        END

    RETURN @drumLotNumber
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

BULK INSERT #tmpRun FROM '..\..\usr\raven\BuildFiles\RunData.csv'
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

BULK INSERT #tmpProduct FROM '..\..\usr\raven\BuildFiles\ProductLotData.csv'
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