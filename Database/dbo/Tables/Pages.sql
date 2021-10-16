CREATE TABLE [dbo].[Pages] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (250) NULL,
    [Url]   NVARCHAR (250) NULL,
    [Icon]  NVARCHAR (250) NULL,
    [Order] INT            NULL,
    CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED ([Id] ASC)
);

