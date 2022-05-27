
CREATE OR ALTER PROCEDURE Distillation.RawMaterialUpdate
    (@materialNumber AS INT,
    @vendorName AS VARCHAR(25),
    @vendorBatchNumber AS VARCHAR(25),
    @drumWeight INT,
    @sapBatchNumber INT,
    @containerNumber CHAR(7),
    @quantity AS INT
)
AS
BEGIN TRAN EnterRawMaterial
BEGIN TRY

DECLARE @drumId AS CHAR(10)
SET @drumId = (Distillation.setDrumId(@materialNumber, @vendorName));

DECLARE @batchId AS INT
SET @batchId = (SELECT BatchId FROM Vendors.VendorBatch WHERE VendorBatchNumber = @vendorBatchNumber);

INSERT INTO Materials.RawMaterialLog
    (DrumLotNumber, MaterialNumber, DrumWeight, SapBatchNumber, ContainerNumber, VendorBatchId, DateUsed)
VALUES
    (@drumId, @materialNumber, @drumWeight, @sapBatchNumber, @containerNumber, @batchId, GETDATE());

COMMIT TRAN;
END TRY
BEGIN CATCH
    ROLLBACK TRAN;
END CATCH
