CREATE OR ALTER PROCEDURE Distillation.InsertProductLevels
AS
BEGIN

CREATE TABLE #tmpRun
(
    LotNumber VARCHAR(10),
    RunNumber INT,
    SystemStatus VARCHAR(2),
    VisualVerification BIT,
    ReceiverLevel INT,
    PrefractionLevel INT,
    ReboilerLevel INT,
    ReadTime TIME,
)

BULK INSERT #tmpRun FROM '..\..\usr\dbfiles\BuildFiles\RunData.csv'
    WITH
    (
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

    INSERT INTO Distillation.ProductLevels(ProductLotNumber,RunNumber,SystemStatus,VisualVerification,ReboilerLevel,PrefractionLevel,ReceiverLevel,ReadTime)
    SELECT LotNumber,RunNumber,SystemStatus,VisualVerification,ReboilerLevel,PrefractionLevel,ReceiverLevel,ReadTime
    FROM #tmpRun
END