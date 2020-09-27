CREATE TABLE [dbo].[SubSystems] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (MAX) NULL,
    [Order] INT            NOT NULL,
    CONSTRAINT [PK_SubSystems] PRIMARY KEY CLUSTERED ([Id] ASC)
);

