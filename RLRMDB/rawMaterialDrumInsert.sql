--Both work seperately need to link them togeather


BEGIN TRAN
BEGIN TRY

EXEC rawMaterialUpdate 2302328,'Sivance','123ABC9999',150,NULL,NULL,NULL,10;

EXEC sampleSubmit 'test0099',NULL;


COMMIT TRAN
END TRY
BEGIN CATCH
    PRINT 'Transaction Failed.';
END CATCH

SELECT *
FROM qualityControl

SELECT *
FROM vendorBatchInformation

SELECT *
FROM RawMaterial