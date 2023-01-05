--EXEC Distillation.InsertProduct
--select * from QualityControl.SampleSubmit
--Select * from Distillation.Production
--select materialnumber.materialnumber, VendorName, parentmaterialnumber,SequenceIdStart,RawMaterialCode from materials.MaterialNumber
--join materials.MaterialId on MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
--join Distillation.ProductNumberSequence on MaterialId.SequenceId = ProductNumberSequence.SequenceId
--join Materials.Material on materialnumber.parentmaterialnumber = material.materialnumber
--where ParentMaterialNumber != materialnumber.MaterialNumber and VendorName = 'Reclaim'
--order by parentmaterialNumber

--Select VendorName from materials.materialId where materialnumber = 32409

EXEC Distillation.RawMaterialUpdate 32409, '111-111-111', DEFAULT, 1231234,DEFAULT,DEFAULT,3,DEFAULT 

select * from Distillation.RawMaterial
where MaterialNumber = 32409

-- select Distillation.setDrumId(32716,'Liquor Store')

-- exec Vendors.AddVendorBatch 32409, 'Wine Palace','111-111-111', 10
-- select * from vendors.vendorbatch
