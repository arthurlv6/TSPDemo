CREATE TABLE [dbo].[SubItemDetails] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX) NULL,
    [SubMenuItemId] INT            NOT NULL,
    [Title]         NVARCHAR (MAX) NULL,
    [Paragraph]     NVARCHAR (MAX) NULL,
    [Image]         NVARCHAR (MAX) NULL,
    [Order]         INT            NOT NULL,
    [Disabled]      BIT            NOT NULL,
    CONSTRAINT [PK_SubItemDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubItemDetails_SubMenuItems_SubMenuItemId] FOREIGN KEY ([SubMenuItemId]) REFERENCES [dbo].[SubMenuItems] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SubItemDetails_SubMenuItemId]
    ON [dbo].[SubItemDetails]([SubMenuItemId] ASC);

