CREATE OR ALTER TRIGGER Distillation.AddProductToUsageLog
ON Distillation.ProductRunLog
AFTER INSERT
AS

DECLARE @productLotNumber VARCHAR(10)
DECLARE @DrumLotNumber VARCHAR(10)
DECLARE @heelsPumped BIT
DECLARE @runNumber VARCHAR(6)
DECLARE @startDate DATE

SET @productLotNumber = (SELECT inserted.ProductLotNumber FROM inserted )

