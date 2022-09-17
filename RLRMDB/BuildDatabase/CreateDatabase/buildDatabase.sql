--Drop Database
USE master
GO

DROP DATABASE RLRMDB
GO

--Create Database
CREATE DATABASE RLRMDB
GO

USE RLRMDB
GO

--Create Database Schemas
CREATE SCHEMA  Vendors
GO
CREATE SCHEMA Materials
GO
CREATE SCHEMA Distillation
GO
CREATE SCHEMA QualityControl
GO
CREATE SCHEMA HumanResources
GO
CREATE SCHEMA Engineering
GO

--Create tabels 
CREATE TABLE Engineering.SystemNomenclature(
    Nomenclature VARCHAR(50) PRIMARY KEY
)

CREATE TABLE Engineering.Indicator(
    IndicatorAbreviation VARCHAR(3),
    IndicatorType VARCHAR(25)
)

CREATE TABLE Distillation.AlphabeticDate
(
    MonthNumber INT PRIMARY KEY,
    AlphabeticCode CHAR(1) NOT NULL
)

CREATE TABLE HumanResources.Employee
(
    EmployeeId CHAR(7) PRIMARY KEY,
    FirstName CHAR(25) NOT NULL,
    LastName CHAR(25) NOT NULL
)

CREATE TABLE Distillation.Receiver
(
    ReceiverId INT PRIMARY KEY IDENTITY(1,1),
    ReceiverName CHAR(5) NOT NULL
)

CREATE TABLE Vendors.Vendor
(
    VendorName VARCHAR(25) PRIMARY KEY,
    IsMPPS BIT NOT NULL DEFAULT(0)
)

CREATE TABLE Distillation.ProductNumberSequence
(
    SequenceId int PRIMARY KEY IDENTITY(1,1),
    SequenceIdStart INT NOT NULL,
    SequenceIdEnd INT NOT NULL
)

CREATE TABLE Materials.Material
(
    ParentMaterialNumber INT PRIMARY KEY,
    MaterialName VARCHAR(50) NOT NULL,
    MaterialNameAbreviation VARCHAR(15),
    PermitNumber VARCHAR(25),
    RawMaterialCode VARCHAR(3),
    ProductCode VARCHAR(3),
    CarbonDrumRequired BIT DEFAULT(0) NOT NULL,
    CarbonDrumDaysAllowed INT,
    CarbonDrumWeightAllowed INT,
    CarbonDrumInstallDate DATE,
    SpecificGravity DECIMAL(3,2),
    PrefractionRefluxRatio VARCHAR(5),
    CollectRefluxRatio VARCHAR(5),
    NumberOfRuns INT,
    --SystemId INT FOREIGN KEY REFERENCES SystemPressureSetPoint
)
GO

CREATE NONCLUSTERED INDEX IX_Material_NameAbreviation
ON Materials.Material(MaterialNameAbreviation ASC)
GO

CREATE TABLE Engineering.IndicatorSetPoints(
    SystemId INT PRIMARY KEY IDENTITY,
    ParentMaterialNumber INT FOREIGN KEY REFERENCES Materials.Material,
    Nomenclature VARCHAR(50) FOREIGN KEY REFERENCES Engineering.SystemNomenclature,
    Indicator VARCHAR(10),
    SetPointLow DECIMAL(6,3),
    SetPointHigh DECIMAL(6,3),
    Variance DECIMAL(6,3),
)

CREATE TABLE QualityControl.SampleSubmit
(
    SampleSubmitNumber CHAR(8) PRIMARY KEY,
    InspectionLotNumber BIGINT,
    Rejected BIT,
    RejectedDate DATE,
    ApprovalDate DATE,
    ExperiationDate DATE,
    EmployeeId CHAR(7) FOREIGN KEY REFERENCES HumanResources.Employee
)

CREATE TABLE Materials.MaterialNumber
(
    MaterialNumber INT PRIMARY KEY,
    ParentMaterialNumber INT FOREIGN KEY REFERENCES Materials.Material NOT NULL,
    BatchManaged BIT NOT NULL DEFAULT(0),
    RequiresProcessOrder BIT NOT NULL DEFAULT(0),
    UnitOfIssue VARCHAR(2),
    IsRawMaterial BIT NOT NULL DEFAULT(0)
)

