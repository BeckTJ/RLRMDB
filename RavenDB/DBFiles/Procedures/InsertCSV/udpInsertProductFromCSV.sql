CREATE OR ALTER PROCEDURE Distillation.InsertProduct
AS
BEGIN
CREATE TABLE #product(
    MaterialNumber INT,
    ProductionBatchNumber INT,
    ProcessOrder NUMERIC,
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

        DECLARE @materialNumber INT
        DECLARE @processOrder NUMERIC
        DECLARE @vendorBatchNumber VARCHAR(25)
        DECLARE @inspectionLotNumber NUMERIC
        DECLARE @sapBatchNumber INT
        DECLARE @sampleSubmitNumber CHAR(8)
        DECLARE @sampleDate DATE
        DECLARE @rows INT
        DECLARE @index INT

            EXEC QualityControl.SubmitSample @sampleSubmitNumber,@inspectionLotNumber,@sampleDate
            EXEC Distillation.UpdateProduction @materialNumber, @sapBatchNumber, @processOrder, @inspectionLotNumber, @sampleSubmitNumber

        COMMIT TRAN;
        END TRY
        BEGIN CATCH
            THROW;
            ROLLBACK;
        END CATCH

END

