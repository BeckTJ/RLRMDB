CREATE OR ALTER TRIGGER UpdateProductNumber
ON Distillation.Production
AFTER UPDATE
AS

IF(UPDATE(SampleSubmitNumber))
    BEGIN
    DECLARE @sampleDate DATE
    DECLARE @product VARCHAR(10)

    SET @sampleDate = GETDATE()
    SET @product = (SELECT inserted.ProductLotNumber FROM inserted)

    

    UPDATE Distillation.Production
    SET ProductLotNumber = Distillation.UpdateProductId(@product,@sampleDate)
    WHERE ProductLotNumber = @product
    END