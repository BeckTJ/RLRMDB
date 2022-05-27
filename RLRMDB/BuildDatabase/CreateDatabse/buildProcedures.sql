--Set Triggers
CREATE OR ALTER TRIGGER Materials.IncrementSequenceId
ON Materials.RawMaterialLog
AFTER INSERT,UPDATE

AS

DECLARE @vendorId AS INT
SET @vendorId = (select top(1)
    inserted.VendorId
FROM inserted);

DECLARE @materialNumber AS INT

SET @materialNumber = (select top(1)
    inserted.MaterialNumber
FROM inserted);

DECLARE @currentId AS INT
SET @currentId = (SELECT CurrentSequenceId
FROM MaterialId
WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber);

IF @currentId = (SELECT SequenceIdEnd
                FROM Distillation.ProductNumberSequence
                    JOIN MaterialId on ProductNumberSequence.SequenceId = MaterialId.SequenceId
                WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber)
SET @currentId = (SELECT sequenceIdStart FROM Distillation.ProductNumberSequence
                    JOIN MaterialId on ProductNumberSequence.SequenceId = MaterialId.SequenceId
                WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber)

ELSE
SET @currentId = (@currentId + 1)


UPDATE MaterialId 
SET CurrentsequenceId = (@currentId)
WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber;
GO

CREATE or ALTER TRIGGER SetInitSequenceId
ON Materials.MaterialId
AFTER INSERT,UPDATE
AS

UPDATE Materials.MaterialId
SET SequenceId = (SELECT SequenceId 
            FROM Distillation.ProductNumberSequence 
            WHERE SequenceIdStart = (SELECT inserted.CurrentSequenceId 
                                                                FROM inserted))
GO

--Set Functions
CREATE OR ALTER FUNCTION Vendors.ObtainVendorBatchId(@vendorId AS INT, @vendorBatchNumber AS CHAR(25))
RETURNS INT
AS
BEGIN

    DECLARE @vendorBatchId AS INT
    SET @vendorBatchId = (SELECT BatchId
    FROM Vendors.VendorBatchInformation
    WHERE VendorBatchNumber = @vendorBatchNumber);

    RETURN @vendorBatchId;
END
GO
        --SetDrumId
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
    FROM MaterialNumber
        JOIN MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
        JOIN Material ON Material.MaterialNameId = MaterialNumber.MaterialNameId
        JOIN Vendor ON Vendor.VendorId = MaterialId.VendorId
    WHERE MaterialNumber.MaterialNumber = @materialNumber
        AND Vendor.vendorName = @vendorName)

    RETURN @drumId
END
GO


--SET Procedures