    DROP table #rawMaterial
    go

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

    DECLARE @total INT
    SET @total = (SELECT count(VendorBatchNumber) FROM #rawMaterial WHERE vendorbatchNumber = #rawMaterial.vendorBatchNumber)

            INSERT INTO Vendors.VendorBatch(VendorBatchNumber,VendorName,Quantity,MaterialNumber)
            SELECT DISTINCT VendorBatchNumber,
                (SELECT Vendor.VendorName FROM Vendors.Vendor 
                JOIN Materials.MaterialId ON Vendor.VendorName = MaterialId.VendorName
                WHERE MaterialId.MaterialNumber = #rawMaterial.MaterialNumber),
                @total,
                MaterialNumber
            FROM #rawMaterial
            WHERE NOT EXISTS(SELECT VendorBatchNumber FROM Vendors.VendorBatch WHERE VendorBatch.VendorBatchNumber = #rawMaterial.VendorBatchNumber) AND VendorBatchNumber != NULL 

SELECT * From Vendors.VendorBatch