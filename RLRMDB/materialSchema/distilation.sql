CREATE TABLE [dbo].[distilation] (
    [productLotNumber]  CHAR (25) NOT NULL,
    [drumLotNumber]     CHAR (10) NULL,
    [rawMaterialLoaded] INT       NULL,
    [prefraction]       INT       NULL,
    [heels]             INT       NULL,
    [heelsPumped]       INT       NULL,
    [runNumber]         INT       NULL,
    [startDate]         DATETIME  NULL,
    [endDate]           DATETIME  NULL,
    PRIMARY KEY CLUSTERED ([productLotNumber] ASC),
    FOREIGN KEY ([drumLotNumber]) REFERENCES [dbo].[rawMaterial] ([drumLotNumber])
);


GO

