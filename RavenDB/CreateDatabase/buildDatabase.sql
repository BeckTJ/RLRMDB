use master 
GO

drop database Raven 
go
--Create Database
CREATE DATABASE Raven
GO

USE Raven
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

CREATE TABLE Materials.Material
(
    MaterialNumber INT PRIMARY KEY,
    MaterialName VARCHAR(50) NOT NULL,
    MaterialNameAbreviation VARCHAR(15) NOT NULL,
    PermitNumber VARCHAR(25),
    UnitOfIssue VARCHAR(2) NOT NULL,
    BatchManaged BIT NOT NULL DEFAULT(0),
    MaterialCode VARCHAR(3) NOT NULL,
    SequenceId INT NOT NULL,
    CurrentSequenceId INT NOT NULL,
    TotalRecords INT NOT NULL,
)
GO

CREATE NONCLUSTERED INDEX IX_Material_NameAbreviation
ON Materials.Material(MaterialNameAbreviation ASC)
GO

CREATE TABLE QualityControl.SampleSubmit
(
    SampleId INT PRIMARY KEY IDENTITY(1,1),
    SampleType CHAR(3),
    InspectionLotNumber BIGINT,
    SampleDate DATE NOT NULL,
    Rejected BIT DEFAULT(0) NOT NULL,
    Approved BIT DEFAULT(0) NOT NULL,
    ReviewDate DATE,
    ExperiationDate DATE,
    EmployeeId CHAR(7) FOREIGN KEY REFERENCES HumanResources.Employee
)


CREATE TABLE Materials.MaterialVendor
(
    MaterialNumber INT PRIMARY KEY,
    VendorName VARCHAR(25),
    ParentMaterialNumber INT FOREIGN KEY REFERENCES Materials.Material,
    MaterialCode VARCHAR(3) NOT NULL,
    SequenceId INT NOT NULL,
    CurrentSequenceId INT NOT NULL,
    TotalRecords INT NOT NULL,
    UnitOfIssue VARCHAR(2),
    BatchManaged BIT DEFAULT(0),
    ProcessOrderRequired BIT DEFAULT(0),
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
)

CREATE TABLE Materials.VendorLot
(
    VendorLotNumber VARCHAR(25) PRIMARY KEY,
    SampleId INT FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    Quantity INT,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialVendor
)

CREATE TABLE Distillation.RawMaterial
(
    DrumLotNumber VARCHAR(10) PRIMARY KEY,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialVendor,
    DrumWeight INT,
    SapBatchNumber INT ,
    ContainerNumber CHAR(7),
    InspectionLotNumber NUMERIC,
    SampleId INT FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    VendorLotNumber VARCHAR(25) FOREIGN KEY REFERENCES Materials.VendorLot,
    EmployeeId CHAR(7) FOREIGN KEY REFERENCES HumanResources.Employee
)

CREATE TABLE Distillation.Production
(
    ProductLotNumber VARCHAR(10) PRIMARY KEY,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.Material,
    ProductBatchNumber INT,
    ProcessOrder NUMERIC,
    InspectionLotNumber NUMERIC,
    ReceiverName CHAR(5),
    SampleId INT FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
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
    ProductId VARCHAR(10) FOREIGN KEY REFERENCES Distillation.RawMaterial,
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
