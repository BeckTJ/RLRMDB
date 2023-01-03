CREATE OR ALTER TRIGGER Engineering.UpdateHourlyReads
ON Engineering.SystemNomenclature
AFTER INSERT,UPDATE
AS

DECLARE @nomen AS VARCHAR(50)
SET @nomen = (SELECT inserted.Nomenclature FROM inserted)

IF NOT EXISTS(SELECT * FROM Distillation.ProductionRunHourlyReads)
ALTER TABLE Distillation.ProductionRunHourlyReads
ADD @nomen DECIMAL(6,3)