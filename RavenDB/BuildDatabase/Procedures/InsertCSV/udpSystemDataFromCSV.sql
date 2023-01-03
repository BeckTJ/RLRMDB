CREATE OR ALTER PROCEDURE SystemDataInsertDB
AS
BEGIN

Create Table #tempSystemTbl(
    MaterialName VARCHAR(25),
    Nomenclature VARCHAR(50),
    Indicator VARCHAR(10),
    SetPoint DECIMAL(6,2),
    Variance DECIMAL(6,2)
);

BULK INSERT #tempSystemTbl FROM '..\..\tmp\SystemData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );
BEGIN TRAN
    BEGIN TRY
    
        INSERT INTO Engineering.SystemNomenclature(Nomenclature)
        SELECT DISTINCT Nomenclature FROM #tempSystemTbl

        INSERT INTO Engineering.IndicatorSetPoints(ParentMaterialNumber,Nomenclature,Indicator,SetPointLow,SetPointHigh,Variance)
        SELECT (SELECT ParentMaterialNumber FROM Materials.Material WHERE Material.MaterialName = #tempSystemTbl.MaterialName),
            (SELECT Nomenclature FROM Engineering.SystemNomenclature WHERE Nomenclature = #tempSystemTbl.Nomenclature),
            Indicator,
            SetPointLow,
            SetPointHigh,
            Variance
         FROM #tempSystemTbl

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        THROW;
        ROLLBACK;
    END CATCH
END