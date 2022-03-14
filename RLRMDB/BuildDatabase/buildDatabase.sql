--DROP DATABASE RLRMDB--
--CREATE DATABASE RLRMDB--

CREATE TABLE vendor
(
    vendorId INT PRIMARY KEY IDENTITY(1,1),
    vendorName VARCHAR(25) NOT NULL,
)

CREATE TABLE vendorBatchInformation
(
    batchId INT PRIMARY KEY IDENTITY(1,1),
    vendorId INT FOREIGN KEY REFERENCES vendor,
    vendorBatchNumber VARCHAR(25),
    quantity INT
)

CREATE TABLE productNumberSequence
(
    sequenceId int PRIMARY KEY,
    sequenceIdStart INT NOT NULL,
    sequenceIdEnd INT NULL
)

CREATE TABLE qualityControl
(
    sampleSubmitNumber CHAR(8) PRIMARY KEY,
    inspectionLotNumber NUMERIC,
    vendorBatchId INT FOREIGN KEY REFERENCES vendorBatchInformation,
    rejected BIT,
    rejectedDate DATE,
    approvalDate DATE,
    experiationDate DATE
)

CREATE TABLE materialName
(
    materialNameId INT PRIMARY KEY IDENTITY(1,1),
    materialName VARCHAR(50),
    materialNameAbreviation VARCHAR(10),
    permitNumber VARCHAR(25),
    rawMaterialCode VARCHAR(3),
    productCode VARCHAR(3),
    carbonDrumRequired BIT,
    carbonDrumDaysAllowed INT,
    carbonDrumWeightAllowed INT
)

CREATE TABLE materialNumber
(
    materialNumber INT PRIMARY KEY,
    materialNameId INT FOREIGN KEY REFERENCES materialName,
    materialGrade CHAR(10),
    batchManaged BIT,
    requiresProcessOrder BIT,
    unitOfIssue CHAR(2),
    isRawMaterial BIT
)
CREATE TABLE materialId
(
    materialId INT PRIMARY KEY IDENTITY(1,1),
    materialNumber INT FOREIGN KEY REFERENCES materialNumber,
    vendorId INT,
    sequenceId INT,
    currentSequenceId INT
)

CREATE TABLE rawMaterial
(
    drumLotNumber CHAR(10) PRIMARY KEY,
    materialNumber INT FOREIGN KEY REFERENCES materialNumber,
    drumWeight INT,
    sapBatchNumber INT,
    containerNumber CHAR(7),
    sampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES qualityControl,
    processOrder NUMERIC,
    vendorId INT FOREIGN KEY REFERENCES vendor,
    vendorBatchId INT FOREIGN KEY REFERENCES vendorBatchInformation,
    dateUsed DATE NOT NULL
)

CREATE TABLE product
(
    productLotNumber CHAR(10) PRIMARY KEY,
    materialNumber INT FOREIGN KEY REFERENCES materialNumber,
    productionBatchNumber INT,
    processOrder NUMERIC,
    receiverId INT,
    sampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES qualityControl,
    quantity INT
)

CREATE TABLE distilation
(
    productId INT PRIMARY KEY IDENTITY(1,1),
    productLotNumber CHAR(10) FOREIGN KEY REFERENCES product,
    drumLotNumber CHAR(10) FOREIGN KEY REFERENCES rawMaterial,
    rawMaterialLoaded INT,
    prefraction INT,
    heels INT,
    heelsPumped BIT,
    runNumber INT,
    --carbonDrumUsed BIT,
    --carbonDrumDaysAllowed INT,
    --carbonDrumInstallationDate DATE,
    --carbonDrumDaysToChangeOut INT,
    startDate DATETIME,
    endDate DATETIME
)

CREATE TABLE alphabeticDate
(
    monthNumber INT PRIMARY KEY,
    alphabeticCode CHAR(1)
)

CREATE TABLE employee
(
    employeeId INT PRIMARY KEY,
    firstName CHAR(25),
    lastName CHAR(25)
)

CREATE TABLE receiver
(
    receiverId INT PRIMARY KEY,
    receiverName CHAR(5)
)
