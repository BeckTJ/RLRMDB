CREATE OR ALTER FUNCTION Materials.SpecificGravity(@materialName AS CHAR(20), @WeightLiters AS DECIMAL(5,2))
RETURNS DECIMAL 
AS
BEGIN

DECLARE @weightKG AS DECIMAL(5,3)
SET @weightKG = (@weightLiters * (SELECT SpecificGravity 
                                    FROM Materials.Material
                                    WHERE MaterialNameAbreviation = @materialName ));

RETURN @weightKG;
END