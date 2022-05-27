CREATE or ALTER TRIGGER SetInitSequenceId
ON Materials.MaterialId
AFTER INSERT,UPDATE
AS

UPDATE Materials.MaterialId
SET SequenceId = (SELECT SequenceId 
            FROM Distillation.ProductNumberSequence 
            WHERE SequenceIdStart = (SELECT inserted.CurrentSequenceId 
                                                                FROM inserted))