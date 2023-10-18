INSERT INTO Distillation.AlphabeticDate
    (MonthNumber, AlphabeticCode)
VALUES
    (1, 'A'),
    (2, 'B'),
    (3, 'C'),
    (4, 'D'),
    (5, 'E'),
    (6, 'F'),
    (7, 'G'),
    (8, 'H'),
    (9, 'J'),
    (10, 'K'),
    (11, 'L'),
    (12, 'M')

GO
INSERT INTO Materials.UnitOfIssue(UnitOfIssue, Nomenclature)
VALUES ('kg','Kilogram'),
    ('g', 'Gram'),
    ('L', 'Liter'),
    ('mL', 'Milliliter')

GO

INSERT INTO Distillation.SystemStatus(StatusCode,StatusName)
VALUES('H','Heatup'),
    ('R','Reflux'),
    ('P', 'Pre-Fraction'),
    ('C', 'Collect'),
    ('S', 'Shutdown'),
    ('SP', 'System Pressurise')

INSERT INTO Engineering.Receiver(ReceiverName)
VALUES('A'),
    ('B'),
    ('C'),
    ('D'),
    ('A-101'),
    ('A-102'),
    ('A-103'),
    ('A-104'),
    ('A-901'),
    ('A-902'),
    ('A-903'),
    ('A-904')
GO

INSERT into HumanResources.Employee (EmployeeId, FirstName, LastName)
    VALUES  ('LAS1234', 'John', 'Smith'),
            ('LAS2345', 'Jane', 'Smith'),
            ('LAS3456', 'Brian', 'Squire')
GO

BULK INSERT QualityControl.SampleRequired FROM '..\..\usr\raven\buildfiles\BuildFiles\MaterialSampleRequired.csv'
WITH
(
    FORMAT = 'csv',
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '\n',
    KEEPNULLS
)
GO

CREATE OR ALTER PROCEDURE SystemDataInsertDB
AS
BEGIN

Create Table #tempSystemTbl(
    IndicatorType VARCHAR(50),
    IsRequired BIT,
    MaterialName VARCHAR(25),
    Nomenclature VARCHAR(50),
    Indicator VARCHAR(25),
    SetPoint DECIMAL(6,2),
    Variance DECIMAL(6,2)
);

