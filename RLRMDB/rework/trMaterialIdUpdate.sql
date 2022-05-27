CREATE OR ALTER TRIGGER materialIdUpdate
ON materialNumber
AFTER INSERT    
AS
BEGIN

    DECLARE @matierlaNumber AS INT
    SET @materialNumber =(SELECT inserted.materialNumber
    FROM inserted)

    DECLARE @vendorId AS INT
    SET @vendorId = (Select vendorId
    FROM vendor
    WHERE vendorName = (SELECT inserted.vendorName
    FROM inserted))

    INSERT INTO materialId
        (materialNumber, vendorId,sequenceId, currentSequenceId)
    VALUES(@materialNumber, @vendorId, @sequenceId, @currentSequenceId)

END