CREATE TABLE [dbo].[Dimension] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Width]     DECIMAL (18) NULL,
    [Length]    DECIMAL (18) NULL,
    [Total]     DECIMAL (18) NULL,
    [IsDeleted] BIT          NULL,
    CONSTRAINT [PK_Dimension] PRIMARY KEY CLUSTERED ([Id] ASC)
);

