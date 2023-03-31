CREATE OR ALTER PROCEDURE Distillation.InsertProductLot
AS
BEGIN

CREATE TABLE #tmpProduct
(
LotNumber VARCHAR(10),
MaterialNumber INT,
ReceiverName VARCHAR(5),
ProcessOrder NUMERIC,
BatchNumber INT,
InspectionLotNumber NUMERIC,
RunNumber INT,
DrumLotNumber VARCHAR(10),
RawMaterialUsed INT,
StartDate DATE
)

BULK INSERT #tmpProduct FROM '..\..\usr\dbfiles\BuildFiles\ProductLotData.csv'
    WITH
    (
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

    INSERT INTO Distillation.Production(ProductLotNumber,MaterialNumber,ProductBatchNumber,ProcessOrder,ReceiverName)
    SELECT DISTINCT LotNumber,MaterialNumber,BatchNumber,ProcessOrder,ReceiverName FROM #tmpProduct
    WHERE NOT EXISTS(SELECT 1 FROM Distillation.Production WHERE Production.ProductLotNumber = #tmpProduct.LotNumber)

    INSERT INTO Distillation.ProductRun(ProductLotNumber,RunNumber,DrumLotNumber,RawMaterialUsed,RunStartDate)
    SELECT 
        (SELECT ProductLotNumber FROM Distillation.Production WHERE Production.ProductLotNumber = #tmpProduct.LotNumber),
        RunNumber,
        (SELECT DrumLotNumber FROM Distillation.RawMaterial WHERE RawMaterial.DrumLotNumber = #tmpProduct.DrumLotNumber),
        RawMaterialUsed,
        StartDate
      FROM #tmpProduct
END