CREATE OR ALTER PROCEDURE Distillation.IncrementSequenceId(@materialNumber INT, @vendor AS VARCHAR(25) = 'Finished Product')
AS
                                                
DECLARE @sequenceId INT
DECLARE @maxSequenceId INT
DECLARE @minSequenceId INT
DECLARE @setSequenceId INT
DECLARE @vendorId INT

SET @vendorId = (SELECT VendorId
                FROM Vendors.Vendor
                WHERE VendorName = @vendor)

SET @sequenceId =(SELECT CurrentSequenceId 
                FROM Materials.MaterialId
                WHERE MaterialId.MaterialNumber = @materialNumber AND MaterialId.VendorId = @vendorId)

SET @maxSequenceId = (SELECT SequenceIdEnd FROM Materials.MaterialId 
                        JOIN Distillation.ProductNumberSequence on MaterialId.SequenceId = ProductNumberSequence.SequenceId
                        WHERE materialNumber = @materialNumber and VendorId = @vendorId)

SET @minSequenceId = (SELECT SequenceIdStart
                        FROM Materials.MaterialId 
                        JOIN Distillation.ProductNumberSequence on MaterialId.SequenceId = ProductNumberSequence.SequenceId
                        WHERE materialNumber = @materialNumber and VendorId = @vendorId)

IF @setSequenceId = @maxSequenceId

SET @setSequenceId = @minSequenceID

ELSE
SET @setSequenceId = (@sequenceId + 1)

    UPDATE Materials.MaterialId
    SET CurrentSequenceId = (@setSequenceId)
    WHERE MaterialId.MaterialNumber = @materialNumber AND MaterialId.VendorId = @vendorId
