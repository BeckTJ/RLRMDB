CREATE TABLE [dbo].[vendor] (
    [vendorId]   INT       IDENTITY (1, 1) NOT NULL,
    [vendorName] CHAR (25) NOT NULL,
    PRIMARY KEY CLUSTERED ([vendorId] ASC)
);


GO

