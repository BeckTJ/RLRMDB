CREATE or ALTER FUNCTION setDrumId
    (@materialNumber AS INT,
    @vendorName AS CHAR(20))
    RETURNS CHAR(10) 
    AS 
BEGIN

    DECLARE @alphabeticDate AS CHAR(1)
    SET @alphabeticDate = (SELECT alphabeticCode
    FROM alphabeticDate
    WHERE monthNumber = MONTH(GETDATE()));

    DECLARE @drumId AS CHAR(10)
    SET @drumId =(
    SELECT CONCAT(FORMAT(materialId.currentSequenceId,'000'), materialName.rawMaterialCode,RIGHT(YEAR(GETDATE()),1),@alphabeticDate,FORMAT(GETDATE(),'dd')) AS 'Drum ID'
    FROM materialNumber
        JOIN materialId ON materialNumber.materialNumber = materialId.materialNumber
        JOIN materialName ON materialName.materialNameId = materialNumber.materialNameId
        JOIN vendor ON vendor.vendorId = materialId.vendorId
    WHERE materialNumber.materialNumber = @materialNumber
        AND vendor.vendorName = @vendorName)

    RETURN @drumId
END

