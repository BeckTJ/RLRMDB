-- -- select * from Materials.Material where MaterialNumber = 58423
-- -- select * from Materials.MaterialNumber where ParentMaterialNumber = 58245
-- -- select * from Materials.MaterialId where materialNumber = 32716
-- -- select * from Distillation.RawMaterial where MaterialNumber = 32716
-- -- UPDATE Materials.VendorBatch
-- -- set SampleSubmitNumber = (select SampleSubmitNumber from QualityControl.SampleSubmit
-- --                             where SampleSubmitNumber = 'RAW31102' )
-- -- where VendorBatchNumber = '222-761-767'
-- -- select * from Materials.VendorBatch where VendorBatchNumber = '599-742-163'

-- -- select * from QualityControl.SampleSubmit where SampleSubmitNumber = 'RAW31102'

-- -- select * from QualityControl.SampleRequired where MaterialNumber = 58245

-- -- select * from Distillation.Production
-- -- where materialnumber = 58423

-- -- select * from Engineering.SystemReceivers

-- -- select * from Materials.MaterialNumber
-- -- where ParentMaterialNumber = 58423 

-- select * from Materials.VendorBatch
-- join Materials.MaterialNumber on MaterialNumber.MaterialNumber = VendorBatch.MaterialNumber
--  where MaterialNumber.ParentMaterialNumber = 58423

-- select * from Distillation.RawMaterial
-- left join Materials.MaterialNumber on RawMaterial.MaterialNumber = MaterialNumber.MaterialNumber
-- where MaterialNumber.ParentMaterialNumber = 58423

-- select * from QualityControl.SampleSubmit
-- join Distillation.RawMaterial on RawMaterial.SampleSubmitNumber = SampleSubmit.SampleSubmitNumber
-- join Materials.MaterialNumber on MaterialNumber.MaterialNumber = RawMaterial.MaterialNumber
-- where MaterialNumber.ParentMaterialNumber = 58423

-- -- Material Number = 3322187
-- update QualityControl.SampleSubmit
-- set Rejected = 0
-- where SampleSubmit.SampleSubmitNumber = 'RAW78245' or SampleSubmit.SampleSubmitNumber = 'RAW61560'
-- -- RAW78245 RAW61560

-- select * from Materials.VendorBatch
-- join Materials.MaterialNumber on MaterialNumber.MaterialNumber = VendorBatch.MaterialNumber
-- where ParentMaterialNumber = 58423

-- select * from Distillation.RawMaterial
-- where SampleSubmitNumber = 'RAW61560'

select * from Materials.VendorBatch
join Distillation.RawMaterial on RawMaterial.VendorBatchNumber = VendorBatch.VendorBatchNumber
where VendorBatch.MaterialNumber = 3322187


select VendorBatch.MaterialNumber,VendorName, VendorBatchNumber from Materials.VendorBatch
join Materials.MaterialNumber on VendorBatch.MaterialNumber = MaterialNumber.MaterialNumber
where MaterialNumber.ParentMaterialNumber = 58423
GROUP BY VendorBatch.MaterialNumber

 