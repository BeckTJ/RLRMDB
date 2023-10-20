CREATE TABLE Distillation.ProductRun
(
    RunId INT IDENTITY(1,1) PRIMARY KEY,
    RunNumber INT,
    DrumLotNumber VARCHAR(10) FOREIGN KEY REFERENCES Materials.RawMaterial,
    RawMaterialStartWeight INT,
    RawMaterialEndWeight INT,
    TotalRawMaterialLoaded INT,
    ReadingTime TIME,
    SystemStatus VARCHAR(10),
    VisualVerification BIT,
    CollectRate INT,
    RecieverLevel INT,
    HeelsLevel INT,
    PrefractionLevel INT,
    ReboilerLevel INT,
    EmployeeId CHAR(7) FOREIGN KEY REFERENCES HumanResources.Employee,
    PressureId INT FOREIGN KEY REFERENCES Distillation.SystemPressureAtRunTime,
    TemperatureId INT FOREIGN KEY REFERENCES Distillation.SystemTemperatureAtRunTime,
    ProductId INT FOREIGN KEY REFERENCES Distillation.Production,
    CheckId INT FOREIGN KEY REFERENCES Distillation.PreStartChecks

)