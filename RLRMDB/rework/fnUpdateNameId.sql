CREATE OR ALTER FUNCTION updateNameId()
RETURNS INT
AS
BEGIN

    DECLARE @nameId AS INT
    SET @nameId =(SELECT TOP(1)
        materialNameId
    FROM materialName
    ORDER BY materialNameId DESC)

    DECLARE @newNameId AS INT
    SET @newNameId = @nameId + 1

    RETURN @newNameId
END