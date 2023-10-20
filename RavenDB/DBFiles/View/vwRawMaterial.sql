CREATE OR ALTER VIEW Distillation.RawMaterialLog
AS

SELECT SampleSubmit.SampleDate AS 'Date',
    Vendor.VendorName AS 'Vendor', 
    DrumLotNumber AS 'Drum ID',
    SapBatchNumber AS 'SAP Batch Number', 
    -- VendorBatch.VendorBatchNumber AS 'Vendor Batch Number', 
    SampleSubmit.SampleSubmitNumber AS 'Sample Number',
    InspectionLotNumber AS 'Lot Number', 
    ContainerNumber AS 'Container Number', 
    CONCAT(DrumWeight,' ',UnitOfIssue) AS 'Weight', 
    ApprovalDate AS 'Approval Date',
    ExperiationDate AS 'Experation Date',
    RejectedDate AS 'Rejected Date'

FROM Distillation.RawMaterial
JOIN Materials.MaterialId ON RawMaterial.MaterialNumber = MaterialId.MaterialNumber
JOIN Materials.MaterialNumber ON MaterialId.MaterialNumber = MaterialNumber.MaterialNumber
JOIN Vendors.VendorBatch ON RawMaterial.VendorBatchNumber = VendorBatch.VendorBatchNumber
JOIN Vendors.Vendor ON MaterialId.VendorName = Vendor.VendorName
JOIN QualityControl.SampleSubmit ON RawMaterial.SampleSubmitNumber = SampleSubmit.SampleSubmitNumber
