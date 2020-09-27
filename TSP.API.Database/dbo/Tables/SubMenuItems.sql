CREATE TABLE [dbo].[SubMenuItems] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [SubSystemId] INT            NOT NULL,
    [Order]       INT            NOT NULL,
    CONSTRAINT [PK_SubMenuItems] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubMenuItems_SubSystems_SubSystemId] FOREIGN KEY ([SubSystemId]) REFERENCES [dbo].[SubSystems] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_SubMenuItems_SubSystemId]
    ON [dbo].[SubMenuItems]([SubSystemId] ASC);

