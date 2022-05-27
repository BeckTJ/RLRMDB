UPDATE Materials.MaterialId
SET SequenceId = (SELECT SequenceId 
                    FROM Distillation.ProductNumberSequence
                    WHERE SequenceIdStart = CurrentSequenceId)