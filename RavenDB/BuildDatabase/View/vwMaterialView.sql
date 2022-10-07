CREATE OR ALTER VIEW Materials.MaterialView
AS
SELECT MaterialNumber.MaterialNumber,
MaterialName,
MaterialNameAbreviation,
Material.ParentMaterialNumber,
PermitNumber,
RawMaterialCode,
ProductCode,
SpecificGravity,
--SequenceIdStart,
--SequenceIdEnd,
UnitOfIssue,
BatchManaged,
IsRawMaterial,5
IsMPPS,
CarbonDrumRequired,
CarbonDrumDaysAllowed,
CarbonDrumWeightAllowed,
Vendor.VendorName
FROM Materials.Material
JOIN Materials.MaterialNumber ON MaterialNumber.ParentMaterialNumber = Material.ParentMaterialNumber
JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
JOIN Vendors.Vendor ON MaterialId.VendorName = Vendor.VendorName
--JOIN Distillation.ProductNumberSequence ON MaterialId.SequenceId = ProductNumberSequence.SequenceId
