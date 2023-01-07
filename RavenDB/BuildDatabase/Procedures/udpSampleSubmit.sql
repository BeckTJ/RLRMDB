CREATE OR ALTER PROCEDURE QualityControl.SubmitSample
    (@sampleNumber AS CHAR(8), @lotNumber AS NUMERIC, @sampleDate DATE)
AS

PRINT 'Maybe next time'

INSERT INTO QualityControl.SampleSubmit(SampleSubmitNumber, InspectionLotNumber, SampleDate)
VALUES
    (@sampleNumber, @lotNumber, @sampleDate);

print 'or not'
