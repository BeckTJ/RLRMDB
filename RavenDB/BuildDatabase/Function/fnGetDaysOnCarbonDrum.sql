CREATE OR ALTER FUNCTION Distillation.GetDaysOnCarbonDrum(@materialNumber INT)
RETURNS INT
AS
BEGIN

DECLARE @installDate DATE
DECLARE @activeDays INT
DECLARE @numberOfDays INT

SET @installDate = (SELECT CarbonDrumInstallDate
                    FROM Materials.Material
                    JOIN Materials.MaterialNumber ON Material.NameId = MaterialNumber.NameId
                    WHERE MaterialNumber = @materialNumber)

SET @activeDays = 1 + (SELECT COUNT(DISTINCT StartDate)
                        FROM Distillation.Production
                        WHERE MaterialNumber = @materialNumber AND StartDate >= @installDate) 

RETURN @activeDays
END