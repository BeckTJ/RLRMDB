select * from Materials.Material where MaterialNumber = 58423
select * from Materials.MaterialNumber where ParentMaterialNumber = 58245
select * from Materials.MaterialId where materialNumber = 32716
select * from Distillation.RawMaterial where MaterialNumber = 32716
UPDATE Materials.VendorBatch
set SampleSubmitNumber = (select SampleSubmitNumber from QualityControl.SampleSubmit
                            where SampleSubmitNumber = 'RAW31102' )
where VendorBatchNumber = '222-761-767'
select * from Materials.VendorBatch where MaterialNumber = 3322187

select * from QualityControl.SampleSubmit where SampleSubmitNumber = 'RAW31102'

select * from QualityControl.SampleRequired where MaterialNumber = 58245

select * from Distillation.RawMaterial