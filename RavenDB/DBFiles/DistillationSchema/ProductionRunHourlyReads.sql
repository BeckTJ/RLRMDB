drop table Distillation.ProductionRunHourlyReads
go
CREATE TABLE Distillation.ProductionRunHourlyReads(
    ReadId INT IDENTITY(1,1) PRIMARY KEY,
    ReadingTime TIME,
    SystemStatus VARCHAR(10),
    VisualVerification BIT,
    RecieverLevel INT,
    PrefractionLevel INT,
    ReboilerLevel INT,
)
GO

IF NOT EXISTS(SELECT * FROM Distillation.ProductionRunHourlyReads)

    select * from (
        select nomenclature from Engineering.SystemNomenclature
    )tab1
    pivot (
    )as tab2


    ALTER TABLE Distillation.ProductionRunHourlyReads
    ADD 
    
    
GO

SELECT * FROM Distillation.ProductionRunHourlyReads