CREATE OR ALTER TRIGGER QualityControl.SetSampleDates
ON QualityControl.SampleSubmit
AFTER UPDATE
AS

DECLARE @rejected AS BIT
SET @rejected = (SELECT inserted.Rejected
                    FROM inserted)

IF(@rejected = 0)
    UPDATE QualityControl.SampleSubmit
    SET ApprovalDate = GETDATE(),
        ExperiationDate = DATEADD(YEAR,1,GETDATE())
    WHERE Rejected = @rejected;    

ELSE IF(@rejected = 1)

    UPDATE QualityControl.SampleSubmit
    SET RejectedDate = GETDATE()
    WHERE Rejected = @rejected;

