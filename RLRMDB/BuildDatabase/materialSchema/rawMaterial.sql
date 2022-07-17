CREATE TABLE [dbo].[rawMaterial] (
    [drumLotNumber]      CHAR (10) NOT NULL,
    [materialNumber]     INT       NULL,
    [productBatchNumber] INT       NULL,
    [quantity]           INT       NULL,
    [containerNumber]    CHAR (7)  NULL,
    [sampleSubmitNumber] CHAR (8)  NULL,
    [processOrder]       INT       NULL,
    [vendorId]           INT       NULL,
    [vendorBatchNumber]  INT       NULL,
    [vendorLotNumber]    CHAR (25) NULL,
    PRIMARY KEY CLUSTERED ([drumLotNumber] ASC),
    FOREIGN KEY ([materialNumber]) REFERENCES [dbo].[material] ([materialNumberId]),
    FOREIGN KEY ([sampleSubmitNumber]) REFERENCES [dbo].[qualityControl] ([sampleSubmitNumber]),
    FOREIGN KEY ([vendorId]) REFERENCES [dbo].[vendor] ([vendorId])
);


GO

