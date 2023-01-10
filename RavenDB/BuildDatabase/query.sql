-- select Distillation.SetProductId(58245,'Finish Product')

-- SELECT SequenceIdStart FROM Distillation.ProductNumberSequence 
--                             JOIN Materials.MaterialId ON ProductNumberSequence.SequenceId = MaterialId.SequenceId
--                             WHERE MaterialId.MaterialNumber = 58245 AND MaterialId.VendorName = 'Finish Product'
EXEC Distillation.UpdateProduction 58245, 3211234, 999123123, 9999999999, NULL

DECLaRE @sampleDate DATE
SET @sampleDate = '2022-05-03'

EXEC QualityControl.SubmitSample 'LOT222222', 9999999999, @sampleDate 
-- DROP TRIGGER Distillation.UpdateProductLotNumber
-- SELECT Distillation.UpdateProductId('100DB', '2001-05-03')
select * from Distillation.Production
select * from QualityControl.SampleSubmit