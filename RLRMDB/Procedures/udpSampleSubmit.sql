CREATE OR ALTER PROCEDURE sampleSubmit
    (@sampleNumber AS CHAR(8),
    @lotNumber AS NUMERIC)
AS

DECLARE @drumId AS VARCHAR(10)
SET @drumId =(SELECT TOP(1)
    drumLotNumber
FROM rawMaterial
ORDER BY drumLotNumber DESC);

DECLARE @batchId AS INT
SET @batchId = (SELECT vendorBatchId
FROM rawMaterial
WHERE drumLotNumber = @drumId);

INSERT INTO qualityControl
    (sampleSubmitNumber, inspectionLotNumber, vendorBatchId)
VALUES
    (@sampleNumber, @lotNumber, @batchId);

UPDATE rawMaterial
SET sampleSubmitNumber = @sampleNumber
WHERE drumLotNumber = @drumId;
