CREATE OR ALTER FUNCTION Distillation.CheckCarbonDrum(@materialNumber INT)
RETURNS BIT
AS
BEGIN

DECLARE @carbonDrumRequired BIT
DECLARE @changeOutRequired BIT
DECLARE @daysOnCarbonDrum INT
DECLARE @daysAllowed INT

SET @carbonDrumRequired = (SELECT CarbonDrumRequired
                                    FROM Materials.Material
                                    JOIN Materials.MaterialNumber ON Material.NameId = MaterialNumber.NameId
                                    WHERE MaterialNumber = @materialNumber)
        
IF @carbonDrumRequired = 1
BEGIN
    SET @daysAllowed = (SELECT CarbonDrumDaysAllowed 
                        FROM Materials.Material
                        JOIN Materials.MaterialNumber ON Material.NameId = MaterialNumber.NameId
                        WHERE MaterialNumber = @materialNumber)
    
    SET @daysOnCarbonDrum = (SELECT Distillation.GetDaysOnCarbonDrum(@materialNumber))

    IF @daysOnCarbonDrum = @daysAllowed
    BEGIN
        SET @changeOutRequired = 1
    END
    ELSE
    BEGIN
        SET @changeOutRequired = 0
    END
END
        RETURN @changeOutRequired
END