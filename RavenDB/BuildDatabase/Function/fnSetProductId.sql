CREATE OR ALTER FUNCTION Distillation.SetProductId(@materialNumber INT, @vendor VARCHAR(25))
RETURNs VARCHAR(6)
AS
BEGIN

    DECLARE @id VARCHAR(6)
    DECLARE @sequenceId INT
    DECLARE @materialCode VARCHAR(3)

    SET @materialCode = (SELECT ProductCode FROM Materials.Material WHERE MaterialNumber = @materialNumber)

    IF @id = NULL OR NOT EXISTS(SELECT TOP(1) ProductLotNumber FROM Distillation.Production WHERE MaterialNumber = @materialNumber ORDER BY ProductLotNumber DESC)
        BEGIN
        SET @sequenceId = (SELECT SequenceIdStart FROM Distillation.ProductNumberSequence 
                            JOIN Materials.MaterialId ON ProductNumberSequence.SequenceId = MaterialId.SequenceId
                            WHERE MaterialId.MaterialNumber = @materialNumber AND MaterialId.VendorName = @vendor)
            SET @id = (CONCAT(@sequenceId,@materialCode))
        END
    ELSE
        BEGIN
        SET @id = (SELECT TOP(1) ProductLotNumber FROM Distillation.Production WHERE MaterialNumber = @materialNumber ORDER BY ProductLotNumber DESC)
        IF(LEN(@id)=10 OR LEN(@id)=6) 
            BEGIN
            SET @sequenceId = LEFT(@id,4)+1
            SET @id = CONCAT(@sequenceId,@materialCode)
            END
        ELSE
            BEGIN 
            SET @sequenceId = LEFT(@id,3)+1
            SET @id = CONCAT(@sequenceId,@materialCode)
            END
        END

        RETURN @id
END