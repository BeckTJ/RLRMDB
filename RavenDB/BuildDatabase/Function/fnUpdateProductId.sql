CREATE OR ALTER FUNCTION Distillation.UpdateProductId(@id VARCHAR(10),@sampleDate DATE = NULL)
RETURNS VARCHAR(10)
AS
BEGIN

IF @sampleDate = NULL
    BEGIN
    SET @sampleDate = GETDATE()
    END

DECLARE @alphabeticDate AS CHAR(1)
    SET @alphabeticDate = (SELECT AlphabeticCode
    FROM AlphabeticDate
    WHERE MonthNumber = MONTH(@sampleDate));
    
DECLARE @productId AS VARCHAR(10)
SET @productId = CONCAT(@id,RIGHT(YEAR(@sampleDate),1),@alphabeticDate, FORMAT(@sampleDate,'dd'))


RETURN @productId
END
