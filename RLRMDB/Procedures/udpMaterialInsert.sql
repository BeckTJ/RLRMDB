CREATE OR ALTER PROCEDURE materialInsert
    (@materialNumber AS INT,
    @materialName AS VARCHAR(50),
    @nameAbreviation AS VARCHAR(10),
    @permitNumber AS VARCHAR(25),
    @rawMaterialCode AS VARCHAR(3),
    @productCode AS VARCHAR(3),
    @carbonDrumRequired AS BIT,
    @carbonDrumDaysAllowed AS INT,
    @carbonDrumWeightAllowed AS INT,
    @materialGrade AS CHAR(10),
    @batchManaged AS BIT,
    @requiresProcessOrder AS BIT,
    @unitOfIssue AS CHAR(2),
    @isRawMaterial AS BIT,
    @vendorName AS VARCHAR(25),
    @sequenceNumber AS INT)
AS

INSERT INTO materialName
    (materialName, materialNameAbreviation, permitNumber, rawMaterialCode, productCode, carbonDrumRequired, carbonDrumDaysAllowed,carbonDrumWeightAllowed)
VALUES(@materialName, @nameAbreviation, @permitNumber, @rawMaterialCode, @productCode, @carbonDrumRequired, @carbonDrumDaysAllowed, @carbonDrumWeightAllowed);

DECLARE @nameId AS INT
SET @nameId = (SELECT materialNameId
FROM materialName
WHERE materialName = @materialName);

INSERT INTO materialNumber
    (materialNumber, materialNameId, materialGrade, batchManaged, requiresProcessOrder, unitOfIssue, isRawMaterial)
VALUES(@materialNumber, @nameId, @materialGrade, @batchManaged, @requiresProcessOrder, @unitOfIssue, @isRawMaterial);

DECLARE @vendorId AS INT
IF EXISTS(SELECT vendorId
FROM vendor
WHERE vendorName = @vendorName)
BEGIN

    SET @vendorId =(SELECT vendorId
    FROM vendor
    WHERE vendorName = @vendorName);
END
ELSE
BEGIN
    INSERT INTO vendor
        (vendorName)
    VALUES(@vendorName);

    SET @vendorId =(SELECT vendorId
    FROM vendor
    WHERE vendorName = @vendorName);
END

DECLARE @sequenceId AS INT
SET @sequenceId =(SELECT sequenceId
FROM sequenceNumber
WHERE sequenceIdStart = @sequenceNumber);

DECLARE @currentSequenceId AS INT
SET @currentSequenceId =(SELECT sequenceIdStart
FROM sequenceNumber
WHERE sequenceId = @sequenceId)

INSERT INTO materialId
    (materialNumber, vendorId,sequenceId, currentSequenceId)
VALUES(@materialNumber, @vendorId, @sequenceId, @currentSequenceId);