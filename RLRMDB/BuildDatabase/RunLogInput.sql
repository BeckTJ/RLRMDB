--DELETE Distillation.Production
--select * From Materials.Material Where CarbonDrumRequired = 1
DECLARE @startDate DATE
SET @startDate = '2022-05-30'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate


--SET @startDate = GETDATE()
--EXEC Distillation.StartRunLog 45333,1232345,100023456,'C-402',@startDate

select * FROM Distillation.Production

