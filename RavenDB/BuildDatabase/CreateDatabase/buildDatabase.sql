USE master
GO

DROP DATABASE RavenDB
GO

--Create Database
CREATE DATABASE RavenDB
GO

USE RavenDB
GO

--Create Database Schemas
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
CREATE TABLE Materials.UnitOfIssue
(
    UnitOfIssue VARCHAR(3) PRIMARY KEY NOT NULL,
    Nomenclature VARCHAR(25) NOT NULL,
)
CREATE TABLE Engineering.SystemNomenclature(
    Nomenclature VARCHAR(50) PRIMARY KEY NOT NULL
)

CREATE TABLE Engineering.SystemIndicator(
    --IndicatorAbreviation VARCHAR(3),
    --IndicatorNomenclature VARCHAR(25),
    IndicatorType VARCHAR(50) NOT NULL
)

CREATE TABLE Distillation.AlphabeticDate
(
    MonthNumber INT PRIMARY KEY NOT NULL,
    AlphabeticCode CHAR(1) NOT NULL
)
CREATE TABLE Distillation.SystemStatus
(
    StatusCode VARCHAR(2) PRIMARY KEY NOT NULL,
    StatusName VARCHAR(25) NOT NULL
)

CREATE TABLE HumanResources.Employee
(
    EmployeeId CHAR(7) PRIMARY KEY,
    FirstName CHAR(25) NOT NULL,
    LastName CHAR(25) NOT NULL,
)

CREATE TABLE Engineering.Receiver
(
    ReceiverName CHAR(5) PRIMARY KEY NOT NULL
)

CREATE TABLE Materials.Vendor
(
    VendorName VARCHAR(25) PRIMARY KEY NOT NULL,
)

CREATE TABLE Materials.Material
(
    MaterialNumber INT PRIMARY KEY,
    MaterialName VARCHAR(50) NOT NULL,
    MaterialNameAbreviation VARCHAR(15) NOT NULL,
    PermitNumber VARCHAR(25),
    CarbonDrumRequired BIT DEFAULT(0) NOT NULL,
    CarbonDrumDaysAllowed INT,
    CarbonDrumWeightAllowed INT,
    CarbonDrumInstallDate DATE,
    VacuumTrapRequired BIT DEFAULT(0) NOT NULL,
    VacuumTrapInstallDate DATE,
    VacuumTrapDaysAllowed INT,
    SpecificGravity DECIMAL(3,2),
    PrefractionRefluxRatio VARCHAR(5),
    CollectRefluxRatio VARCHAR(5),
    NumberOfRuns INT,
    HeelPumpFrequency INT,
)
GO

CREATE NONCLUSTERED INDEX IX_Material_NameAbreviation
ON Materials.Material(MaterialNameAbreviation ASC)
GO

CREATE TABLE Engineering.SystemReceivers(
    ReceiverId INT PRIMARY KEY IDENTITY(1,1),
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.Material,
    ReceiverName CHAR(5) FOREIGN KEY REFERENCES Engineering.Receiver,
    MaxReceiverLevel INT,
)

CREATE TABLE Engineering.IndicatorSetPoint(
    SystemId INT PRIMARY KEY IDENTITY(1,1),
    IndicatorType VARCHAR(50),
    IsRequired BIT NOT NULL DEFAULT(0),
    MaterialNumber INT NOT NULL FOREIGN KEY REFERENCES Materials.Material,
    Nomenclature VARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Engineering.SystemNomenclature,
    Indicator VARCHAR(25),
    SetPoint DECIMAL(6,2),
    Variance DECIMAL(6,2),
)

CREATE TABLE QualityControl.SampleSubmit
(
    SampleSubmitNumber CHAR(8) PRIMARY KEY,
    InspectionLotNumber BIGINT,
    SampleDate DATE NOT NULL,
    Rejected BIT DEFAULT(0) NOT NULL,
    Approved BIT DEFAULT(0) NOT NULL,
    ReviewDate DATE,
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
    MaterialNumber INT,
    MaterialType VARCHAR(25),
    VLN VARCHAR(25),
    Assay BIT DEFAULT(0),
    Water BIT DEFAULT(0),
    Metals BIT DEFAULT(0),
    Chloride BIT DEFAULT(0),
    Boron BIT DEFAULT(0),
    Phosphorus BIT DEFAULT(0),
    Amps INT DEFAULT(0),
    AmpVolume INT,
    AmpUnitOfIssue VARCHAR(3),
    MetalBubbler INT DEFAULT(0),
    BubblerVolume INT,
    BubblerUnitOfIssue VARCHAR(3),
    AssayBulb INT DEFAULT(0),
    Vials INT DEFAULT(0),
    VialVolume INT,
    VialUnitOfIssue VARCHAR(3),
    Retain INT DEFAULT(0),
    PRIMARY KEY (MaterialNumber,VLN)
)

CREATE TABLE Materials.VendorBatch
(
    VendorBatchNumber VARCHAR(25) PRIMARY KEY,
    VendorName VARCHAR(25) FOREIGN KEY REFERENCES Materials.Vendor,
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    Quantity INT,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber
)

CREATE TABLE Materials.MaterialId
(
    MaterialNumber INT,
    VendorName VARCHAR(25),
    MaterialCode VARCHAR(3),
    SequenceId INT,
    TotalRecords INT,
    PRIMARY KEY (MaterialNumber,VendorName)
)

CREATE TABLE Distillation.RawMaterial
(
    DrumLotNumber VARCHAR(10) PRIMARY KEY,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    DrumWeight INT,
    SapBatchNumber INT ,
    ContainerNumber CHAR(7),
    InspectionLotNumber NUMERIC,
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    VendorBatchNumber VARCHAR(25) FOREIGN KEY REFERENCES Materials.VendorBatch,
    EmployeeId CHAR(7) FOREIGN KEY REFERENCES HumanResources.Employee

)

CREATE TABLE Distillation.Production
(
    ProductLotNumber VARCHAR(10) PRIMARY KEY,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    ProductBatchNumber INT,
    ProcessOrder NUMERIC,
    InspectionLotNumber NUMERIC,
    ReceiverName CHAR(5) FOREIGN KEY REFERENCES Engineering.Receiver,
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
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
    RawMaterialUsed INT,
    RunStartDate DATE,
    ProductLotNumber VARCHAR(10) FOREIGN KEY REFERENCES Distillation.Production,
    EmployeeId CHAR(7) FOREIGN KEY REFERENCES HumanResources.Employee,
)
CREATE TABLE Distillation.ProductLevels
(
    LevelId INT IDENTITY(1,1) PRIMARY KEY,
    ProductLotNumber VARCHAR(10) FOREIGN KEY REFERENCES Distillation.Production,
    RunNumber INT FOREIGN KEY REFERENCES Distillation.ProductRun,
    SystemStatus VARCHAR(2),
    VisualVerification BIT DEFAULT(0),
    ReboilerLevel INT,
    PrefractionLevel INT,
    ReceiverLevel INT,
    ReadTime TIME,
)

CREATE TABLE Distillation.PreStartChecks(
    CheckID INT IDENTITY(1,1) PRIMARY KEY,
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
    RunId INT FOREIGN KEY REFERENCES Distillation.ProductRun,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.Material
)


GO
