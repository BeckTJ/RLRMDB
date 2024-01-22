CREATE OR ALTER TRIGGER InsertRawMaterialId
ON Distillation.RawMaterial
FOR INSERT
AS

DECLARE @materialNumber INT
DECLARE @productId VARCHAR(10)

SET @materialNumber = (select inserted.MaterialNumber FROM inserted)
SET @productId = (Distillation.SetRawMaterialProductId(@materialNumber))


INSERT INTO Distillation.RawMaterial(DrumLotNumber)
VALUES (@productId)
