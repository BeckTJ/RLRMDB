CREATE OR ALTER FUNCTION Distillation.GetDaysOnCarbonDrum(@nameId INT)
RETURNS INT
AS
BEGIN

DECLARE @installDate DATE
DECLARE @numberOfDays INT

SET @installDate = (SELECT CarbonDrumInstallDate
                    FROM Materials.Material
                    WHERE nameId = @nameId)
                    

SET @numberOfDays = DATEDIFF(day,@installDate, GETDATE())

RETURN @numberOfDays
END