CREATE OR ALTER TRIGGER incrementSequenceId
ON rawMaterial
AFTER INSERT,UPDATE

AS

DECLARE @vendorId AS INT
SET @vendorId = (select top(1)
    inserted.vendorId
FROM inserted);

DECLARE @materialNumber AS INT
SET @materialNumber = (select top(1)
    inserted.materialNumber
FROM inserted);

DECLARE @currentId AS INT
SET @currentId = (SELECT currentSequenceId
FROM materialId
WHERE vendorId = @vendorId AND materialNumber = @materialNumber);

DECLARE @increaseId AS INT
SET @increaseId = (@currentId +1);

UPDATE materialId 
SET currentsequenceId = (@increaseId)
WHERE vendorId = @vendorId AND materialNumber = @materialNumber;

