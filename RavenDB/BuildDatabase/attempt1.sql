SELECT * FROM Distillation.RawMaterial
JOIN Materials.MaterialId ON RawMaterial.MaterialNumber = MaterialId.MaterialNumber
JOIN Materials.MaterialNumber ON MaterialId.MaterialNumber = MaterialNumber.MaterialNumber
JOIN QualityControl.SampleSubmit ON RawMaterial.SampleSubmitNumber = SampleSubmit.SampleSubmitNumber
JOIN Vendors.Vendor ON MaterialId.VendorName = Vendor.VendorName
--JOIN Vendors.VendorBatch ON RawMaterial.VendorBatchNumber = VendorBatch.VendorBatchNumber


