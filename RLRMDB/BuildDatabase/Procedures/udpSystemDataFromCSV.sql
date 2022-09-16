DROP TABLE #tempSystemTbl
GO

Create Table #tempSystemTbl(
    MaterialName VARCHAR(25),
    SystemName VARCHAR(50),
    Indicator VARCHAR(25),
    SetPointLow VARCHAR(25),
    SetPointHight VARCHAR(25),
    Variance VARCHAR(100)
);

BULK INSERT #tempSystemTbl FROM '../../tmp/SystemData.csv'
    WITH(
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

Select distinct SystemName from #tempSystemTbl;