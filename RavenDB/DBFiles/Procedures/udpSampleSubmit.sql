CREATE OR ALTER PROCEDURE QualityControl.SubmitSample
    (@sampleNumber AS CHAR(8), @lotNumber AS NUMERIC, @sampleDate DATE)
AS

DECLARE @id VARCHAR(10)
DECLARE @productId VARCHAR(10)

SET @id = (SELECT ProductLotNumber FROM Distillation.Production
            WHERE InspectionLotNumber = @lotNumber)

set @productId = Distillation.UpdateProductId(@id,@sampleDate)

INSERT INTO QualityControl.SampleSubmit(SampleSubmitNumber, InspectionLotNumber, SampleDate)
VALUES
    (@sampleNumber, @lotNumber, @sampleDate);

UPDATE Distillation.Production
SET SampleSubmitNumber = (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit
                            WHERE SampleSubmitNumber = @sampleNumber),
    ProductLotNumber = @productId
WHERE InspectionLotNumber = @lotNumber