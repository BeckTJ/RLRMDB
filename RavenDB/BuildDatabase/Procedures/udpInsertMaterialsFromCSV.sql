CREATE OR ALTER PROCEDURE MaterialInsertDB
AS
BEGIN
CREATE TABLE #tempTbl(
    MaterialName VARCHAR(50),
    MaterialNameAbreviation VARCHAR(15),
    MaterialNumber INT,
    PermitNumber VARCHAR(25),
    RawMaterialCode VARCHAR(3),
    ProductCode VARCHAR(3),
    CarbonDrumRequired BIT,
    CarbonDrumWeight INT, 
    CarbonDrumDays INT,
    SpecificGravity DECIMAL(3,2),
    PrefractionRefluxRatio VARCHAR(5),
    CollectRefluxRatio VARCHAR(5),
    NumberOfRuns INT,
    BatchManaged BIT,
    RequiresProcessOrder BIT,
    UnitOfIssue VARCHAR(2),
    IsRawMaterial BIT,
    Vendor VARCHAR(25),
    SequenceId INT);

BULK INSERT #tempTbl FROM '..\..\tmp\MaterialData.csv'
    WITH(
        FORMAT = 'csv',
        FIRSTROW = 2,
        FIELDTERMINATOR = ',',
        ROWTERMINATOR = '\n',
        KEEPNULLS
    );
    BEGIN TRAN 
        BEGIN TRY
            
            INSERT INTO Materials.Material(ParentMaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,RawMaterialCode,ProductCode,CarbonDrumRequired,CarbonDrumDaysAllowed,CarbonDrumWeightAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns)
            SELECT TOP(6) MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,RawMaterialCode,ProductCode,CarbonDrumRequired,CarbonDrumDays,CarbonDrumWeight,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns
            FROM #tempTbl
            WHERE NOT EXISTS(SELECT * FROM Materials.Material WHERE Material.MaterialName = #tempTbl.MaterialName)
            
            INSERT INTO Materials.MaterialNumber(MaterialNumber,ParentMaterialNumber,BatchManaged,RequiresProcessOrder,UnitOfIssue,IsRawMaterial)
            SELECT MaterialNumber,(Select ParentMaterialNumber FROM Materials.Material WHERE Material.MaterialName = #tempTbl.MaterialName),BatchManaged,RequiresProcessOrder,UnitOfIssue,IsRawMaterial
            FROM #tempTbl
            WHERE NOT EXISTS(SELECT * FROM Materials.MaterialNumber WHERE MaterialNumber.MaterialNumber = #tempTbl.MaterialNumber)

        
            INSERT INTO Vendors.Vendor(VendorName)
            SELECT DISTINCT Vendor
            FROM #tempTbl
            WHERE NOT EXISTS(Select * FROM Vendors.Vendor WHERE Vendor.VendorName = #tempTbl.Vendor)

            INSERT INTO Materials.MaterialId(MaterialNumber, VendorName, CurrentSequenceId, SequenceId)
            SELECT MaterialNumber,(SELECT VendorName FROM Vendors.Vendor WHERE VendorName = #tempTbl.Vendor),SequenceId,(SELECT SequenceId FROM Distillation.ProductNumberSequence WHERE SequenceIdStart = #tempTbl.SequenceId)
            FROM #tempTbl


            COMMIT TRAN;
        END TRY
        BEGIN CATCH
           ROLLBACK;
        END CATCH
END
