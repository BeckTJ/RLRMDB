CREATE OR ALTER FUNCTION Distillation.UpdateProductId(@id VARCHAR(10))
RETURNS VARCHAR(10)
AS
BEGIN

DECLARE @alphabeticDate AS CHAR(1)
    SET @alphabeticDate = (SELECT AlphabeticCode
    FROM AlphabeticDate
    WHERE MonthNumber = MONTH(GETDATE()));

DECLARE @productId AS VARCHAR(10)
SET @productId = CONCAT(@id,RIGHT(YEAR(GETDATE()),1),@alphabeticDate, FORMAT(GETDATE(),'dd'))


RETURN @productId
END