BULK INSERT #tempSystemTbl FROM '..\..\us\raven\buildfiles\BuildFiles\SystemData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );
BEGIN TRAN
    BEGIN TRY

        INSERT INTO Engineering.SystemIndicator(IndicatorType)
        SELECT DISTINCT IndicatorType FROM #tempSystemTbl
    
        INSERT INTO Engineering.SystemNomenclature(Nomenclature)
        SELECT DISTINCT Nomenclature FROM #tempSystemTbl

        INSERT INTO Engineering.IndicatorSetPoint(IndicatorType,IsRequired,MaterialNumber,Nomenclature,Indicator,SetPoint,Variance)
        SELECT (SELECT IndicatorType FROM Engineering.SystemIndicator WHERE IndicatorType = #tempSystemTbl.IndicatorType),
            IsRequired, 
            (SELECT MaterialNumber FROM Materials.Material WHERE Material.MaterialName = #tempSystemTbl.MaterialName),
            (SELECT Nomenclature FROM Engineering.SystemNomenclature WHERE Nomenclature = #tempSystemTbl.Nomenclature),
            Indicator,
            SetPoint,
            Variance
         FROM #tempSystemTbl

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK;
    END CATCH
END
GO

CREATE OR ALTER PROCEDURE MaterialInsertDB
AS
BEGIN
CREATE TABLE #tempTbl(
    Id INT IDENTITY(1,1),
    MaterialName VARCHAR(50),
    MaterialNameAbreviation VARCHAR(15),
    MaterialNumber INT,
    PermitNumber VARCHAR(25),
    MaterialCode VARCHAR(3),
    CarbonDrumRequired BIT,
    CarbonDrumWeight INT, 
    CarbonDrumDays INT,
    VacuumTrapRequired BIT,
    VacuumTrapDaysAllowed INT,
    SpecificGravity DECIMAL(3,2),
    PrefractionRefluxRatio VARCHAR(5),
    CollectRefluxRatio VARCHAR(5),
    NumberOfRuns INT,
    BatchManaged BIT,
    RequiresProcessOrder BIT,
    UnitOfIssue VARCHAR(2),
    IsRawMaterial BIT,
    Vendor VARCHAR(25),
    IsMPPS BIT,
    SequenceId INT);

BULK INSERT #tempTbl FROM '..\..\usr\raven\buildfiles\BuildFiles\MaterialData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );
    BEGIN TRAN 
        BEGIN TRY

            DECLARE @materialNumber INT
            DECLARE @parentMaterialNumber INT
            DECLARE @batchManaged BIT
            DECLARE @requiresProcessOrder BIT
            DECLARE @unitofIssue CHAR(2)
            DECLARE @isRawMaterial BIT
            DECLARE @materialName VARCHAR(10)
            DECLARE @count INT
            DECLARE @index INT

            set @index = 1

            INSERT INTO Materials.Material(MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,CarbonDrumRequired,CarbonDrumDaysAllowed,CarbonDrumWeightAllowed,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns)
            SELECT TOP(6) MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,CarbonDrumRequired,CarbonDrumDays,CarbonDrumWeight,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns
            FROM #tempTbl
            WHERE NOT EXISTS(SELECT * FROM Materials.Material WHERE Material.MaterialName = #tempTbl.MaterialName)


            while(@index <= (select count(*)FROM #tempTbl))
            BEGIN
                SET @materialNumber = (SELECT MaterialNumber FROM #tempTbl WHERE ID = @index)
                SET @materialName = (SELECT MaterialNameAbreviation FROM #tempTbl WHERE ID = @index)
                SET @parentMaterialNumber = (SELECT MaterialNumber FROM Materials.Material WHERE Material.MaterialNameAbreviation = @materialName)
                SET @batchManaged  = (SELECT BatchManaged FROM #tempTbl WHERE ID = @index)
                SET @requiresProcessOrder  = (SELECT RequiresProcessOrder FROM #tempTbl WHERE ID = @index)
                SET @unitofIssue  = (SELECT UnitOfIssue FROM #tempTbl WHERE ID = @index)
                SET @isRawMaterial  = (SELECT IsRawMaterial FROM #tempTbl WHERE ID = @index)
                set @index += 1

                EXEC Materials.InsertMaterialNumber @materialNumber,@parentMaterialNumber,@batchManaged,@requiresProcessOrder,@unitOfIssue,@isRawMaterial
            END

            INSERT INTO Materials.Vendor(VendorName)
            SELECT DISTINCT Vendor
            FROM #tempTbl
            WHERE NOT EXISTS(Select * FROM Materials.Vendor WHERE Vendor.VendorName = #tempTbl.Vendor)

            INSERT INTO Materials.MaterialId(MaterialNumber, VendorName, MaterialCode, SequenceId, TotalRecords)
            SELECT MaterialNumber,(SELECT VendorName FROM Materials.Vendor WHERE VendorName = #tempTbl.Vendor),MaterialCode, SequenceId, 100
            FROM #tempTbl

            COMMIT TRAN;
        END TRY
        BEGIN CATCH
           ROLLBACK;
        END CATCH
END
GO

CREATE OR ALTER PROCEDURE InsertRawMaterial
AS
BEGIN

    CREATE TABLE #rawMaterial(
        Id INT IDENTITY(1,1),
        SampleDate DATE,
        MaterialNumber INT,
        Vendor VARCHAR(25),
        VendorBatchNumber VARCHAR(25),
        SapBatchNumber INT,
        InspectionLotNumber NUMERIC,
        ContainerNumber CHAR(7),
        SampleSubmitNumber CHAR(8),
        Quantity INT,
        DrumWeight DECIMAL(6,2),

    )

    BULK INSERT #rawMaterial FROM '..\..\usr\raven\buildfiles\BuildFiles\RawMaterialData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    )

            DECLARE @materialNumber INT
            DECLARE @vendor VARCHAR(25)
            DECLARE @vendorBatchNumber VARCHAR(25)
            DECLARE @inspectionLotNumber NUMERIC
            DECLARE @sapBatchNumber INT
            DECLARE @sampleSubmitNumber CHAR(8)
            DECLARE @containerNumber CHAR(7)
            DECLARE @numberOfDrums INT
            DECLARE @drumWeight DECIMAL(6,2)
            DECLARE @sampleDate DATE
            DECLARE @rows INT
            DECLARE @index INT
            
            SET @rows = (SELECT COUNT(*) FROM #rawMaterial)
            SET @index = 1

            WHILE(@index <= @rows)
                BEGIN
                SET @materialNumber = (select materialnumber from #rawMaterial where id = @index)
                SET @vendor = (select vendor from #rawMaterial where id = @index)
                SET @vendorBatchNumber = (select VendorBatchNumber from #rawMaterial where id = @index)
                SET @inspectionLotNumber = (select InspectionLotNumber from #rawMaterial where id = @index)
                SET @sapBatchNumber = (select SapBatchNumber from #rawMaterial where id = @index)
                SET @sampleSubmitNumber = (select SampleSubmitNumber from #rawMaterial where id = @index)
                SET @containerNumber = (select ContainerNumber from #rawMaterial where id = @index)
                SET @numberOfDrums = (select Quantity from #rawMaterial where id = @index)
                SET @drumWeight = (select drumWeight from #rawMaterial where id = @index)
                SET @sampleDate = (select SampleDate from #rawMaterial where id = @index)

                EXEC Distillation.SetRawMaterial @materialNumber, @vendor, @vendorBatchNumber, @inspectionLotNumber, @sapBatchNumber, @sampleSubmitNumber, @containerNumber, @numberOfDrums, @drumWeight, @sampleDate
                
                SET @index += 1
                END
                
END
GO

EXEC MaterialInsertDB
GO

EXEC SystemDataInsertDB
GO

EXEC InsertRawMaterial
GO

EXEC Distillation.InsertProductLot
GO

EXEC Distillation.InsertProductLevels
GO

CREATE TABLE #tmpReceiver(
    Id int PRIMARY KEY IDENTITY(1,1),
    MaterialName VARCHAR(10),
    MaterialNumber INT,
    ReceiverName VARCHAR(6),
    MaxReceiverLevel INT,
)

BULK INSERT #tmpReceiver FROM '..\..\usr\raven\buildfiles\BuildFiles\ReceiverData.csv'
WITH(
    FORMAT = 'csv',
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '\n',
    KEEPNULLS
)

INSERT INTO Engineering.SystemReceivers(MaterialNumber,ReceiverName,MaxReceiverLevel)
SELECT 
(SELECT MaterialNumber FROM Materials.MaterialNumber WHERE MaterialNumber.MaterialNumber = #tmpReceiver.MaterialNumber),
(SELECT ReceiverName FROM Engineering.Receiver WHERE Receiver.ReceiverName = #tmpReceiver.ReceiverName),
MaxReceiverLevel 
FROM #tmpReceiver
GO

-- select * from Distillation.RawMaterial
-- select * from QualityControl.SampleSubmit
-- select * from QualityControl.SampleRequired