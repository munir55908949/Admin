CREATE TABLE [dbo].[Users_Pages] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [PageId] INT            NULL,
    [UserId] NVARCHAR (128) NULL,
    CONSTRAINT [PK_Users_Pages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_Pages_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Users_Pages_Pages] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Pages] ([Id])
);

