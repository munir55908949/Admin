CREATE TABLE [dbo].[Invoice] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [InvoiceDate]    DATETIME        NULL,
    [ClientId]       INT             NULL,
    [TotalId]        INT             NULL,
    [CreatedBy]      NVARCHAR (128)  NULL,
    [TotalOrder]     INT             NULL,
    [TotalUnit]      INT             NULL,
    [TotalPrice]     DECIMAL (18, 3) NULL,
    [Note]           NVARCHAR (MAX)  NULL,
    [IsDone]         BIT             NULL,
    [IsDeleted]      BIT             NULL,
    [DeliveryAmount] DECIMAL (18, 3) NULL,
    [Discount]       DECIMAL (18, 3) NULL,
    [Rest]           DECIMAL (18, 3) NULL,
    [Paid]           DECIMAL (18, 3) NULL,
    [DateOut]        DATETIME        NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Invoice_AspNetUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Invoice_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id])
);

