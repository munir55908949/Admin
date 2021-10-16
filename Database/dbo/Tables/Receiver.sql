CREATE TABLE [dbo].[Receiver] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Receiver] PRIMARY KEY CLUSTERED ([Id] ASC)
);

