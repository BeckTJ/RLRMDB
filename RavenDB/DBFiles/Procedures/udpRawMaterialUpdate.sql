CREATE OR ALTER PROCEDURE Distillation.RawMaterialUpdate
    (@materialNumber AS INT,
    @vendorBatchNumber AS VARCHAR(25) = NULL,
    @inspectionLotNumber NUMERIC = NULL,
    @sapBatchNumber INT = NULL,
    @sampleSubmitNumber CHAR(8) = NULL,
    @sampleDate DATE = NULL,
    @containerNumber CHAR(7) = NULL,
    @quantity INT = 1,
    @drumWeight DECIMAL(6,2) = NULL
    )
    AS
    DECLARE @vendor VARCHAR(50)
    SET @vendor = (SELECT VendorName FROM Vendors.VendorBatch
                    WHERE VendorBatchNumber = @vendorBatchNumber)
                    
    WHILE(@quantity > 0)
        BEGIN
        INSERT INTO Distillation.RawMaterial
            (DrumLotNumber, MaterialNumber, DrumWeight, SapBatchNumber, ContainerNumber, VendorBatchNumber, SampleSubmitNumber)
        VALUES
            (Distillation.SetDrumId(@materialNumber,@vendor,@sampleDate), @materialNumber, @drumWeight, @sapBatchNumber, @containerNumber, @vendorBatchNumber,@sampleSubmitNumber)
            
          SET @quantity=@quantity-1

        END
