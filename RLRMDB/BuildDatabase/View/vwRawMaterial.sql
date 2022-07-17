CREATE OR ALTER VIEW Materials.RawMaterialLog
AS

SELECT DateUsed AS 'Date',
    Vendor.VendorName AS 'Vendor', 
    HumanResources.EmployeeInitials(Employee.EmployeeId)AS 'Operator',
    DrumLotNumber AS 'Drum ID',
    SapBatchNumber AS 'SAP Batch Number', 
    VendorBatch.VendorBatchNumber AS 'Vendor Batch Number', 
    SampleSubmit.SampleSubmitNumber AS 'Sample Number',
    InspectionLotNumber AS 'Lot Number', 
    ContainerNumber AS 'Container Number', 
    CONCAT(DrumWeight,' ',UnitOfIssue) AS 'Weight', 
    HumanResources.EmployeeInitials(SampleSubmit.EmployeeId) AS 'QC Operator',
    ApprovalDate AS 'Approval Date',
    ExperiationDate AS 'Experation Date',
    RejectedDate AS 'Rejected Date'

FROM Materials.RawMaterial
JOIN Materials.MaterialNumber ON RawMaterial.MaterialNumber = MaterialNumber.MaterialNumber
JOIN Materials.Material ON MaterialNumber.NameId = Material.NameId
JOIN Vendors.VendorBatch ON RawMaterial.VendorBatchId = VendorBatch.BatchId
JOIN Vendors.Vendor ON VendorBatch.VendorId = Vendor.VendorId
JOIN QualityControl.SampleSubmit ON RawMaterial.SampleSubmitNumber = SampleSubmit.SampleSubmitNumber
JOIN HumanResources.Employee ON RawMaterial.EmployeeId = Employee.EmployeeId