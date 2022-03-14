CREATE OR ALTER FUNCTION verifyDrum
    (@vendorBatchNumber AS INT, 
    @vendorLotNumber AS CHAR(10))

RETURNS INT
AS
BEGIN


    DECLARE @rejected AS INT
    SET @rejected = (SELECT rejected
    FROM qualityControl
    WHERE vendorLotNumber = @vendorLotNumber AND vendorBatchNumber = @vendorBatchNumber);

    DECLARE @expDate AS DATE
    SET @expDate = (SELECT experiationDate
    FROM qualityControl
    WHERE vendorLotNumber = @vendorLotNumber AND vendorBatchNumber = @vendorBatchNumber);

    IF (@rejected = 0 AND @expDate <= GETDATE())
        RETURN 1
        ELSE
        RETURN 0
    RETURN 1
END