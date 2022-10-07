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