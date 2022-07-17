CREATE OR ALTER VIEW Materials.MaterialView
AS
SELECT MaterialNumber.MaterialNumber,
MaterialName,
MaterialNameAbreviation,
PermitNumber,
RawMaterialCode,
ProductCode,
SpecificGravity,
SequenceIdStart,
SequenceIdEnd,
UnitOfIssue,
BatchManaged,
IsRawMaterial,5
IsMPPS,
CarbonDrumRequired,
CarbonDrumDaysAllowed,
CarbonDrumWeightAllowed,
VendorName
FROM Materials.MaterialNumber
JOIN Materials.Material ON MaterialNumber.NameId = Material.NameId
JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
JOIN Vendors.Vendor ON MaterialId.VendorId = Vendor.VendorId
JOIN Distillation.ProductNumberSequence ON MaterialId.SequenceId = ProductNumberSequence.SequenceId