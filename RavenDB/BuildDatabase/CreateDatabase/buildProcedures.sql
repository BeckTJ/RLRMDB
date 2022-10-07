--PROCEDURES

CREATE OR ALTER PROCEDURE Vendors.AddVendor(@vendorName AS VARCHAR(25), @isMpps AS BIT)
AS
BEGIN

IF NOT EXISTS (SELECT VendorName FROM Vendors.Vendor WHERE VendorName = @vendorName)

INSERT INTO Vendors.Vendor(VendorName,IsMPPS)
VALUES (@vendorName, @isMpps);

END
GO

CREATE OR ALTER PROCEDURE Vendors.AddVendorBatch(@vendorName AS VARCHAR(25), @batchNumber AS VARCHAR(50))
AS
BEGIN TRAN VendorBatch
BEGIN TRY

INSERT INTO Vendors.VendorBatch(VendorName,VendorBatchNumber)
VALUES(@vendorName,@batchNumber);
COMMIT TRAN
END TRY
BEGIN CATCH
    ROLLBACK TRAN
END CATCH
GO

CREATE OR ALTER PROCEDURE Materials.MaterialInsert
    (@materialNumber AS INT,
    @materialName AS VARCHAR(50),
    @nameAbreviation AS VARCHAR(10),
    @permitNumber AS VARCHAR(25),
    @rawMaterialCode AS VARCHAR(3),
    @productCode AS VARCHAR(3),
    @carbonDrumRequired AS BIT,
    @carbonDrumDaysAllowed AS INT = NULL,
    @carbonDrumWeightAllowed AS INT = NULL,
    @batchManaged AS BIT,
    @requiresProcessOrder AS BIT,
    @unitOfIssue AS CHAR(2),
    @isRawMaterial AS BIT,
    @vendorName AS VARCHAR(25),
    @sequenceNumber AS INT)
AS
BEGIN TRAN MaterialInsert
BEGIN TRY 
INSERT INTO Materials.Material
    (MaterialName, MaterialNameAbreviation, PermitNumber, RawMaterialCode, ProductCode, CarbonDrumRequired, CarbonDrumDaysAllowed, CarbonDrumWeightAllowed)
VALUES(@materialName, @nameAbreviation, @permitNumber, @rawMaterialCode, @productCode, @carbonDrumRequired, @carbonDrumDaysAllowed, @carbonDrumWeightAllowed);

DECLARE @parentMaterialNumber AS INT
SET @parentMaterialNumber = (SELECT ParentMaterialNumber
FROM Materials.Material
WHERE MaterialName = @materialName);

INSERT INTO Materials.MaterialNumber
    (MaterialNumber, ParentMaterialNumber,  BatchManaged, RequiresProcessOrder, UnitOfIssue, IsRawMaterial)
VALUES(@materialNumber, @parentMaterialNumber,  @batchManaged, @requiresProcessOrder, @unitOfIssue, @isRawMaterial);

IF NOT EXISTS(SELECT VendorName
FROM Vendors.Vendor
WHERE VendorName = @vendorName)

BEGIN
    INSERT INTO Vendors.Vendor
        (VendorName)
    VALUES(@vendorName);
END

DECLARE @sequenceId AS INT
SET @sequenceId =(SELECT sequenceId
FROM Distillation.ProductNumberSequence
WHERE sequenceIdStart = @sequenceNumber);

DECLARE @currentSequenceId AS INT
SET @currentSequenceId =(SELECT sequenceIdStart
FROM Distillation.ProductNumberSequence
WHERE sequenceId = @sequenceId);

INSERT INTO Materials.MaterialId
    (MaterialNumber, VendorName, SequenceId, CurrentSequenceId)
VALUES(@materialNumber, @vendorName, @sequenceId, @currentSequenceId);
COMMIT;

END TRY
BEGIN CATCH
    THROW;
    ROLLBACK;
END CATCH
GO


CREATE OR ALTER PROCEDURE Distillation.RawMaterialUpdate
    (@materialNumber AS INT,
    @vendorName AS VARCHAR(25),
    @vendorBatchNumber AS VARCHAR(25),
    @drumWeight INT = NULL,
    @sapBatchNumber INT = NULL,
    @containerNumber CHAR(7)=NULL,
    @quantity AS INT = 1
)
AS
BEGIN TRAN EnterRawMaterial
BEGIN TRY

DECLARE @drumId AS CHAR(10)
SET @drumId = (Distillation.setDrumId(@materialNumber, @vendorName));

INSERT INTO Distillation.RawMaterial
    (DrumLotNumber, MaterialNumber, DrumWeight, SapBatchNumber, ContainerNumber, VendorBatchNumber, DateUsed)
VALUES
    (@drumId, @materialNumber, @drumWeight, @sapBatchNumber, @containerNumber, @vendorBatchNumber, GETDATE());

COMMIT TRAN;
END TRY
BEGIN CATCH
    ROLLBACK TRAN;
END CATCH
GO

