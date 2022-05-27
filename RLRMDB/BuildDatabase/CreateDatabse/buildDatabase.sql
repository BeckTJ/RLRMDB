--Drop Database
USE master
GO

DROP DATABASE RLRMDB
GO

--Create Database
CREATE DATABASE RLRMDB;
GO

USE RLRMDB;
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
CREATE SCHEMA HumanResource
GO

--Create tabels 
CREATE TABLE Distillation.AlphabeticDate
(
    MonthNumber INT PRIMARY KEY,
    AlphabeticCode CHAR(1) NOT NULL
)
GO
INSERT INTO Distillation.AlphabeticDate
    (MonthNumber, AlphabeticCode)
VALUES
    (10, 'A'),
    (11, 'B'),
    (12, 'C'),
    (1, 'D'),
    (2, 'E'),
    (3, 'F'),
    (4, 'G'),
    (5, 'H'),
    (6, 'I'),
    (7, 'J'),
    (8, 'K'),
    (9, 'L')
GO

CREATE TABLE HumanResource.Employee
(
    EmployeeId INT PRIMARY KEY,
    FirstName CHAR(25) NOT NULL,
    LastName CHAR(25) NOT NULL
)
GO
INSERT into HumanResource.Employee (EmployeeId, FirstName, LastName)
    VALUES  (1, 'john', 'smith'),
            (2, 'jane', 'smith'),
            (3, 'brian', 'squire')
GO

CREATE TABLE Distillation.Receiver
(
    ReceiverId INT PRIMARY KEY IDENTITY(1,1),
    ReceiverName CHAR(5) NOT NULL
)
GO
insert into Distillation.Receiver
        (ReceiverName)
values
        ('A'),
        ('B'),
        ('C'),
        ('D'),
        ('E'),
        ('F'),
        ('C-201'),
        ('C-202'),
        ('C-203'),
        ('C-204'),
        ('C-401'),
        ('C-402'),
        ('C-403')
GO

CREATE TABLE Vendors.Vendor
(
    VendorId INT PRIMARY KEY IDENTITY(1,1),
    VendorName VARCHAR(25) NOT NULL,
    IsMPPS BIT NOT NULL DEFAULT(0)
)
GO
insert into Vendors.Vendor
        (VendorName,IsMPPS)
values
        ('Axiall',0),
        ('Berje',0),
        ('Symerise',0),
        ('Evonik',0),
        ('Silabond',0),
        ('Sivance',0),
        ('Wacker',0),
        ('Boulder',0),
        ('SAFC',0),
        ('Anderson',0),
        ('Hacros',0),
        ('Eastman',0),
        ('Heels and Prefraction',1),
        ('Prefraction',1),
        ('Reclaim',1),
        ('Treated',0),
        ('SVM',1),
        ('Ascensus',0),
        ('Solvay',0),
        ('Millipore Sigma',0),
        ('Column Drain',1),
        ('Moelhausen',0),
        ('Supelco',0),
        ('ATI',0)
GO

CREATE TABLE Distillation.ProductNumberSequence
(
    SequenceId int PRIMARY KEY IDENTITY(1,1),
    SequenceIdStart INT NOT NULL,
    SequenceIdEnd INT NOT NULL
)
GO
INSERT into Distillation.ProductNumberSequence
    (SequenceIdStart, SequenceIdEnd)
VALUES(001, 099),
    (100, 199),
    (200, 299),
    (300, 399),
    (400, 499),
    (500, 599),
    (600, 699),
    (700, 799),
    (800, 899),
    (900, 999),
    (1000, 1999),
    (2000, 2999),
    (3000, 3999),
    (4000, 4999),
    (5000, 5999),
    (6000, 6999),
    (7000, 7999),
    (8000, 8999),
    (9000, 9999)
GO

CREATE TABLE Materials.Material
(
    NameId INT PRIMARY KEY IDENTITY(1,1),
    MaterialName VARCHAR(50) NOT NULL,
    MaterialNameAbreviation VARCHAR(15),
    PermitNumber VARCHAR(25),
    RawMaterialCode VARCHAR(3),
    ProductCode VARCHAR(3),
    CarbonDrumRequired BIT DEFAULT(0) NOT NULL,
    CarbonDrumDaysAllowed INT,
    CarbonDrumWeightAllowed INT
)
GO
INSERT INTO Materials.Material
    (MaterialName, MaterialNameAbreviation, PermitNumber, RawMaterialCode, ProductCode, CarbonDrumRequired, CarbonDrumDaysAllowed, CarbonDrumWeightAllowed)
