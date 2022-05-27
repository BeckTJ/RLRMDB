CREATE OR ALTER PROCEDURE AddVendorBatch(@materailNumber AS INT,@Name AS VARCHAR(25),@batchNumber AS VARCHAR(50),@qty AS INT)
AS
DECLARE @id AS INT
SET @id = (SELECT VendorId 
            FROM Vendors.Vendor
            WHERE VendorName = @name)

INSERT INTO Vendors.VendorBatch(VendorId,VendorBatchNumber,Quantity,MaterialNumber)
VALUES(@id,@batchNumber,@qty,@materailNumber)