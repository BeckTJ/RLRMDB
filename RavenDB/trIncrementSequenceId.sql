--Not sure if i want to update here need to create a reset to allow the records to go back if 
-- a certain number is reached

CREATE OR ALTER TRIGGER IncrementSequenceId
ON Distillation.RawMaterial
AFTER INSERT 
AS
BEGIN

    UPDATE Materials.MaterialVendor
    SET SequenceId = SequenceId + 1
    WHERE MaterialNumber = (SELECT inserted.MaterialNumber FROM inserted)
END
GO

