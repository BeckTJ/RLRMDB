CREATE OR ALTER FUNCTION GetMaterialId(@materialNumber AS INT, @vendorName AS VARCHAR(25))
RETURNS INT
AS 
BEGIN

    DECLARE @id AS INT
    SET @id = (SELECT vendorId 
            FROM Vendors.Vendor 
            WHERE VendorName = @vendorName)

    DECLARE @materialId AS INT
    SET @materialId = (SELECT MaterialId 
            FROM Materials.MaterialId
            WHERE VendorId = @id AND materialNumber = @materialNumber)

    RETURN @materialId;
END