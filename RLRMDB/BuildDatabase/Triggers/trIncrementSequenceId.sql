CREATE OR ALTER TRIGGER Materials.IncrementSequenceId
ON Materials.RawMaterial
AFTER INSERT,UPDATE

AS

DECLARE @vendorId AS INT
SET @vendorId = (select VendorId
                    From Vendors.VendorBatch
                    WHERE BatchId = (SELECT inserted.VendorBatchId
                                    FROM inserted));
                                                
DECLARE @materialNumber AS INT

SET @materialNumber = (select top(1)
    inserted.MaterialNumber
FROM inserted);

DECLARE @currentId AS INT
SET @currentId = (SELECT CurrentSequenceId
FROM MaterialId
WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber);

IF @currentId = (SELECT SequenceIdEnd
                FROM Distillation.ProductNumberSequence
                    JOIN MaterialId on ProductNumberSequence.SequenceId = MaterialId.SequenceId
                WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber)
SET @currentId = (SELECT sequenceIdStart FROM Distillation.ProductNumberSequence
                    JOIN MaterialId on ProductNumberSequence.SequenceId = MaterialId.SequenceId
                WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber)

ELSE
SET @currentId = (@currentId + 1)


UPDATE MaterialId 
SET CurrentsequenceId = (@currentId)
WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber;

