CREATE OR ALTER TRIGGER InsertProductId
ON Distillation.Production
FOR INSERT
AS

DECLARE @materialNumber INT
DECLARE @productId VARCHAR(10)

SET @materialNumber = (select inserted.MaterialNumber FROM inserted)
SET @productId = (Distillation.SetProductId(@materialNumber))


INSERT INTO Distillation.Production(ProductLotNumber)
VALUES (@productId)
