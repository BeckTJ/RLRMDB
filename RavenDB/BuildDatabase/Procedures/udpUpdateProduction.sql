CREATE OR ALTER PROCEDURE Distillation.UpdateProduction(@materialNumber INT, @batchNumber INT, @processOrder NUMERIC, @inspectionLotNumber NUMERIC = NULL, @sampleNumber CHAR(8) = NULL)
AS

    DECLARE @productId VARCHAR(10)
    SET @productId = Distillation.SetProductID(@materialNumber,'Finish Product')



    INSERT INTO Distillation.Production(ProductLotNumber,MaterialNumber, ProductBatchNumber, ProcessOrder, InspectionLotNumber, SampleSubmitNumber)
    VALUES(@productId,
        (SELECT MaterialNumber FROM Materials.Material WHERE MaterialNumber = @materialNumber),
        @batchNumber,
        @processOrder,
        @inspectionLotNumber,
        (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmitNumber = @sampleNumber))