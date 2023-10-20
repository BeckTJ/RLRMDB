CREATE OR ALTER PROCEDURE MaterialInsertDB
AS
BEGIN
CREATE TABLE #tempTbl(
    Id INT IDENTITY(1,1),
    MaterialName VARCHAR(50),
    MaterialNameAbreviation VARCHAR(15),
    MaterialNumber INT,
    PermitNumber VARCHAR(25),
    MaterialCode VARCHAR(3),
    BatchManaged BIT,
    UnitOfIssue VARCHAR(2),
    SequenceId INT);

BULK INSERT #tempTbl FROM '..\..\usr\dbfiles\BuildFiles\MaterialData.csv'
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
            DECLARE @parentMaterialNumber INT
            DECLARE @batchManaged BIT
            DECLARE @requiresProcessOrder BIT
            DECLARE @unitofIssue CHAR(2)
            DECLARE @isRawMaterial BIT
            DECLARE @materialName VARCHAR(10)
            DECLARE @count INT
            DECLARE @index INT

            set @index = 1

            INSERT INTO Materials.Material(MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,CarbonDrumRequired,CarbonDrumDaysAllowed,CarbonDrumWeightAllowed,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns)
            SELECT TOP(6) MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,CarbonDrumRequired,CarbonDrumDays,CarbonDrumWeight,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns
            FROM #tempTbl
            WHERE NOT EXISTS(SELECT * FROM Materials.Material WHERE Material.MaterialName = #tempTbl.MaterialName)


            while(@index <= (select count(*)FROM #tempTbl))
            BEGIN
                SET @materialNumber = (SELECT MaterialNumber FROM #tempTbl WHERE ID = @index)
                SET @materialName = (SELECT MaterialNameAbreviation FROM #tempTbl WHERE ID = @index)
                SET @parentMaterialNumber = (SELECT MaterialNumber FROM Materials.Material WHERE Material.MaterialNameAbreviation = @materialName)
                SET @batchManaged  = (SELECT BatchManaged FROM #tempTbl WHERE ID = @index)
                SET @requiresProcessOrder  = (SELECT RequiresProcessOrder FROM #tempTbl WHERE ID = @index)
                SET @unitofIssue  = (SELECT UnitOfIssue FROM #tempTbl WHERE ID = @index)
                SET @isRawMaterial  = (SELECT IsRawMaterial FROM #tempTbl WHERE ID = @index)
                set @index += 1

                EXEC Materials.InsertMaterialNumber @materialNumber,@parentMaterialNumber,@batchManaged,@requiresProcessOrder,@unitOfIssue,@isRawMaterial
            END

            INSERT INTO Materials.Vendor(VendorName)
            SELECT DISTINCT Vendor
            FROM #tempTbl
            WHERE NOT EXISTS(Select * FROM Materials.Vendor WHERE Vendor.VendorName = #tempTbl.Vendor)

            INSERT INTO Materials.MaterialId(MaterialNumber, VendorName, MaterialCode, SequenceId, TotalRecords)
            SELECT MaterialNumber,(SELECT VendorName FROM Materials.Vendor WHERE VendorName = #tempTbl.Vendor),MaterialCode, SequenceId, 100
            FROM #tempTbl

            COMMIT TRAN;
        END TRY
        BEGIN CATCH
           ROLLBACK;
        END CATCH
END