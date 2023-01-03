--EXEC Distillation.InsertProduct
--select * from QualityControl.SampleSubmit
--Select * from Distillation.Production
select * from Vendors.Vendor
--select materialnumber.materialnumber, VendorName, parentmaterialnumber,SequenceIdStart,RawMaterialCode from materials.MaterialNumber
--join materials.MaterialId on MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
--join Distillation.ProductNumberSequence on MaterialId.SequenceId = ProductNumberSequence.SequenceId
--join Materials.Material on materialnumber.parentmaterialnumber = material.materialnumber
--where ParentMaterialNumber != materialnumber.MaterialNumber and VendorName = 'Reclaim'
--order by parentmaterialNumber

