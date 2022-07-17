CREATE TABLE Distillation.Production
(
    ProductLotNumber VARCHAR(10) PRIMARY KEY,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    ProductionBatchNumber INT,
    ProcessOrder NUMERIC,
    ReceiverId INT FOREIGN KEY REFERENCES Distillation.Receiver,
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    DrumLotNumber VARCHAR(10) FOREIGN KEY REFERENCES Materials.RawMaterial,
    RawMaterialStartWeight INT,
    RawMaterialEndWeight INT,
    TotalRawMaterialLoaded INT,
    KOPotDrained BIT,
    RunNumber INT,
    StartDate DATE,
    SystemStatus VARCHAR(10),
    ReadingTime TIME,
    VisualVerification BIT,
    CollectRate INT,
    RecieverLevel INT,
    HeelsLevel INT,
    HeelsPumped INT,
    PrefractionLevel INT,
    ReboilerLevel INT,
    EmployeeId CHAR(7) FOREIGN KEY REFERENCES HumanResources.Employee,
    PressureId INT FOREIGN KEY REFERENCES Distillation.SystemPressureAtRunTime,
    TemperatureId INT FOREIGN KEY REFERENCES Distillation.SystemTemperatureAtRunTime
)

CREATE NONCLUSTERED INDEX IX_ProductRunLog_ProductionBatchNumber
ON Distillation.Production(ProductionBatchNumber ASC)

CREATE NONCLUSTERED INDEX IX_ProductRunLog_ProcessOrder
ON Distillation.Production(ProcessOrder ASC)
