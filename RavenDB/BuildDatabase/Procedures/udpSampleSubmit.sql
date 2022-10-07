CREATE OR ALTER PROCEDURE QualityControl.SubmitSample
    (@sampleNumber AS CHAR(8),
    @lotNumber AS NUMERIC)
AS

DECLARE @drumId AS VARCHAR(10)
SET @drumId =(SELECT TOP(1)
    DrumLotNumber
FROM RawMaterial
ORDER BY DrumLotNumber DESC);

DECLARE @batchId AS INT
SET @batchId = (SELECT VendorBatchId
FROM RawMaterial
WHERE DrumLotNumber = @drumId);

INSERT INTO qualityControl
    (SampleSubmitNumber, InspectionLotNumber, VendorBatchId)
VALUES
    (@sampleNumber, @lotNumber, @batchId);

UPDATE RawMaterial
SET SampleSubmitNumber = @sampleNumber
WHERE DrumLotNumber = @drumId;
