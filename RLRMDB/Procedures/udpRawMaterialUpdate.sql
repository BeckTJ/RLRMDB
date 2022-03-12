
CREATE OR ALTER PROCEDURE rawMaterialUpdate
    (@materialNumber AS INT,
    @vendorName AS VARCHAR(25),
    @vendorBatchNumber AS VARCHAR(25),
    @drumWeight INT,
    @sapBatchNumber INT,
    @containerNumber CHAR(7),
    @processOrder NUMERIC,
    @quantity AS INT
)
AS

DECLARE @vendorId AS INT
SET @vendorId = (SELECT vendorId
FROM vendor
WHERE vendorName = @vendorName);

DECLARE @drumId AS CHAR(10)
SET @drumId = (dbo.setDrumId(@materialNumber, @vendorName));

DECLARE @vendorBatchId AS INT

IF EXISTS(SELECT BatchId
FROM vendorBatchInformation
WHERE vendorBatchNumber = @vendorBatchNumber)

BEGIN
    SET @vendorBatchId = (dbo.obtainVendorBatchId(@vendorId,@vendorBatchNumber))
END

ELSE
BEGIN
    INSERT INTO vendorBatchInformation
        (vendorId, vendorBatchNumber, quantity)
    VALUES
        (@vendorId, @vendorBatchNumber, @quantity);

    SET @vendorBatchId = (dbo.obtainVendorBatchId(@vendorId,@vendorBatchNumber))
END

INSERT INTO rawMaterial
    (drumLotNumber, materialNumber, drumWeight,sapBatchNumber,containerNumber,processOrder,vendorId, vendorBatchId, dateUsed)
VALUES
    (@drumId, @materialNumber, @drumWeight, @sapBatchNumber, @containerNumber, @processOrder, @vendorId, @vendorBatchId, GETDATE());
