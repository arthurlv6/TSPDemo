CREATE TABLE [dbo].[ContactUs] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NULL,
    [Email]   NVARCHAR (100) NOT NULL,
    [Message] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

