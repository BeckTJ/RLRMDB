select MaterialNumber,
    RunStartDate,
    Production.ProductLotNumber, 
    ProductRun.RunNumber,
    ProductBatchNumber, 
    ProcessOrder, 
    DrumLotNumber,
    RawMaterialUsed,
    ReadTime,
    SystemStatus,
    VisualVerification,
    ReboilerLevel,
    PrefractionLevel,
    ReceiverName,
    ReceiverLevel,
    EmployeeId
FROM Distillation.Production
join Distillation.ProductRun on Production.ProductLotNumber = ProductRun.ProductLotNumber
join Distillation.ProductLevels on production.ProductLotNumber = ProductLevels.ProductLotNumber


