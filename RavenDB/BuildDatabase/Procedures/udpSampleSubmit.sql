CREATE OR ALTER PROCEDURE QualityControl.SubmitSample
    (@sampleNumber AS CHAR(8), @lotNumber AS NUMERIC, @sampleDate DATE)
AS

INSERT INTO QualityControl.SampleSubmit(SampleSubmitNumber, InspectionLotNumber, SampleDate)
VALUES
    (@sampleNumber, @lotNumber, @sampleDate);
