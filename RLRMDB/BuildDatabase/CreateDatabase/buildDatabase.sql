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

--Create tabels 
--CREATE TABLE Distillation.SystemTemperatureAtRunTime(
--    TemperatureId INT PRIMARY KEY,
--    ReboilerZone1 INT,
--    ReboilerZone2 INT,
--    ReboilerZone3 INT,
--    ReboilerZone4 INT,
--    ReboilerZone5 INT,
--    ReboilerZone6 INT,
--    ColumnAverage INT,
--    InternalFluidTemp INT,
--    ColumnBottom INT,
--    ColumnMiddle INT,
--    ColumnTop INT,
--    RefluxSplitter INT,
--    CondenserTop INT,
--    CondenserCoolantOutlet INT,
--    AftercoolerCoolantOutlet INT,
--    CondeserSkin INT,
--    CondenserInlet INT,
--    AftercoolerInlet INT,
--    LN2 INT,
--)

--Create TABLE Distillation.SystemPressureSetPoint(
--    SystemId INT PRIMARY KEY,
--    SystemPressure INT,
--    SystemDifferentialPressure INT,
--    ReboilerPressure INT,
--    PrefractionFlaskPressure INT,
--    ReceiverPressure INT,
--    ChillerRecircPressure INT,
--    NitrogenBleedRate INT ,
--    CondenserCoolantFlowRate INT,
--    AftercoolerCoolantFlowRate INT,
--    CondenserCoolantPressure INT,
--    AftercoolerCoolantPressure INT,
--    HighPurityVentPurge INT,
--    ColumnDPLevelSensePurge INT,
--    ReboilerLevelSensePurge INT,
--    RecieverPurge INT,
--    ContainmentPurge INT,
--    ReboilerPurge INT ,
--    PrefractionLevelSencePurge INT ,
--    SystemPurge INT,
--    N2ToCondenserCoolantTank INT,
--    HeatedTankPurge INT,
--    CondenserPurge INT,
--    ReboilerContainmentPressure INT,
--    FeedDrumVentPurge INT,
--    WasteDrumVentPurge INT,
--    VentHeaderPurge INT,
--    ReliefHeaderVentPurge INT,
--    ParticleCounterContainmentPressure INT,
--    VacuumBreakLinePurge INT,
--    VacuumPumpCasePurge INT
--)

--CREATE TABLE Distillation.SystemTemperatureSetPoint(
--    TemperatureSetPointId INT,
--    ReboilerZone1 INT,
--    ReboilerZone2 INT,
--    ReboilerZone3 INT,
--    ReboilerZone4 INT,
--    ReboilerZone5 INT,
--    ReboilerZone6 INT,
--    ColumnAverage INT,
--    InternalFluidTemp INT,
--    ColumnBottom INT,
--    ColumnMiddle INT,
--    ColumnTop INT,
--    RefluxSplitter INT,
--    CondenserTop INT,
--    CondenserCoolantOutlet INT,
--    AftercoolerCoolantOutlet INT,
--    CondeserSkin INT,
--    CondenserInlet INT,
--    AftercoolerInlet INT,
--    LN2 INT,
--)

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
    VendorId INT PRIMARY KEY IDENTITY(1,1),
    VendorName VARCHAR(25) NOT NULL,
    IsMPPS BIT NOT NULL DEFAULT(0)
)
CREATE NONCLUSTERED INDEX IX_Vendor_VendorName
ON Vendors.Vendor(VendorName ASC)
GO

CREATE TABLE Distillation.ProductNumberSequence
(
    SequenceId int PRIMARY KEY IDENTITY(1,1),
    SequenceIdStart INT NOT NULL,
    SequenceIdEnd INT NOT NULL
)



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
    NameId INT FOREIGN KEY REFERENCES Materials.Material NOT NULL,
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

