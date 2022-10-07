CREATE OR ALTER PROCEDURE Materials.MaterialInsert
    (@materialNumber AS INT,
    @materialName AS VARCHAR(50),
    @nameAbreviation AS VARCHAR(10),
    @permitNumber AS VARCHAR(25),
    @rawMaterialCode AS VARCHAR(3),
    @productCode AS VARCHAR(3),
    @carbonDrumRequired AS BIT,
    @carbonDrumDaysAllowed AS INT = NULL,
    @carbonDrumWeightAllowed AS INT = NULL,
    @batchManaged AS BIT,
    @requiresProcessOrder AS BIT,
    @unitOfIssue AS CHAR(2),
    @isRawMaterial AS BIT,
    @vendorName AS VARCHAR(25),
    @sequenceNumber AS INT)
AS
BEGIN TRAN MaterialInsert
BEGIN TRY 
INSERT INTO Materials.Material
    (MaterialName, MaterialNameAbreviation, PermitNumber, RawMaterialCode, ProductCode, CarbonDrumRequired, CarbonDrumDaysAllowed, CarbonDrumWeightAllowed)
VALUES(@materialName, @nameAbreviation, @permitNumber, @rawMaterialCode, @productCode, @carbonDrumRequired, @carbonDrumDaysAllowed, @carbonDrumWeightAllowed);

DECLARE @parentMaterialNumber AS INT
SET @parentMaterialNumber = (SELECT ParentMaterialNumber
FROM Materials.Material
WHERE MaterialName = @materialName);

INSERT INTO Materials.MaterialNumber
    (MaterialNumber, ParentMaterialNumber,  BatchManaged, RequiresProcessOrder, UnitOfIssue, IsRawMaterial)
VALUES(@materialNumber, @parentMaterialNumber,  @batchManaged, @requiresProcessOrder, @unitOfIssue, @isRawMaterial);

IF NOT EXISTS(SELECT VendorName
FROM Vendors.Vendor
WHERE VendorName = @vendorName)

BEGIN
    INSERT INTO Vendors.Vendor
        (VendorName)
    VALUES(@vendorName);
END

DECLARE @sequenceId AS INT
SET @sequenceId =(SELECT sequenceId
FROM Distillation.ProductNumberSequence
WHERE sequenceIdStart = @sequenceNumber);

DECLARE @currentSequenceId AS INT
SET @currentSequenceId =(SELECT sequenceIdStart
FROM Distillation.ProductNumberSequence
WHERE sequenceId = @sequenceId);

INSERT INTO Materials.MaterialId
    (MaterialNumber, VendorName, SequenceId, CurrentSequenceId)
VALUES(@materialNumber, @vendorName, @sequenceId, @currentSequenceId);
COMMIT;

END TRY
BEGIN CATCH
    THROW;
    ROLLBACK;
END CATCH
