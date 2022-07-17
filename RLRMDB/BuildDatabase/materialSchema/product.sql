CREATE TABLE [dbo].[product] (
    [productLotNumber]      CHAR (10) NOT NULL,
    [materialNumber]        INT       NULL,
    [productionBatchNumber] INT       NULL,
    [processOrder]          INT       NULL,
    [sampleSubmitNumber]    CHAR (8)  NULL,
    [quantity]              INT       NULL,
    PRIMARY KEY CLUSTERED ([productLotNumber] ASC),
    FOREIGN KEY ([materialNumber]) REFERENCES [dbo].[material] ([materialNumberId]),
    FOREIGN KEY ([sampleSubmitNumber]) REFERENCES [dbo].[qualityControl] ([sampleSubmitNumber])
);


GO