--TRIGGERS

CREATE OR ALTER TRIGGER Distillation.IncrementSequenceId
ON Distillation.RawMaterial
AFTER INSERT,UPDATE

AS

DECLARE @vendorName AS INT
SET @vendorName = (select VendorName
                    From Vendors.VendorBatch
                    WHERE VendorBatchNumber = (SELECT inserted.VendorBatchNumber
                                    FROM inserted));
                                                
DECLARE @materialNumber AS INT

SET @materialNumber = (select top(1)
    inserted.MaterialNumber
FROM inserted);

DECLARE @currentId AS INT
SET @currentId = (SELECT CurrentSequenceId
FROM MaterialId
WHERE VendorName = @vendorName AND MaterialNumber = @materialNumber);

IF @currentId = (SELECT SequenceIdEnd
                FROM Distillation.ProductNumberSequence
                    JOIN MaterialId on ProductNumberSequence.SequenceId = MaterialId.SequenceId
                WHERE VendorName = @vendorName AND MaterialNumber = @materialNumber)
SET @currentId = (SELECT sequenceIdStart FROM Distillation.ProductNumberSequence
                    JOIN MaterialId on ProductNumberSequence.SequenceId = MaterialId.SequenceId
                WHERE VendorName = @vendorName AND MaterialNumber = @materialNumber)

ELSE
SET @currentId = (@currentId + 1)


UPDATE MaterialId 
SET CurrentsequenceId = (@currentId)
WHERE VendorName = @vendorName AND MaterialNumber = @materialNumber;
GO

CREATE OR ALTER TRIGGER QualityControl.SetSampleDates
ON QualityControl.SampleSubmit
AFTER UPDATE
AS

DECLARE @rejected AS BIT
SET @rejected = (SELECT inserted.Rejected
                    FROM inserted)

IF(@rejected = 0)
    UPDATE QualityControl.SampleSubmit
    SET ApprovalDate = GETDATE(),
        ExperiationDate = DATEADD(YEAR,1,GETDATE())
    WHERE Rejected = @rejected;    

ELSE IF(@rejected = 1)

    UPDATE QualityControl.SampleSubmit
    SET RejectedDate = GETDATE()
    WHERE Rejected = @rejected;
GO

--FUNCTIONS
CREATE or ALTER FUNCTION Distillation.SetDrumId
    (@materialNumber AS INT,
    @vendorName AS CHAR(20))
    RETURNS CHAR(10) 
    AS 
BEGIN

    DECLARE @alphabeticDate AS CHAR(1)
    SET @alphabeticDate = (SELECT AlphabeticCode
    FROM AlphabeticDate
    WHERE MonthNumber = MONTH(GETDATE()));

    DECLARE @drumId AS CHAR(10)
    SET @drumId =(
    SELECT CONCAT(FORMAT(MaterialId.CurrentSequenceId,'000'), Material.RawMaterialCode,RIGHT(YEAR(GETDATE()),1),@alphabeticDate,FORMAT(GETDATE(),'dd')) AS 'Drum ID'
    FROM Materials.MaterialNumber
        JOIN Materials.MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
        JOIN Materials.Material ON Material.ParentMaterialNumber = MaterialNumber.ParentMaterialNumber
        JOIN Vendors.Vendor ON Vendor.VendorName = MaterialId.VendorName
    WHERE MaterialNumber.MaterialNumber = @materialNumber
        AND Vendor.VendorName = @vendorName)

    RETURN @drumId
END
GO

CREATE OR ALTER FUNCTION Materials.SpecificGravity(@materialName AS CHAR(20), @WeightLiters AS DECIMAL(5,2))
RETURNS DECIMAL 
AS
BEGIN

DECLARE @weightKG AS DECIMAL(5,3)
SET @weightKG = (@weightLiters * (SELECT SpecificGravity 
                                    FROM Materials.Material
                                    WHERE MaterialNameAbreviation = @materialName ));

RETURN @weightKG;
END
GO

CREATE OR ALTER FUNCTION HumanResources.EmployeeInitials(@employeeId AS CHAR(7) = 'NA')
RETURNS CHAR(2)
AS
BEGIN
DECLARE @employeeInit AS CHAR(2)
SET @employeeInit = (CONCAT(Left(1,(SELECT FirstName FROM HumanResources.Employee WHERE EmployeeId = @employeeId)),Left(1,(SELECT LastName FROM HumanResources.Employee WHERE EmployeeId = @employeeId))))

RETURN @employeeInit;
END
GO

CREATE OR ALTER FUNCTION Materials.SpecificGravity(@materialName AS CHAR(20), @WeightLiters AS DECIMAL(5,2))
RETURNS DECIMAL 
AS
BEGIN

DECLARE @weightKG AS DECIMAL(5,3)
SET @weightKG = (@weightLiters * (SELECT SpecificGravity 
                                    FROM Materials.Material
                                    WHERE MaterialNameAbreviation = @materialName ));

RETURN @weightKG;
END
GO
