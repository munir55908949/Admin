CREATE TABLE [dbo].[InvoiceList] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [InvoiceId] INT            NULL,
    [Status]    NVARCHAR (MAX) NULL,
    [IsDeleted] BIT            NULL,
    CONSTRAINT [PK_InvoiceList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceList_InvoiceList] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([Id])
);

