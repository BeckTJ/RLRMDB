CREATE OR ALTER PROCEDURE InsertRawMaterial
AS
BEGIN

    CREATE TABLE #rawMaterial(
        DrumLotNumber VARCHAR(10),
        MaterialNumber INT,
        SapBatchNumber INT,
        ContainerNumber CHAR(7),
        SampleSubmitNumber CHAR(8),
        VendorBatchNumber VARCHAR(25),
        SampleDate DATE,
        ApprovalDate DATE,
        RejectedDate DATE,
        EmployeeId CHAR(7)
    )

    BULK INSERT #rawMaterial FROM '..\..\usr\dbfiles\BuildFiles\RawMaterialData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    )
    BEGIN TRAN
        BEGIN TRY

            DECLARE @rejected BIT
            IF(SELECT RejectedDate FROM #rawMaterial)!= NULL
                BEGIN
                SET @rejected = 1
                END
            
            DECLARE @total INT
            SET @total = (SELECT count(*) FROM #rawMaterial WHERE vendorbatchNumber = #rawMaterial.vendorBatchNumber)

            INSERT INTO QualityControl.SampleSubmit(SampleSubmitNumber,SampleDate,Rejected,RejectedDate,ApprovalDate)
            SELECT DISTINCT SampleSubmitNumber,SampleDate,@rejected,RejectedDate,ApprovalDate
            FROM #rawMaterial
            WHERE NOT EXISTS(SELECT * FROM QualityControl.SampleSubmit WHERE SampleSubmit.SampleSubmitNumber = #rawMaterial.SampleSubmitNumber)

            IF(SELECT VendorBatchNumber FROM #rawMaterial WHERE VendorBatchNumber = #rawMaterial.VendorBatchNumber) != NULL
                BEGIN
                    INSERT INTO Vendors.VendorBatch(VendorBatchNumber,VendorName,Quantity,MaterialNumber)
                    SELECT DISTINCT VendorBatchNumber,
                        (SELECT Vendor.VendorName FROM Vendors.Vendor 
                        JOIN Materials.MaterialId ON Vendor.VendorName = MaterialId.VendorName
                        WHERE MaterialId.MaterialNumber = #rawMaterial.MaterialNumber),
                        @total,
                        MaterialNumber
                    FROM #rawMaterial
                    WHERE NOT EXISTS(SELECT * FROM Vendors.VendorBatch WHERE VendorBatch.VendorBatchNumber = #rawMaterial.VendorBatchNumber)
                END

            INSERT INTO Distillation.RawMaterial(DrumLotNumber,MaterialNumber,SapBatchNumber,ContainerNumber,SampleSubmitNumber,VendorBatchNumber)
            SELECT DrumLotNumber,MaterialNumber,SapBatchNumber, ContainerNumber,
            (SELECT SampleSubmitNumber FROM QualityControl.SampleSubmit WHERE SampleSubmit.SampleSubmitNumber = #rawMaterial.SampleSubmitNumber),
            (SELECT VendorBatchNumber FROM Vendors.VendorBatch WHERE VendorBatch.VendorBatchNumber = #rawMaterial.VendorBatchNumber)
            FROM #rawMaterial

            COMMIT TRAN;
        END TRY
    BEGIN CATCH
        THROW;
        ROLLBACK;
    END CATCH
END
