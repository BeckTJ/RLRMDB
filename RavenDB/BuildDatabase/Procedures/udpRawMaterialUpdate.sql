
CREATE OR ALTER PROCEDURE Distillation.RawMaterialUpdate
    (@materialNumber AS INT,
    @vendorName AS VARCHAR(25),
    @vendorBatchNumber AS VARCHAR(25) = NULL,
    @drumWeight INT = NULL,
    @sapBatchNumber INT = NULL,
    @containerNumber CHAR(7) = NULL,
    @quantity AS INT = 1
)
AS
BEGIN TRAN EnterRawMaterial
BEGIN TRY

DECLARE @drumId AS CHAR(10)
SET @drumId = (Distillation.setDrumId(@materialNumber, @vendorName));

DECLARE @batchId AS INT
SET @batchId = (SELECT BatchId FROM Vendors.VendorBatch WHERE VendorBatchNumber = @vendorBatchNumber);

INSERT INTO Materials.RawMaterial
    (DrumLotNumber, MaterialNumber, DrumWeight, SapBatchNumber, ContainerNumber, VendorBatchId, DateUsed)
VALUES
    (@drumId, @materialNumber, @drumWeight, @sapBatchNumber, @containerNumber, @batchId, GETDATE());

COMMIT TRAN;
END TRY
BEGIN CATCH
    ROLLBACK TRAN;
END CATCH