CREATE TABLE QualityControl.SampleRequired
(
    RequiredId INT PRIMARY KEY IDENTITY(1,1),
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    AmpRequired BIT NOT NULL DEFAULT(0),
    NumberOfAmps INT DEFAULT(0),
    MetalsRequired BIT NOT NULL DEFAULT(0),
    NumberOfMetals INT DEFAULT(0),
    RetainRequired BIT NOT NULL DEFAULT(0)
)

CREATE TABLE Vendors.VendorBatch
(
    VendorBatchNumber VARCHAR(25) PRIMARY KEY,
    VendorName VARCHAR(25) FOREIGN KEY REFERENCES Vendors.Vendor,
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    Quantity INT,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber
)

CREATE TABLE Materials.MaterialId
(
    MaterialNumber INT,
    VendorName VARCHAR(25),
    SequenceId INT,
    CurrentSequenceId INT
    PRIMARY KEY (MaterialNumber,VendorName)
)

CREATE TABLE Distillation.RawMaterial
(
    DrumLotNumber VARCHAR(10) PRIMARY KEY,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    DrumWeight INT,
    SapBatchNumber INT,
    ContainerNumber CHAR(7),
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    VendorBatchNumber VARCHAR(25) FOREIGN KEY REFERENCES Vendors.VendorBatch,
    DateUsed DATE NOT NULL,
    EmployeeId CHAR(7) FOREIGN KEY REFERENCES HumanResources.Employee

)

CREATE TABLE Distillation.PreStartChecks(
    CheckID INT IDENTITY(1,1) PRIMARY KEY,
    VacuumTrapInstallDate DATE,
    ReboilerSkinTempBelowValue BIT,
    KnockOutPotDrained BIT,
    HeelsPumped BIT,
    HeliumCylinderPSI INT,
    HeliumFlowPSI INT,
    CoolantLevel BIT,
    CoolantPurgeSet BIT,
    NitrogenFlowRate INT,
    NitrogenPurge INT,
    HeatingMantlePurgeSet INT,
    NitrogenFlow INT,
    AftercoolerPressure INT,
    ChillerSetting INT,
    NitrogenToCondenserPurge INT,
    SecondaryPurgeSet BIT,
    InspectLines BIT,
    ControllerInitialSetBelowValue BIT,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber
)

CREATE TABLE Distillation.Production
(
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductLotNumber VARCHAR(10),
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    ProductBatchNumber INT,
    ProcessOrder NUMERIC,
    ReceiverId INT FOREIGN KEY REFERENCES Distillation.Receiver,
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    StartDate DATE,
)
CREATE NONCLUSTERED INDEX IX_Production_ProductLotNumber
ON Distillation.Production(ProductLotNumber ASC)

CREATE NONCLUSTERED INDEX IX_Production_ProductionBatchNumber
ON Distillation.Production(ProductBatchNumber ASC)

CREATE NONCLUSTERED INDEX IX_Production_ProcessOrder
ON Distillation.Production(ProcessOrder ASC)

CREATE TABLE Distillation.ProductRun
(
    RunId INT IDENTITY(1,1) PRIMARY KEY,
    RunNumber INT,
    DrumLotNumber VARCHAR(10) FOREIGN KEY REFERENCES Distillation.RawMaterial,
    RawMaterialStartWeight INT,
    RawMaterialEndWeight INT,
    TotalRawMaterialLoaded INT,
    KOPotDrained BIT,
    ReadingTime TIME,
    SystemStatus VARCHAR(10),
    VisualVerification BIT,
    CollectRate INT,
    RecieverLevel INT,
    HeelsLevel INT,
    HeelsPumped BIT,
    PrefractionLevel INT,
    ReboilerLevel INT,
    EmployeeId CHAR(7) FOREIGN KEY REFERENCES HumanResources.Employee,
    --PressureId INT FOREIGN KEY REFERENCES Distillation.PressureAtRunTime,
    --TemperatureId INT FOREIGN KEY REFERENCES Distillation.TemperatureAtRunTime,
    --ProductId INT FOREIGN KEY REFERENCES Distillation.Production,
    --CheckID INT FOREIGN KEY REFERENCES Distillation.PreStartChecks
)
GO