VALUES
    ('Alpha-Terpinene', 'ATRP', 'APCD2010-PTO-000602', 'TC', 'TP', 1, NULL, 18),
    ('Bis(tert-butylamino)silane', 'BTBAS SAFC', NULL, 'SD', 'SA', 0, NULL, NULL),
    ('Diethoxymethylsilane', 'DEMS', 'APCD2011-PTO-000926', 'SR', 'SE', 0, NULL, NULL),
    ('Hexane', 'Hexane', NULL, NULL, NULL, 0, NULL, NULL),
    ('Hafnium Tetrachloride', 'HFCL', '986660', 'HR', 'HC', 0, NULL, NULL),
    ('Isopropanol', 'IPA', 'APCD2010-PTO-000602', NULL, NULL, 0, NULL, NULL),
    ('Tetrakis(dimethylamino)titanium', 'TDMAT', 'APCD2008-PTO-976853', 'WR', 'WT', 0, NULL, NULL),
    ('Triethylborate', 'TEB', 'APCD2002-PTO-870666', 'UR', 'UE', 1, 265, NULL),
    ('Tetraethyl Orthosilicate', 'TEOS', 'APCD2009-PTO-950939', 'ER', 'EX', 1, 16, NULL),
    ('Triethyl Phosphate', 'TEPO', 'APCD1997-PTO-950407', 'PD', 'PT', 0, NULL, NULL),
    ('Trimethylborate', 'TMB', 'APCD2002-PTO-870666', 'TR', 'TX', 1, 265, NULL),
    ('Trimethyl Phosphite', 'TMPI', 'APCD1997-PTO-950407', 'IR', 'IT', 0, NULL, NULL),
    ('Trimethyl Phosphate', 'TMPO', 'APCD1997-PTO-950407', 'OR', 'OT', 0, NULL, NULL),
    ('Tetramethylcyclotetrasiloxane', 'TOMCATS', NULL, NULL, 'CT', 0, NULL, NULL),
    ('Trans-dichloroethylene', 'TRANS', 'APCD1999-PTO-940529', 'DR', 'DX', 1, 33, NULL),
    ('Bis(tert-butylamino)silane', 'BTBAS BANWOL', NULL, 'SD', 'SA', 0, NULL, NULL),
    ('Bis(tert-butylamino)silane', 'BTBAS 2NTE', NULL, NULL, 'SD', 0, NULL, NULL)
GO
CREATE TABLE QualityControl.SampleSubmit
(
    SampleSubmitNumber CHAR(8) PRIMARY KEY,
    InspectionLotNumber NUMERIC,
    Rejected BIT,
    RejectedDate DATE,
    ApprovalDate DATE,
    ExperiationDate DATE
)

CREATE TABLE Materials.MaterialNumber
(
    MaterialNumber INT PRIMARY KEY,
    NameId INT FOREIGN KEY REFERENCES Materials.Material NOT NULL,
    BatchManaged BIT NOT NULL DEFAULT(0),
    RequiresProcessOrder BIT NOT NULL DEFAULT(0),
    UnitOfIssue CHAR(2),
    IsRawMaterial BIT NOT NULL DEFAULT(0)
)
GO
INSERT INTO Materials.MaterialNumber
	(MaterialNumber, NameId, IsRawMaterial, BatchManaged, RequiresProcessOrder, UnitOfIssue)
