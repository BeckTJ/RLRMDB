
CREATE OR ALTER PROCEDURE Distillation.RawMaterialUpdate
    (@materialNumber AS INT,
    @vendorBatchNumber AS VARCHAR(25) = NULL,
    @inspectionLotNumber NUMERIC = NULL,
    @sapBatchNumber INT = NULL,
    @sampleSubmitNumber CHAR(8) = NULL,
    @containerNumber CHAR(7) = NULL,
    @quantity AS INT = 1,
    @drumWeight AS DECIMAL(6,2) = NULL
    )
    AS
    BEGIN TRAN EnterRawMaterial
    BEGIN TRY

    DECLARE @vendor VARCHAR(50)
    SET @vendor = (SELECT VendorName FROM Vendors.VendorBatch
                    WHERE VendorBatchNumber = @vendorBatchNumber)
                    
    WHILE(@quantity > 0)
        BEGIN
        INSERT INTO Distillation.RawMaterial
            (DrumLotNumber, MaterialNumber, DrumWeight, SapBatchNumber, ContainerNumber, VendorBatchNumber)
        VALUES
            (Distillation.SetDrumId(@materialNumber,@vendor), @materialNumber, @drumWeight, @sapBatchNumber, @containerNumber, @vendorBatchNumber)

          SET @quantity=@quantity-1
        END
        
    COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
    END CATCH
