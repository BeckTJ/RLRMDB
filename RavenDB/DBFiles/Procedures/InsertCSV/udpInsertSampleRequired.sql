DROP TABLE #tmpRequired
go
CREATE TABLE #tmpRequired
(
    MaterialNumber INT,
    MaterialName VARCHAR(25),
    MaterailType VARCHAR(25),
    VLN VARCHAR(25),
    Assay BIT,
    Water BIT,
    Metals BIT,
    Chloride BIT,
    Boron BIT,
    Phosphorus BIT,
    Amp INT,
    AmpVolume INT,
    AmpUI VARCHAR(2),
    AssayBulb INT,
    MetalBubbler INT,
    BubblerVolume INT,
    BubblerUI VARCHAR(2),
    Vial INT,
    VialVolume INT,
    VialUI VARCHAR(2),
    Retain BIT,
)
DELETE FROM QualityControl.SampleRequired
BULK INSERT QualityControl.SampleRequired FROM '..\..\usr\dbfiles\BuildFiles\MaterialSampleRequired.csv'
WITH
(
    FORMAT = 'csv',
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '\n',
    KEEPNULLS
)
select * from QualityControl.SampleRequired
select * from #tmpRequired