CREATE OR ALTER TRIGGER UpdateProductNumber
ON Distillation.Production
AFTER UPDATE
AS

IF(UPDATE(SampleSubmitNumber))
BEGIN
DECLARE @product VARCHAR(10)
SET @product = (SELECT inserted.ProductLotNumber FROM inserted)

UPDATE Distillation.Production
SET ProductLotNumber = Distillation.UpdateProductId(@product)
WHERE ProductLotNumber = @product
END