CREATE OR ALTER PROCEDURE Materials.MaterialInsert
    (@materialNumber INT,
    @materialName VARCHAR(50),
    @nameAbreviation VARCHAR(10),
    @permitNumber VARCHAR(25),
    @MaterialCode VARCHAR(3),
    @carbonDrumRequired BIT,
    @carbonDrumDaysAllowed INT = NULL,
    @carbonDrumWeightAllowed  INT = NULL,
    @vacuumTrapRequired BIT,
    @vacuumTrapDaysAllowed INT,
    @specificGravity DECIMAL(6,2),
    @prefractionRefluxRatio VARCHAR(6),
    @collectRefluxRatio VARCHAR(6),
    @numberOfRuns INT,
    @batchManaged BIT,
    @requiresProcessOrder BIT,
    @unitOfIssue CHAR(2),
    @isRawMaterial BIT,
    @vendorName VARCHAR(25),
    @sequenceNumber INT)
AS
BEGIN TRAN MaterialInsert
BEGIN TRY 

INSERT INTO Materials.Material
    (MaterialNumber, MaterialName, MaterialNameAbreviation, PermitNumber, CarbonDrumRequired, CarbonDrumDaysAllowed, CarbonDrumWeightAllowed,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns)
VALUES(@materialName, @nameAbreviation, @permitNumber, @carbonDrumRequired, @carbonDrumDaysAllowed, @carbonDrumWeightAllowed, @vacuumTrapRequired, @vacuumTrapDaysAllowed, @specificGravity, @prefractionRefluxRatio, @collectRefluxRatio, @numberOfRuns);

INSERT INTO Materials.MaterialNumber
    (MaterialNumber, ParentMaterialNumber,  BatchManaged, RequiresProcessOrder, UnitOfIssue, IsRawMaterial)
VALUES(@materialNumber, (SELECT MaterialNumber FROM Materials.Material WHERE MaterialNameAbreviation ),  @batchManaged, @requiresProcessOrder, @unitOfIssue, @isRawMaterial);

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

INSERT INTO Materials.MaterialId
    (MaterialNumber, VendorName, SequenceId)
VALUES(@materialNumber, @vendorName, @sequenceId);
COMMIT;

END TRY
BEGIN CATCH
    THROW;
    ROLLBACK;
END CATCH
