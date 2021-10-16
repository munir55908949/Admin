CREATE TABLE [dbo].[ClientType] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Type]      NVARCHAR (MAX) NULL,
    [IsDeleted] BIT            NULL,
    CONSTRAINT [PK_ClientType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

