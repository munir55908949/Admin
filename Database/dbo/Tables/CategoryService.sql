CREATE TABLE [dbo].[CategoryService] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [CategoryId]    INT             NULL,
    [ServiceTypeId] INT             NULL,
    [UnitTypeId]    INT             NULL,
    [PriceUnit]     DECIMAL (18, 3) NULL,
    [IsDeleted]     BIT             NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Category_PriceType] FOREIGN KEY ([UnitTypeId]) REFERENCES [dbo].[UnitType] ([Id]),
    CONSTRAINT [FK_Category_Service] FOREIGN KEY ([ServiceTypeId]) REFERENCES [dbo].[ServiceType] ([Id]),
    CONSTRAINT [FK_CategoryService_CategoryService] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);

