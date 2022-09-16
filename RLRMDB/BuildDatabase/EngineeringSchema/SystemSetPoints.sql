CREATE SCHEMA Engineering
GO

CREATE TABLE Engineering.SystemIndicators(
    IndicatorId INT PRIMARY KEY,
    IndicatorNomenclature VARCHAR(50)
)

CREATE TABLE Engineering.SystemSetPoints(
    SystemId INT PRIMARY KEY IDENTITY,
    NameId INT FOREIGN KEY REFERENCES Materials.Material,
    IndicatorId INT FOREIGN KEY REFERENCES Engineering.SystemIndicators,
    Indicator VARCHAR(10),
    SetPointLow DECIMAL(6,3),
    SetPointHigh DECIMAL(6,3),
    Variance DECIMAL(6,3),

)