CREATE OR ALTER PROCEDURE Distillation.InsertProduct
AS
BEGIN
CREATE TABLE #product(
    ProductLotNumber VARCHAR(10),
    MaterialNumber INT,
    ProductionBatchNumber INT,
    ProcessOrder NUMERIC,
    ReceiverId INT,
    InspectionLotNumber BIGINT,
    SampleSubmitNumber CHAR(8),
    SampleDate DATE
);

BULK INSERT #product FROM '..\..\usr\dbfiles\BuildFiles\ProductData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );

    BEGIN TRAN
        BEGIN TRY

            INSERT INTO Distillation.Production(ProductLotNumber,MaterialNumber,ProductBatchNumber,ProcessOrder,ReceiverId,InspectionLotNumber)
            SELECT Distillation.UpdateProductId(ProductLotNumber),MaterialNumber,ProductionBatchNumber,ProcessOrder,ReceiverId,InspectionLotNumber
            FROM #product

            INSERT INTO QualityControl.SampleSubmit(SampleSubmitNumber,InspectionLotNumber,SampleDate)
            SELECT SampleSubmitNumber,InspectionLotNumber,SampleDate FROM #product

            UPDATE Distillation.Production
            SET SampleSubmitNumber = (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmit.InspectionLotNumber = Production.InspectionLotNumber)

        COMMIT TRAN;
        END TRY
        BEGIN CATCH
            THROW;
            ROLLBACK;
        END CATCH

END