VALUES
	(37705, (select NameId From Materials.Material where MaterialNameAbreviation ='TDMAT'), 0, 1, 0, 'kg'),
	(45230, (select NameId From Materials.Material where MaterialNameAbreviation ='TEPO'), 0, 1, 0, 'kg'),
	(45231, (select NameId From Materials.Material where MaterialNameAbreviation ='TEPO'), 1, 0, 0, 'kg'),
	(45234, (select NameId From Materials.Material where MaterialNameAbreviation ='TEOS'), 0, 0, 0, 'kg'),
	(45235, (select NameId From Materials.Material where MaterialNameAbreviation ='TEOS'), 1, 0, 0, 'kg'),
	(45255, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS SAFC'), 0, 1, 0, 'kg'),
	(45256, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS SAFC'), 0, 1, 0, 'kg'),
	(45260, (select NameId From Materials.Material where MaterialNameAbreviation ='TMB'), 0, 0, 0, 'kg'),
	(45261, (select NameId From Materials.Material where MaterialNameAbreviation ='TMB'), 1, 0, 0, 'kg'),
	(45262, (select NameId From Materials.Material where MaterialNameAbreviation ='TMPI'), 0, 0, 0, 'kg'),
	(45264, (select NameId From Materials.Material where MaterialNameAbreviation ='TMPI'), 1, 0, 0, 'kg'),
	(45265, (select NameId From Materials.Material where MaterialNameAbreviation ='TMPO'), 0, 0, 0, 'kg'),
	(45266, (select NameId From Materials.Material where MaterialNameAbreviation ='TMPO'), 1, 0, 0, 'kg'),
	(45267, (select NameId From Materials.Material where MaterialNameAbreviation ='TOMCATS'), 1, 0, 0, 'kg'),
	(45269, (select NameId From Materials.Material where MaterialNameAbreviation ='TRANS'), 0, 0, 0, 'kg'),
	(45270, (select NameId From Materials.Material where MaterialNameAbreviation ='TRANS'), 1, 0, 0, 'kg'),
	(45275, (select NameId From Materials.Material where MaterialNameAbreviation ='TEB'), 0, 0, 0, 'kg'),
	(45276, (select NameId From Materials.Material where MaterialNameAbreviation ='TEB'), 1, 0, 0, 'kg'),
	(45320, (select NameId From Materials.Material where MaterialNameAbreviation ='ATRP'), 1, 0, 0, 'kg'),
	(45329, (select NameId From Materials.Material where MaterialNameAbreviation ='ATRP'), 0, 0, 0, 'kg'),
	(45333, (select NameId From Materials.Material where MaterialNameAbreviation ='DEMS'), 0, 1, 0, 'kg'),
	(45337, (select NameId From Materials.Material where MaterialNameAbreviation ='Hexane'), 1, 0, 0, 'kg'),
	(176164, (select NameId From Materials.Material where MaterialNameAbreviation ='IPA'), 1, 0, 0, 'kg'),
	(186924, (select NameId From Materials.Material where MaterialNameAbreviation ='HFCL'), 1, 0, 0, 'g'),
	(186997, (select NameId From Materials.Material where MaterialNameAbreviation ='HFCL'), 0, 0, 0, 'g'),
	(444807, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS BANWOL'), 1, 1, 0, 'kg'),
	(444808, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS BANWOL'), 0, 1, 0, 'kg'),
	(456324, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS 2NTE'), 1, 1, 0, 'kg'),
	(475743, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS SAFC'), 1, 1, 0, 'kg'),
	(475744, (select NameId From Materials.Material where MaterialNameAbreviation ='TDMAT'), 1, 1, 0, 'kg'),
	(2302328, (select NameId From Materials.Material where MaterialNameAbreviation ='DEMS'), 1, 1, 0, 'kg'),
	(2308223, (select NameId From Materials.Material where MaterialNameAbreviation ='ATRP'), 1, 0, 0, 'kg'),
	(2304780, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS SAFC'), 1, 1, 1, 'kg'),
	(2304783, (select NameId From Materials.Material where MaterialNameAbreviation ='BTBAS SAFC'), 1, 1, 0, 'kg'),
	(2305864, (select NameId From Materials.Material where MaterialNameAbreviation ='DEMS'), 1, 1, 1, 'kg'),
	(2305935, (select NameId From Materials.Material where MaterialNameAbreviation ='TEOS'), 1, 0, 0, 'kg'),
	(2306209, (select NameId From Materials.Material where MaterialNameAbreviation ='TRANS'), 1, 0, 1, 'kg'),
	(2306574, (select NameId From Materials.Material where MaterialNameAbreviation ='TDMAT'), 1, 1, 1, 'kg'),
	(2308221, (select NameId From Materials.Material where MaterialNameAbreviation ='ATRP'), 1, 0, 1, 'kg'),
	(2308222, (select NameId From Materials.Material where MaterialNameAbreviation ='ATRP'), 1, 0, 1, 'kg'),
	(2308339, (select NameId From Materials.Material where MaterialNameAbreviation ='TEPO'), 1, 1, 0, 'kg')
GO
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
    BatchId INT PRIMARY KEY IDENTITY(1,1),
    VendorId INT FOREIGN KEY REFERENCES Vendors.Vendor,
    VendorBatchNumber VARCHAR(25),
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    Quantity INT,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber
)

CREATE TABLE Materials.MaterialId
(
    MaterialId INT PRIMARY KEY IDENTITY(1,1),
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    VendorId INT,
    SequenceId INT,
    CurrentSequenceId INT
)
GO
INSERT INTO Materials.MaterialId
	(MaterialNumber, VendorId, CurrentSequenceId)
VALUES
	(2308223, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Symerise'), 1000),
	(45320, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Berje'), 3000),
	(45329, 0, 600),
	(2308221, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 8000),
	(2308222, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Column Drain'),  9000),
	(2304780, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 1),
	(2304780, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Heels and Prefraction'), 1),
	(456324, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'SAFC'), NULL),
	(444807, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'SAFC'), 200),
	(444808, 0, 400),
	(475743, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 700),
	(45255, 0, 300),
	(45256, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 100),
	(2304783, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'SVM'), 500),
	(45333, 0, 700),
	(2302328, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Wacker'), 900),
	(2302328, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Sivance'), 800),
	(2305864, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Heels and Prefraction'), 2000),
	(45337, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'SAFC'), 100),
	(186924, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'ATI') , 100),
	(186997, 0, 700),
	(37705, 0, 900),
	(475744, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Boulder'), 600),
	(475744, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Evonik'), 700),
	(2306574, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 200),
	(45275, 0, 200),
	(45276, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Anderson'), 200),
	(45234, 0, 500),
	(45234, 0, 700),
	(45235, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Silabond'), 2000),
	(45235, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Wacker'), 3000),
	(2305935, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 4000),
	(45230, 0, 200),
	(45231, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Heels and Prefraction'), 600),
	(2308339, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Treated'), 700),
	(45260, 0, 100),
	(45261, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Solvay'), 300),
	(45262, 0, 200),
	(45264, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Hacros'), 400),
	(45264, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Solvay'), 300),
	(45265, 0, 200),
	(45266, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'SAFC'), 500),
	(45267, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Sivance'), 400),
	(45269, 0, 600),
	(45270, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Axiall'), 1000),
	(2306209, (SELECT VendorId FROM Vendors.Vendor WHERE VendorName = 'Reclaim'), 500)
GO
UPDATE Materials.MaterialId
SET SequenceId = (SELECT SequenceId 
                    FROM Distillation.ProductNumberSequence
                    WHERE SequenceIdStart = CurrentSequenceId)
GO

CREATE TABLE Materials.RawMaterialLog
(
    DrumLotNumber VARCHAR(10) PRIMARY KEY,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    DrumWeight INT,
    SapBatchNumber INT,
    ContainerNumber CHAR(7),
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    VendorBatchId INT FOREIGN KEY REFERENCES Vendors.VendorBatch,
    DateUsed DATE NOT NULL
)

CREATE TABLE Distillation.Product
(
    ProductLotNumber VARCHAR(10) PRIMARY KEY,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    ProductionBatchNumber INT,
    ProcessOrder NUMERIC,
    ReceiverId INT FOREIGN KEY REFERENCES Distillation.Receiver,
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    Quantity INT
)

CREATE TABLE Distillation.UsageLog
(
    ProductId INT PRIMARY KEY IDENTITY(1,1),
    ProductLotNumber VARCHAR(10) FOREIGN KEY REFERENCES Distillation.Product,
    DrumLotNumber VARCHAR(10) FOREIGN KEY REFERENCES Materials.RawMaterialLog,
    HeelsPumped BIT,
    RunNumber INT,
    StartDate DATETIME,
    EndDate DATETIME
)
