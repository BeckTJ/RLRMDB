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

INSERT into HumanResources.Employee (EmployeeId, FirstName, LastName)
    VALUES  ('LAS1234', 'John', 'Smith'),
            ('LAS2345', 'Jane', 'Smith'),
            ('LAS3456', 'Brian', 'Squire')
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

BULK INSERT #tempSystemTbl FROM '..\..\usr\dbfiles\BuildFiles\SystemData.csv'
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

BULK INSERT #tempTbl FROM '..\..\usr\dbfiles\BuildFiles\MaterialData.csv'
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

            while(@index <= (select count(*)FROM #tempTbl))

            SET @materialNumber = (SELECT MaterialNumber FROM #tempTbl WHERE ID = @index)
            SET @materialName = (SELECT MaterialNameAbreviation FROM #tempTbl WHERE ID = @index)
            SET @parentMaterialNumber = (SELECT MaterialNumber FROM Materials.Material WHERE Material.MaterialNameAbreviation = @materialName)
            SET @batchManaged  = (SELECT BatchManaged FROM #tempTbl WHERE ID = @index)
            SET @requiresProcessOrder  = (SELECT RequiresProcessOrder FROM #tempTbl WHERE ID = @index)
            SET @unitofIssue  = (SELECT UnitOfIssue FROM #tempTbl WHERE ID = @index)
            SET @isRawMaterial  = (SELECT IsRawMaterial FROM #tempTbl WHERE ID = @index)
            set @index += 1

            INSERT INTO Materials.Material(MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,CarbonDrumRequired,CarbonDrumDaysAllowed,CarbonDrumWeightAllowed,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns)
            SELECT TOP(6) MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,CarbonDrumRequired,CarbonDrumDays,CarbonDrumWeight,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns
            FROM #tempTbl
            WHERE NOT EXISTS(SELECT * FROM Materials.Material WHERE Material.MaterialName = #tempTbl.MaterialName)
            
            EXEC Materials.InsertMaterialNumber @materialNumber,@parentMaterialNumber,@batchManaged,@requiresProcessOrder,@unitOfIssue,@isRawMaterial
        
            INSERT INTO Vendors.Vendor(VendorName,IsMPPS)
            SELECT DISTINCT Vendor,IsMPPS
            FROM #tempTbl
            WHERE NOT EXISTS(Select * FROM Vendors.Vendor WHERE Vendor.VendorName = #tempTbl.Vendor)

            INSERT INTO Materials.MaterialId(MaterialNumber, VendorName, MaterialCode, SequenceId)
            SELECT MaterialNumber,(SELECT VendorName FROM Vendors.Vendor WHERE VendorName = #tempTbl.Vendor),MaterialCode,
            (SELECT SequenceId FROM Distillation.ProductNumberSequence WHERE SequenceId = #tempTbl.SequenceId)
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
        VendorBatchNumber VARCHAR(25),
        SapBatchNumber INT,
        InspectionLotNumber NUMERIC,
        ContainerNumber CHAR(7),
        SampleSubmitNumber CHAR(8),
        Quantity INT,
        DrumWeight DECIMAL(6,2),

    )

    BULK INSERT #rawMaterial FROM '..\..\usr\dbfiles\BuildFiles\RawMaterialData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    )

            DECLARE @materialNumber INT
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
                SET @vendorBatchNumber = (select VendorBatchNumber from #rawMaterial where id = @index)
                SET @inspectionLotNumber = (select InspectionLotNumber from #rawMaterial where id = @index)
                SET @sapBatchNumber = (select SapBatchNumber from #rawMaterial where id = @index)
                SET @sampleSubmitNumber = (select SampleSubmitNumber from #rawMaterial where id = @index)
                SET @containerNumber = (select ContainerNumber from #rawMaterial where id = @index)
                SET @numberOfDrums = (select Quantity from #rawMaterial where id = @index)
                SET @drumWeight = (select drumWeight from #rawMaterial where id = @index)
                SET @sampleDate = (select SampleDate from #rawMaterial where id = @index)

                EXEC Distillation.SetRawMaterial @materialNumber, @vendorBatchNumber, @inspectionLotNumber, @sapBatchNumber, @sampleSubmitNumber, @containerNumber, @numberOfDrums, @drumWeight, @sampleDate
                
                SET @index += 1
                END
                
END
GO

EXEC MaterialInsertDB
GO
EXEC SystemDataInsertDB
GO
EXEC InsertRawMaterial

