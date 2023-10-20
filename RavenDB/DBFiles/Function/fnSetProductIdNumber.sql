CREATE OR ALTER FUNCTION Distillation.SetProductIdNumber(@materialNumber INT, @vendor AS VARCHAR(25) = 'Finished Product')
RETURNS VARCHAR(7)
AS
BEGIN

Declare @isRM AS BIT
SET @isRM = (SELECT IsRawMaterial  
                From Materials.MaterialNumber
                WHERE MaterialNumber = @materialNumber);

DECLARE @id AS VARCHAR(4)

    DECLARE @vid AS INT
    SET @vid = (SELECT VendorId
                FROM Vendors.Vendor
                WHERE VendorName = @vendor)

    SET @id = (SELECT FORMAT(MaterialId.CurrentSequenceId,'000')
                    FROM Materials.Material
                        JOIN Materials.MaterialNumber ON Material.NameId = MaterialNumber.NameId
                        JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
                    WHERE MaterialNumber.MaterialNumber = @materialNumber AND MaterialId.VendorId = @vid);
    
DECLARE @code AS VARCHAR(3)

IF (@isRM = 0)
    SET @code = (SELECT ProductCode 
                    FROM Materials.Material
                    JOIN Materials.MaterialNumber ON Material.NameId = MaterialNumber.NameId
                    WHERE MaterialNumber = @materialNumber);
ELSE
    SET @code = (SELECT RawMaterialCode 
                    FROM Materials.Material
                    JOIN Materials.MaterialNumber ON Material.NameId = MaterialNumber.NameId
                    WHERE MaterialNumber = @materialNumber);

DECLARE @LotId AS VARCHAR(7)
SET @LotId = (SELECT CONCAT(@id,@code));

RETURN @LotId
END