CREATE TABLE Distillation.RawMaterial
(
    DrumLotNumber VARCHAR(10) PRIMARY KEY,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.MaterialNumber,
    DrumWeight INT,
    SapBatchNumber INT,
    ContainerNumber CHAR(7),
    SampleSubmitNumber CHAR(8) FOREIGN KEY REFERENCES QualityControl.SampleSubmit,
    VendorBatchId INT FOREIGN KEY REFERENCES Vendors.VendorBatch,
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
    --PressureId INT FOREIGN KEY REFERENCES Distillation.SystemPressureAtRunTime,
    --TemperatureId INT FOREIGN KEY REFERENCES Distillation.SystemTemperatureAtRunTime,
    --ProductId INT FOREIGN KEY REFERENCES Distillation.Production,
    --CheckID INT FOREIGN KEY REFERENCES Distillation.PreStartChecks
)
GO

--PROCEDURES

CREATE OR ALTER PROCEDURE Vendors.AddVendor(@vendorName AS VARCHAR(25), @isMpps AS BIT)
AS
BEGIN

IF NOT EXISTS (SELECT VendorName FROM Vendors.Vendor WHERE VendorName = @vendorName)

INSERT INTO Vendors.Vendor(VendorName,IsMPPS)
VALUES (@vendorName, @isMpps);

END
GO

CREATE OR ALTER PROCEDURE Vendors.AddVendorBatch(@vendorName AS VARCHAR(25), @batchNumber AS VARCHAR(50))
AS
BEGIN TRAN VendorBatch
BEGIN TRY
DECLARE @vendorId AS INT
SET @vendorId = (SELECT vendorId 
                    FROM Vendors.Vendor
                    WHERE vendorName = @vendorName)

INSERT INTO Vendors.VendorBatch(vendorId,VendorBatchNumber)
VALUES(@vendorId,@batchNumber);
COMMIT TRAN
END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH
GO

CREATE OR ALTER PROCEDURE Materials.MaterialInsert
    (@materialNumber AS INT,
    @materialName AS VARCHAR(50),
    @nameAbreviation AS VARCHAR(10),
    @permitNumber AS VARCHAR(25),
    @rawMaterialCode AS VARCHAR(3),
    @productCode AS VARCHAR(3),
    @carbonDrumRequired AS BIT,
    @carbonDrumDaysAllowed AS INT = NULL,
    @carbonDrumWeightAllowed AS INT = NULL,
    @batchManaged AS BIT,
    @requiresProcessOrder AS BIT,
    @unitOfIssue AS CHAR(2),
    @isRawMaterial AS BIT,
    @vendorName AS VARCHAR(25),
    @sequenceNumber AS INT)
AS
BEGIN TRAN MaterialInsert
BEGIN TRY 
INSERT INTO Material
    (MaterialName, MaterialNameAbreviation, PermitNumber, RawMaterialCode, ProductCode, CarbonDrumRequired, CarbonDrumDaysAllowed, CarbonDrumWeightAllowed)
VALUES(@materialName, @nameAbreviation, @permitNumber, @rawMaterialCode, @productCode, @carbonDrumRequired, @carbonDrumDaysAllowed, @carbonDrumWeightAllowed);

DECLARE @nameId AS INT
SET @nameId = (SELECT NameId
FROM MaterialName
WHERE MaterialName = @materialName);

INSERT INTO MaterialNumber
    (MaterialNumber, NameId,  BatchManaged, RequiresProcessOrder, UnitOfIssue, IsRawMaterial)
VALUES(@materialNumber, @nameId,  @batchManaged, @requiresProcessOrder, @unitOfIssue, @isRawMaterial);

DECLARE @vendorId AS INT
IF EXISTS(SELECT VendorId
FROM Vendor
WHERE VendorName = @vendorName)
BEGIN
    SET @vendorId =(SELECT VendorId
    FROM Vendor
    WHERE VendorName = @vendorName);
END
ELSE
BEGIN
    INSERT INTO Vendor
        (VendorName)
    VALUES(@vendorName);

    SET @vendorId =(SELECT vendorId
    FROM vendor
    WHERE vendorName = @vendorName);
END

DECLARE @sequenceId AS INT
SET @sequenceId =(SELECT sequenceId
FROM productNumberSequence
WHERE sequenceIdStart = @sequenceNumber);

DECLARE @currentSequenceId AS INT
SET @currentSequenceId =(SELECT sequenceIdStart
FROM productNumberSequence
WHERE sequenceId = @sequenceId);

INSERT INTO materialId
    (materialNumber, vendorId,sequenceId, currentSequenceId)
