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
    FROM MaterialNumber
        JOIN MaterialId ON MaterialNumber.MaterialNumber = MaterialId.MaterialNumber
        JOIN Material ON Material.MaterialNameId = MaterialNumber.MaterialNameId
        JOIN Vendor ON Vendor.VendorId = MaterialId.VendorId
    WHERE MaterialNumber.MaterialNumber = @materialNumber
        AND Vendor.vendorName = @vendorName)

    RETURN @drumId
END

