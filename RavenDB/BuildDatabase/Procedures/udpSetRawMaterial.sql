CREATE OR ALTER PROCEDURE Distillation.SetRawMaterial(
    @materialNumber INT,
    @vendorBatchNumber VARCHAR(25) = NULL,
    @inspectionLotNumber NUMERIC = NULL,
    @sapBatchNumber INT = NULL,
    @sampleSubmitNumber CHAR(8),
    @containerNumber CHAR(7) = NULL,
    @numberOfDrums INT = 1,
    @drumWeight DECIMAL(6,2) = NULL,
    @sampleDate DATE
)
AS

    DECLARE @vendorName AS VARCHAR(25)
    SET @vendorName = (SELECT VendorName FROM Materials.MaterialId WHERE MaterialNumber = @materialNumber)

    EXEC QualityControl.SubmitSample @sampleSubmitNumber,@inspectionlotNumber,@sampleDate;
    EXEC Vendors.AddVendorBatch @vendorName, @vendorBatchNumber, @materialNumber, @numberOfDrums;
    EXEC Distillation.RawMaterialUpdate @materialNumber, @vendorBatchNumber,@inspectionLotNumber,@sapBatchNumber,@sampleSubmitNumber,@sampleDate,@containerNumber,@numberOfDrums,@drumWeight
