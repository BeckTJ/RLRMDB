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
    CarbonDrumRequired BIT,
    CarbonDrumWeight INT, 
    CarbonDrumDays INT,
    VacuumTrapRequired BIT,
    VacuumTrapDaysAllowed INT,
    SpecificGravity DECIMAL(3,2),
    PrefractionRefluxRatio VARCHAR(5),
    CollectRefluxRatio VARCHAR(5),
    NumberOfRuns INT,
    BatchManaged BIT,
    RequiresProcessOrder BIT,
    UnitOfIssue VARCHAR(2),
    IsRawMaterial BIT,
    Vendor VARCHAR(25),
    IsMPPS BIT,
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

            while(@index <= (select count(*)FROM #tempTbl))

            SET @materialNumber = (SELECT MaterialNumber FROM #tempTbl WHERE ID = @index)
            SET @materialName = (SELECT MaterialNameAbreviation FROM #tempTbl WHERE ID = @index)
            SET @parentMaterialNumber = (SELECT MaterialNumber FROM Materials.Material WHERE Material.MaterialNameAbreviation = @materialName)
            SET @batchManaged  = (SELECT BatchManaged FROM #tempTbl WHERE ID = @index)
            SET @requiresProcessOrder  = (SELECT RequiresProcessOrder FROM #tempTbl WHERE ID = @index)
            SET @unitofIssue  = (SELECT UnitOfIssue FROM #tempTbl WHERE ID = @index)
            SET @isRawMaterial  = (SELECT IsRawMaterial FROM #tempTbl WHERE ID = @index)
            set @index += 1

            INSERT INTO Materials.Material(MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,CarbonDrumRequired,CarbonDrumDaysAllowed,CarbonDrumWeightAllowed,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns)
            SELECT TOP(6) MaterialNumber,MaterialName,MaterialNameAbreviation,PermitNumber,CarbonDrumRequired,CarbonDrumDays,CarbonDrumWeight,VacuumTrapRequired,VacuumTrapDaysAllowed,SpecificGravity,PrefractionRefluxRatio,CollectRefluxRatio,NumberOfRuns
            FROM #tempTbl
            WHERE NOT EXISTS(SELECT * FROM Materials.Material WHERE Material.MaterialName = #tempTbl.MaterialName)
            
            EXEC Materials.InsertMaterialNumber @materialNumber,@parentMaterialNumber,@batchManaged,@requiresProcessOrder,@unitOfIssue,@isRawMaterial
        
            INSERT INTO Vendors.Vendor(VendorName,IsMPPS)
            SELECT DISTINCT Vendor,IsMPPS
            FROM #tempTbl
            WHERE NOT EXISTS(Select * FROM Vendors.Vendor WHERE Vendor.VendorName = #tempTbl.Vendor)

            INSERT INTO Materials.MaterialId(MaterialNumber, VendorName, MaterialCode, SequenceId)
            SELECT MaterialNumber,(SELECT VendorName FROM Vendors.Vendor WHERE VendorName = #tempTbl.Vendor),MaterialCode,
            (SELECT SequenceId FROM Distillation.ProductNumberSequence WHERE SequenceId = #tempTbl.SequenceId)
            FROM #tempTbl

            COMMIT TRAN;
        END TRY
        BEGIN CATCH
           ROLLBACK;
        END CATCH
END
