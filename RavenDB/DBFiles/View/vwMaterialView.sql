-- CREATE OR ALTER VIEW Materials.MaterialView
-- AS
SELECT 
MaterialNumber.ParentMaterialNumber,
MaterialNumber.MaterialNumber,
MaterialNameAbreviation,
VendorBatch.VendorName,
MaterialName,
PermitNumber,
MaterialCode,
SpecificGravity,
SequenceId,
UnitOfIssue,
BatchManaged,
IsRawMaterial,
CarbonDrumRequired,
CarbonDrumDaysAllowed,
CarbonDrumWeightAllowed
FROM Materials.Material
Left JOIN Materials.MaterialNumber ON MaterialNumber.ParentMaterialNumber = Material.MaterialNumber
right JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
right JOIN Materials.VendorBatch ON MaterialId.VendorName = VendorBatch.VendorName
ORDER BY MaterialNumber.ParentMaterialNumber