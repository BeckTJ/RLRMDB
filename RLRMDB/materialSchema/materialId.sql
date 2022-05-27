CREATE TABLE [dbo].[materialId] (
    [id]               INT IDENTITY (1, 1) NOT NULL,
    [materialNumberId] INT NULL,
    [vendorId]         INT NULL,
    [sequenceId]       INT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);


GO

