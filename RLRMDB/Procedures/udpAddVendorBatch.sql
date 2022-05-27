CREATE OR ALTER PROCEDURE Vendors.AddVendorBatch(@vendorName AS VARCHAR(25), @batchNumber AS VARCHAR(50))
AS
BEGIN TRAN VendorBatch
BEGIN TRY
DECLARE @vendorId AS INT
SET @vendorId = (SELECT vendorId 
                    FROM Vendors.Vendor
                    WHERE vendorName = @vendorName)

INSERT INTO Vendors.VendorBatch(vendorId,VendorBatchNumber)
VALUES(@vendorId,@batchNumber);
COMMIT TRAN
END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH