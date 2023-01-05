CREATE OR ALTER PROCEDURE Vendors.AddVendorBatch(@vendorName AS VARCHAR(25), @batchNumber AS VARCHAR(50),@materialNumber INT, @qty INT = 1)
AS
BEGIN TRAN VendorBatch
BEGIN TRY

    INSERT INTO Vendors.VendorBatch(VendorBatchNumber,VendorName,Quantity,MaterialNumber)
    VALUES(@batchNumber,@vendorName, @qty, @materialNumber)

COMMIT TRAN
END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH