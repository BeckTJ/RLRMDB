CREATE TABLE [dbo].[qualityControl] (
    [sampleSubmitNumber]  CHAR (8) NOT NULL,
    [inspectionLotNumber] INT      NULL,
    [rejected]            INT      NULL,
    [rejectedDate]        DATE     NULL,
    [approvalDate]        DATE     NULL,
    [experiationDate]     DATE     NULL,
    PRIMARY KEY CLUSTERED ([sampleSubmitNumber] ASC)
);


GO

