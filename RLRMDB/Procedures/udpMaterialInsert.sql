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
    @isRawMaterial AS BIT)
AS

DECLARE @nameId AS INT
SET @nameId = dbo.updateNameId()


INSERT INTO materialName
    (materialNameId, materialName, materialNameAbreviation, permitNumber, rawMaterialCode, productCode, carbonDrumRequired, carbonDrumDaysAllowed,carbonDrumWeightAllowed)
VALUES(@nameId, @materialName, @nameAbreviation, @permitNumber, @rawMaterialCode, @productCode, @carbonDrumRequired, @carbonDrumDaysAllowed, @carbonDrumWeightAllowed)

INSERT INTO materialNumber
    (materialNumber, materialNameId, materialGrade, batchManaged, requiresProcessOrder, unitOfIssue, isRawMaterial)
VALUES(@materialNumber, @nameId, @materialGrade, @batchManaged, @requiresProcessOrder, @unitOfIssue, @isRawMaterial);