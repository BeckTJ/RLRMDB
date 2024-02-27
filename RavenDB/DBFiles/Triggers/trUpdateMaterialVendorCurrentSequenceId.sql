CREATE OR ALTER TRIGGER Materials.UpdateMaterialVendorCurrentSequenceId
ON Distillation.RawMaterial
AFTER INSERT 
AS

DECLARE @materialNumber INT
DECLARE @startId INT
DECLARE @currentId INT
DECLARE @totalRecords INT
DECLARE @newId INT

SET @materialNumber = (SELECT inserted.MaterialNumber FROM inserted);
SET @startId = (SELECT SequenceId FROM Materials.MaterialVendor
                    WHERE MaterialNumber = @materialNumber);
SET @currentId = (SELECT CurrentSequenceId FROM Materials.MaterialVendor
                    WHERE MaterialNumber = @materialNumber);
SET @totalRecords = (SELECT TotalRecords FROM Materials.MaterialVendor
                    WHERE MaterialNumber = @materialNumber);
SET @newId = @currentId + 1;

IF(@newId = (@startId + @totalRecords))
    BEGIN
        UPDATE Materials.MaterialVendor
        SET CurrentSequenceId = @startId
        WHERE MaterialNumber = @materialNumber;
    END
ELSE
    BEGIN
        UPDATE Materials.MaterialVendor
        SET CurrentSequenceId = @currentId + 1
        WHERE MaterialNumber = @materialNumber;
    END