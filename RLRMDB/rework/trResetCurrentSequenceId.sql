CREATE OR ALTER TRIGGER resetCurrentSequenceId
ON materialId
AFTER INSERT,UPDATE
AS

DECLARE @materialNumberId AS INT
SET @materialNumberId = (SELECT inserted.materialNumberId
FROM inserted);

DECLARE @vendorId AS INT
SET @vendorId = (SELECT inserted.vendorId
FROM inserted);

DECLARE @maxSequenceId AS INT
SET @maxSequenceId = (SELECT sequenceIdEnd
FROM productNumberSequence JOIN materialId ON materialId.sequenceId = productNumberSequence.sequenceId
WHERE vendorId = @vendorId AND materialNumberId = @materialNumberId);

DECLARE @currentId AS INT
SET @currentId =(
SELECT currentSequenceId
FROM materialId
WHERE vendorId = @vendorId AND materialNumberId = @materialNumberId);

IF (@currentId = @maxSequenceId)
BEGIN
    UPDATE materialId
SET currentSequenceId = (SELECT sequenceIdStart
    FROM productNumberSequence
        JOIN materialId ON materialId.sequenceId = productNumberSequence.sequenceId
    WHERE vendorId = @vendorId AND materialNumberId = @materialNumberId);
End
