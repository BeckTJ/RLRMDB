CREATE OR ALTER FUNCTION obtainVendorBatchId(@vendorId AS INT, @vendorBatchNumber AS CHAR(25))
RETURNS INT
AS
BEGIN

    DECLARE @vendorBatchId AS INT
    SET @vendorBatchId = (SELECT BatchId
    FROM vendorBatchInformation
    WHERE VendorBatchNumber = @vendorBatchNumber);

    RETURN @vendorBatchId;
END