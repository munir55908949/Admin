CREATE TABLE [dbo].[RequestPo] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [PoNumber]     NVARCHAR (MAX) NULL,
    [PoValue]      NVARCHAR (MAX) NULL,
    [DepartmentId] INT            NULL,
    [StatusId]     INT            NULL,
    [CompanyId]    INT            NULL,
    [ReceiverId]   INT            NULL,
    [CreatedBy]    INT            NULL,
    [CreatedAt]    DATETIME       NULL,
    [ModifiedBy]   INT            NULL,
    [ModifiedAt]   DATETIME       NULL,
    [IsDeleted]    BIT            NULL,
    CONSTRAINT [PK_RequestPo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestPo_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]),
    CONSTRAINT [FK_RequestPo_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id]),
    CONSTRAINT [FK_RequestPo_Receiver] FOREIGN KEY ([ReceiverId]) REFERENCES [dbo].[Receiver] ([Id]),
    CONSTRAINT [FK_RequestPo_Status] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id])
);

