BULK INSERT Materials.Material FROM '..\..\usr\dbfiles\BuildFiles\MaterialData.csv'
    WITH(
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

    BULK INSERT Materials.MaterialVendor FROM '..\..\usr\dbfiles\BuildFiles\MaterialVender.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

select * from Materials.materialVendor

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

BULK INSERT QualityControl.SampleRequired FROM '..\..\usr\dbfiles\BuildFiles\MaterialSampleRequired.csv'
WITH
(
    FORMAT = 'csv',
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '\n',
    KEEPNULLS
)
GO
