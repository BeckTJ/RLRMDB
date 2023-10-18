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

CREATE TABLE Distillation.AlphabeticDate
(
    MonthNumber INT PRIMARY KEY NOT NULL,
    AlphabeticCode CHAR(1) NOT NULL
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
    TotalRecords INT NOT NULL,
)

CREATE TABLE Materials.MaterialVendor
(
    MaterialNumber INT PRIMARY KEY,
    VendorName VARCHAR(25) FOREIGN KEY REFERENCES Material.Vendor NOT NULL,
    ParentMaterialNumber INT FOREIGN KEY REFERENCES Materials.Material NOT NULL,
    MaterialCode VARCHAR(3) NOT NULL,
    SequenceId INT NOT NULL,
    TotalRecords INT NOT NULL,
    UnitOfIssue VARCHAR(2),
    BatchManaged BIT NOT NULL DEFAULT(0),
)