VALUES(@materialNumber, @vendorId, @sequenceId, @currentSequenceId);
COMMIT;

END TRY
BEGIN CATCH
    THROW;
    ROLLBACK;
END CATCH
GO


CREATE OR ALTER PROCEDURE Distillation.RawMaterialUpdate
    (@materialNumber AS INT,
    @vendorName AS VARCHAR(25),
    @vendorBatchNumber AS VARCHAR(25),
    @drumWeight INT = NULL,
    @sapBatchNumber INT = NULL,
    @containerNumber CHAR(7)=NULL,
    @quantity AS INT = 1
)
AS
BEGIN TRAN EnterRawMaterial
BEGIN TRY

DECLARE @drumId AS CHAR(10)
SET @drumId = (Distillation.setDrumId(@materialNumber, @vendorName));

DECLARE @batchId AS INT
SET @batchId = (SELECT BatchId FROM Vendors.VendorBatch WHERE VendorBatchNumber = @vendorBatchNumber);

INSERT INTO Distillation.RawMaterialLog
    (DrumLotNumber, MaterialNumber, DrumWeight, SapBatchNumber, ContainerNumber, VendorBatchId, DateUsed)
VALUES
    (@drumId, @materialNumber, @drumWeight, @sapBatchNumber, @containerNumber, @batchId, GETDATE());

COMMIT TRAN;
END TRY
BEGIN CATCH
    ROLLBACK TRAN;
END CATCH
GO

CREATE OR ALTER PROCEDURE udpMaterialInsertDB
AS
BEGIN
CREATE TABLE #tempTbl(
    MaterialName VARCHAR(50),
    MaterialNameAbreviation VARCHAR(15),
    MaterialNumber INT,
    PermitNumber VARCHAR(25),
    RawMaterialCode VARCHAR(3),
    ProductCode VARCHAR(3),
    CarbonDrumRequired BIT,
    CarbonDrumWeight INT, 
    CarbonDrumDays INT,
    SpecificGravity DECIMAL(3,2),
    PrefractionRefluxRatio VARCHAR(5),
    CollectRefluxRatio VARCHAR(5),
    NumberOfRuns INT,
    BatchManaged BIT,
    RequiresProcessOrder BIT,
    UnitOfIssue VARCHAR(2),
    IsRawMaterial BIT,
    Vendor VARCHAR(25),
    SequenceId INT);

