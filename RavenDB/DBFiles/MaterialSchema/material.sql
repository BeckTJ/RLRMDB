CREATE TABLE [dbo].[material] (
    [materialNumberId]      INT       IDENTITY (1, 1) NOT NULL,
    [materialNumber]        INT       NOT NULL,
    [chemicalName]          CHAR (25) NOT NULL,
    [productAlphabeticCode] CHAR (25) NULL,
    [batchManaged]          INT       NULL,
    [requiresProcessOrder]  INT       NULL,
    [weightType]            CHAR (2)  NULL,
    PRIMARY KEY CLUSTERED ([materialNumberId] ASC)
);


GO

