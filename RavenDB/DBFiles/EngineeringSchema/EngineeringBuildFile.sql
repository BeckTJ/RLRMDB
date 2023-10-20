CREATE SCHEMA Engineering
GO

CREATE TABLE Engineering.SystemNomenclature(
    Nomenclature VARCHAR(50) PRIMARY KEY
)

CREATE TABLE Engineering.Indicator(
    IndicatorAbreviation VARCHAR(3),
    IndicatorType VARCHAR(25)
)

CREATE TABLE Engineering.IndicatorSetPoints(
    SystemId INT PRIMARY KEY IDENTITY,
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.Material,
    Nomenclature VARCHAR(50) FOREIGN KEY REFERENCES Engineering.SystemNomenclature,
    Indicator VARCHAR(10),
    SetPointChar VARCHAR(10),
    SetPointLow DECIMAL(6,2),
    SetPointHigh DECIMAL(6,2),
    Variance DECIMAL(6,2)
)
CREATE TABLE Engineering.Receiver
(
    ReceiverName CHAR(5) PRIMARY KEY NOT NULL
)
CREATE TABLE Engineering.SystemReceivers(
    ReceiverId INT PRIMARY KEY IDENTITY(1,1),
    MaterialNumber INT FOREIGN KEY REFERENCES Materials.Material,
    ReceiverName CHAR(5) FOREIGN KEY REFERENCES Engineering.Receiver,
    MaxReceiverLevel INT,
)
CREATE TABLE Engineering.SystemInformation
(
    MaterialNumber INT PRIMARY KEY,
    CarbonDrumRequired BIT DEFAULT(0) NOT NULL,
    CarbonDrumDaysAllowed INT,
    CarbonDrumWeightAllowed INT,
    CarbonDrumInstallDate DATE,
    VacuumTrapRequired BIT DEFAULT(0) NOT NULL,
    VacuumTrapInstallDate DATE,
    VacuumTrapDaysAllowed INT,
    SpecificGravity DECIMAL(3,2),
    PrefractionRefluxRatio VARCHAR(5),
    CollectRefluxRatio VARCHAR(5),
    NumberOfRuns INT,
    HeelPumpFrequency INT,
)
GO

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

BULK INSERT #tempSystemTbl FROM '..\..\usr\raven\BuildFiles\SystemData.csv'
    WITH(
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
INSERT INTO Engineering.SystemReceivers(MaterialNumber,ReceiverName,MaxReceiverLevel)
SELECT 
(SELECT MaterialNumber FROM Materials.MaterialNumber WHERE MaterialNumber.MaterialNumber = #tmpReceiver.MaterialNumber),
(SELECT ReceiverName FROM Engineering.Receiver WHERE Receiver.ReceiverName = #tmpReceiver.ReceiverName),
MaxReceiverLevel 
FROM #tmpReceiver
GO
