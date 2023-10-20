use Raven
GO

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

INSERT into HumanResources.Employee (EmployeeId, FirstName, LastName)
    VALUES  ('LAS1234', 'John', 'Smith'),
            ('LAS2345', 'Jane', 'Smith'),
            ('LAS3456', 'Brian', 'Squire')
GO

BULK INSERT Materials.Material FROM '..\..\usr\raven\BuildFiles\MaterialData.csv'
    WITH(
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

BULK INSERT Materials.MaterialVendor FROM '..\..\usr\raven\BuildFiles\MaterialVendor.csv'
    WITH(
        FORMAT='csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

BULK INSERT QualityControl.SampleRequired FROM '..\..\usr\raven\BuildFiles\MaterialSampleRequired.csv'
WITH
(
    FORMAT = 'csv',
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '\n',
    KEEPNULLS
)
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
        ContainerNumber VARCHAR(7),
        SampleSubmitNumber CHAR(8),
        Quantity INT,
        DrumWeight DECIMAL(6,2),

    )

    BULK INSERT #rawMaterial FROM '..\..\usr\raven\BuildFiles\RawMaterialData.csv'
    WITH(
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
delete QualityControl.SampleSubmit
EXEC InsertRawMaterial
GO

