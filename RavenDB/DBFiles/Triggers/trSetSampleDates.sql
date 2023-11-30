CREATE OR ALTER TRIGGER QualityControl.SetSampleDates
ON QualityControl.SampleSubmit
AFTER UPDATE
AS

DECLARE @rejected AS BIT
SET @rejected = (SELECT inserted.Rejected
                    FROM inserted)

DECLARE @approved AS BIT
SET @approved = (SELECT inserted.Approved 
                    FROM inserted)

IF(@approved = 1)
    UPDATE QualityControl.SampleSubmit
    SET ReviewDate = GETDATE(),
        ExperiationDate = DATEADD(YEAR,1,GETDATE())
    WHERE SampleSubmitNumber = (SELECT inserted.SampleSubmitNumber FROM inserted);    

ELSE IF(@rejected = 1)

    UPDATE QualityControl.SampleSubmit
    SET ReviewDate = GETDATE()
    WHERE SampleSubmitNumber = (SELECT inserted.SampleSubmitNumber FROM inserted);    

