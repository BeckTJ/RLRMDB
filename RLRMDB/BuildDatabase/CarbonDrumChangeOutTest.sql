DELETE Distillation.Production

DECLARE @startDate DATE

SET @startDate = '2022-05-19'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-20'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-22'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-23'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-24'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-25'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-26'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-27'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-28'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-29'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-30'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-05-31'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-06-14'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-06-15'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-06-16'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-06-17'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-06-18'
EXEC Distillation.StartRunLog 45234,1231234,1000123456,'A',@startDate
SET @startDate = '2022-06-19'

select * FROM Distillation.Production
