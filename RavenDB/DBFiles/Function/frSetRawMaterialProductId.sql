CREATE OR ALTER FUNCTION Distillation.SetRawMaterialProductId(@materialNumber INT)
RETURNS VARCHAR(10)
AS
BEGIN

DECLARE @id INT
DECLARE @code VARCHAR(3)
DECLARE @productId VARCHAR(10)

SET @productId = (SELECT TOP(1) ProductLotNumber FROM Distillation.RawMaterial
                WHERE MaterialNumber = @materialNumber
                ORDER BY ProductLotNumber DESC) 

SET @code = (SELECT MaterialCode FROM Materials.MaterialId
                WHERE MaterialNumber = @materialNumber)  

IF (LEN(@productId) = 10 OR LEN(@productId) = 6)
BEGIN
    SET @id = SUBSTRING(@productId,1,4)
    SET @productId = CONCAT((@id+1),@code)
END
ELSE IF (LEN(@productId) = 9 OR LEN(@productId) = 5)
BEGIN
    SET @id = SUBSTRING(@productId,1,3)
    SET @productId = CONCAT((@id+1),@code)
END
ELSE
BEGIN
    SET @id = (SELECT SequenceId FROM Materials.MaterialId
                WHERE MaterialNumber = @materialNumber)
    SET @productId = CONCAT(@id,  @code)
END
RETURN @productId
END