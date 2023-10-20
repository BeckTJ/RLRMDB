--exec SystemDataInsertDB
drop table #tempSystemTbl
go

Create Table #tempSystemTbl(
    MaterialName VARCHAR(50),
    Nomenclature VARCHAR(50),
    Indicator VARCHAR(10),
    SetPoint DECIMAL(6,2),
    Variance DECIMAL(6,2)
);

BULK INSERT #tempSystemTbl FROM '../../tmp/SystemData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

    --select * from #tempSystemTbl

        INSERT INTO Engineering.SystemNomenclature(Nomenclature)
        SELECT DISTINCT Nomenclature FROM #tempSystemTbl

        INSERT INTO Engineering.IndicatorSetPoint(MaterialNumber,Nomenclature,Indicator,SetPoint,Variance)
        SELECT (SELECT ParentMaterialNumber FROM Materials.Material WHERE Material.MaterialName = #tempSystemTbl.MaterialName),
            (SELECT Nomenclature FROM Engineering.SystemNomenclature WHERE Nomenclature = #tempSystemTbl.Nomenclature),
            Indicator,
            SetPoint,
            Variance
         FROM #tempSystemTbl

select * from Engineering.IndicatorSetPoint
select * from Engineering.SystemNomenclature
