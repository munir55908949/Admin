CREATE TABLE [dbo].[OrderData] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [InvoiceId]         INT             NULL,
    [CategoryServiceId] INT             NULL,
    [UnitNo]            INT             NULL,
    [Price]             DECIMAL (18, 3) NULL,
    [Note]              NVARCHAR (MAX)  NULL,
    [IsDeleted]         BIT             NULL,
    CONSTRAINT [PK_Orderdata] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderData_CategoryService] FOREIGN KEY ([CategoryServiceId]) REFERENCES [dbo].[CategoryService] ([Id]),
    CONSTRAINT [FK_OrderData_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([Id])
);

