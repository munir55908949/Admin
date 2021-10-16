CREATE TABLE [dbo].[Client] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [ClientName]   NVARCHAR (MAX) NULL,
    [Address]      NVARCHAR (MAX) NULL,
    [Phone]        NVARCHAR (MAX) NULL,
    [Email]        NVARCHAR (MAX) NULL,
    [ClientTypeId] INT            NULL,
    [IsDeleted]    BIT            NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Client_ClientType] FOREIGN KEY ([ClientTypeId]) REFERENCES [dbo].[ClientType] ([Id])
);

