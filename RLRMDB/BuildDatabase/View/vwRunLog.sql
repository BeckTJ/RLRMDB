--CREATE OR ALTER VIEW Distillation.RunLog
--AS

SELECT 
Production.ProductLotNumber,
ProductBatchNumber,
ProcessOrder,
ReceiverId,
StartDate
FROM Distillation.Production
--JOIN Distillation.PreStartChecks ON Production.CheckId = PreStartChecks.CheckId
--JOIN Distillation.ProductRun ON Production.ProductId = ProductRun.ProductId

Select * FROM Distillation.PreStartChecks 