BULK INSERT #tempTbl FROM '..\..\tmp\MaterialData.csv'
    WITH(
        FORMAT = 'CSV',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );
    BEGIN TRAN 
        BEGIN TRY
            
            INSERT INTO Materials.Material(MaterialName,MaterialNameAbreviation,PermitNumber,RawMaterialCode,ProductCode,CarbonDrumRequired,CarbonDrumDaysAllowed,CarbonDrumWeightAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns)
            SELECT TOP(6) MaterialName,MaterialNameAbreviation,PermitNumber,RawMaterialCode,ProductCode,CarbonDrumRequired,CarbonDrumDays,CarbonDrumWeight,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns
            FROM #tempTbl
            WHERE NOT EXISTS(SELECT * FROM Materials.Material WHERE Material.MaterialName = #tempTbl.MaterialName)
            
            INSERT INTO Materials.MaterialNumber(MaterialNumber,NameId,BatchManaged,RequiresProcessOrder,UnitOfIssue,IsRawMaterial)
            SELECT MaterialNumber,(Select NameId FROM Materials.Material WHERE Material.MaterialName = #tempTbl.MaterialName),BatchManaged,RequiresProcessOrder,UnitOfIssue,IsRawMaterial
            FROM #tempTbl
            WHERE NOT EXISTS(SELECT * FROM Materials.MaterialNumber WHERE MaterialNumber.MaterialNumber = #tempTbl.MaterialNumber)

        
            INSERT INTO Vendors.Vendor(VendorName)
            SELECT DISTINCT Vendor
            FROM #tempTbl
            WHERE NOT EXISTS(Select * FROM Vendors.Vendor WHERE Vendor.VendorName = #tempTbl.Vendor)

            INSERT INTO Materials.MaterialId(MaterialNumber, VendorId, CurrentSequenceId, SequenceId)
            SELECT MaterialNumber,(SELECT VendorId FROM Vendors.Vendor WHERE VendorName = #tempTbl.Vendor),SequenceId,(SELECT SequenceId FROM Distillation.ProductNumberSequence WHERE SequenceIdStart = #tempTbl.SequenceId)
            FROM #tempTbl


            COMMIT TRAN;
        END TRY
        BEGIN CATCH
           ROLLBACK;
        END CATCH
END
GO

--TRIGGERS

CREATE OR ALTER TRIGGER Distillation.IncrementSequenceId
ON Distillation.RawMaterial
AFTER INSERT,UPDATE

AS

DECLARE @vendorId AS INT
SET @vendorId = (select VendorId
                    From Vendors.VendorBatch
                    WHERE BatchId = (SELECT inserted.VendorBatchId
                                    FROM inserted));
                                                
DECLARE @materialNumber AS INT

SET @materialNumber = (select top(1)
    inserted.MaterialNumber
FROM inserted);

DECLARE @currentId AS INT
SET @currentId = (SELECT CurrentSequenceId
FROM MaterialId
WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber);

IF @currentId = (SELECT SequenceIdEnd
                FROM Distillation.ProductNumberSequence
                    JOIN MaterialId on ProductNumberSequence.SequenceId = MaterialId.SequenceId
                WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber)
SET @currentId = (SELECT sequenceIdStart FROM Distillation.ProductNumberSequence
                    JOIN MaterialId on ProductNumberSequence.SequenceId = MaterialId.SequenceId
                WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber)

ELSE
SET @currentId = (@currentId + 1)


UPDATE MaterialId 
SET CurrentsequenceId = (@currentId)
WHERE VendorId = @vendorId AND MaterialNumber = @materialNumber;
GO

CREATE OR ALTER TRIGGER QualityControl.SetSampleDates
ON QualityControl.SampleSubmit
AFTER UPDATE
AS

DECLARE @rejected AS BIT
SET @rejected = (SELECT inserted.Rejected
                    FROM inserted)

IF(@rejected = 0)
    UPDATE QualityControl.SampleSubmit
    SET ApprovalDate = GETDATE(),
        ExperiationDate = DATEADD(YEAR,1,GETDATE())
    WHERE Rejected = @rejected;    

ELSE IF(@rejected = 1)

    UPDATE QualityControl.SampleSubmit
    SET RejectedDate = GETDATE()
    WHERE Rejected = @rejected;
GO

--FUNCTIONS

CREATE OR ALTER FUNCTION Materials.GetMaterialId(@materialNumber AS INT, @vendorName AS VARCHAR(25))
RETURNS INT
AS 
BEGIN

DECLARE @id AS INT
SET @id = (SELECT vendorId 
        FROM Vendors.Vendor 
        WHERE VendorName = @vendorName)

DECLARE @materialId AS INT
SET @materialId = (SELECT MaterialId 
        FROM Materials.MaterialId
        WHERE VendorId = @id AND materialNumber = @materialNumber)

RETURN @materialId;
END
GO

CREATE OR ALTER FUNCTION Vendors.ObtainVendorBatchId(@name AS VARCHAR(25), @vendorBatchNumber AS CHAR(25))
RETURNS INT
AS
BEGIN
DECLARE @vendorBatchId AS INT
IF EXISTS(SELECT BatchId
            FROM Vendors.VendorBatch
            WHERE VendorBatchNumber = @vendorBatchNumber)
BEGIN
    SET @vendorBatchId = (SELECT BatchId
                            FROM Vendors.VendorBatch
                            WHERE VendorBatchNumber = @vendorBatchNumber);
END
ELSE
    
    EXEC AddVendorBatch @name, @vendorBatchNumber;
    
    SET @vendorBatchId = (SELECT BatchId
                            FROM Vendors.VendorBatch
                            WHERE VendorBatchNumber = @vendorBatchNumber)
    
    RETURN @vendorBatchId;
END
GO
CREATE or ALTER FUNCTION Distillation.SetDrumId
    (@materialNumber AS INT,
    @vendorName AS CHAR(20))
    RETURNS CHAR(10) 
    AS 
BEGIN

    DECLARE @alphabeticDate AS CHAR(1)
    SET @alphabeticDate = (SELECT AlphabeticCode
    FROM AlphabeticDate
    WHERE MonthNumber = MONTH(GETDATE()));

    DECLARE @drumId AS CHAR(10)
    SET @drumId =(
    SELECT CONCAT(FORMAT(MaterialId.CurrentSequenceId,'000'), Material.RawMaterialCode,RIGHT(YEAR(GETDATE()),1),@alphabeticDate,FORMAT(GETDATE(),'dd')) AS 'Drum ID'
    FROM Materials.MaterialNumber
        JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
        JOIN Materials.Material ON Material.NameId = MaterialNumber.NameId
        JOIN Vendors.Vendor ON Vendor.VendorId = MaterialId.VendorId
    WHERE MaterialNumber.MaterialNumber = @materialNumber
        AND Vendor.vendorName = @vendorName)

    RETURN @drumId
END
GO

CREATE OR ALTER FUNCTION Materials.SpecificGravity(@materialName AS CHAR(20), @WeightLiters AS DECIMAL(5,2))
RETURNS DECIMAL 
AS
BEGIN

DECLARE @weightKG AS DECIMAL(5,3)
SET @weightKG = (@weightLiters * (SELECT SpecificGravity 
                                    FROM Materials.Material
                                    WHERE MaterialNameAbreviation = @materialName ));

RETURN @weightKG;
END
GO

CREATE OR ALTER FUNCTION HumanResources.EmployeeInitials(@employeeId AS CHAR(7) = 'NA')
RETURNS CHAR(2)
AS
BEGIN
DECLARE @employeeInit AS CHAR(2)
SET @employeeInit = (CONCAT(Left(1,(SELECT FirstName FROM HumanResources.Employee WHERE EmployeeId = @employeeId)),Left(1,(SELECT LastName FROM HumanResources.Employee WHERE EmployeeId = @employeeId))))

RETURN @employeeInit;
END
GO

CREATE OR ALTER FUNCTION Materials.SpecificGravity(@materialName AS CHAR(20), @WeightLiters AS DECIMAL(5,2))
RETURNS DECIMAL 
AS
BEGIN

DECLARE @weightKG AS DECIMAL(5,3)
SET @weightKG = (@weightLiters * (SELECT SpecificGravity 
                                    FROM Materials.Material
                                    WHERE MaterialNameAbreviation = @materialName ));

RETURN @weightKG;
END
GO

--Veiw

CREATE OR ALTER VIEW Distillation.RawMaterialLog
AS

SELECT DateUsed AS 'Date',
    Vendor.VendorName AS 'Vendor', 
    HumanResources.EmployeeInitials(Employee.EmployeeId)AS 'Operator',
    DrumLotNumber AS 'Drum ID',
    SapBatchNumber AS 'SAP Batch Number', 
    VendorBatch.VendorBatchNumber AS 'Vendor Batch Number', 
    SampleSubmit.SampleSubmitNumber AS 'Sample Number',
    InspectionLotNumber AS 'Lot Number', 
    ContainerNumber AS 'Container Number', 
    CONCAT(DrumWeight,' ',UnitOfIssue) AS 'Weight', 
    HumanResources.EmployeeInitials(SampleSubmit.EmployeeId) AS 'QC Operator',
    ApprovalDate AS 'Approval Date',
    ExperiationDate AS 'Experation Date',
    RejectedDate AS 'Rejected Date'

FROM Distillation.RawMaterial
JOIN Materials.MaterialNumber ON RawMaterial.MaterialNumber = MaterialNumber.MaterialNumber
JOIN Materials.Material ON MaterialNumber.NameId = Material.NameId
JOIN Vendors.VendorBatch ON RawMaterial.VendorBatchId = VendorBatch.BatchId
JOIN Vendors.Vendor ON VendorBatch.VendorId = Vendor.VendorId
JOIN QualityControl.SampleSubmit ON RawMaterial.SampleSubmitNumber = SampleSubmit.SampleSubmitNumber
JOIN HumanResources.Employee ON RawMaterial.EmployeeId = Employee.EmployeeId
GO