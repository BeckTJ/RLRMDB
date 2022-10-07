CREATE OR ALTER FUNCTION Vendors.ObtainVendorBatchId(@name AS VARCHAR(25), @vendorBatchNumber AS CHAR(25))
RETURNS INT
AS
BEGIN
DECLARE @vendorBatchId AS INT
IF EXISTS(SELECT BatchId
            FROM Vendors.VendorBatch
            WHERE VendorBatchNumber = @vendorBatchNumber)
BEGIN
    SET @vendorBatchId = (SELECT BatchId
                            FROM Vendors.VendorBatch
                            WHERE VendorBatchNumber = @vendorBatchNumber);
END
ELSE
    
    EXEC AddVendorBatch @name, @vendorBatchNumber;
    
    SET @vendorBatchId = (SELECT BatchId
                            FROM Vendors.VendorBatch
                            WHERE VendorBatchNumber = @vendorBatchNumber)
    
    RETURN @vendorBatchId;
END