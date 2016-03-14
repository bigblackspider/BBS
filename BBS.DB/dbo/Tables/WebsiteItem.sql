CREATE TABLE [dbo].[WebsiteItem] (
    [WebsiteItemId] INT           IDENTITY (1, 1) NOT NULL,
    [ItemName]      NCHAR (50)    NOT NULL,
    [ItemValue]     VARCHAR (MAX) NOT NULL,
    [WebsiteId]     INT           NOT NULL,
    CONSTRAINT [PK_WebsiteItem] PRIMARY KEY CLUSTERED ([WebsiteItemId] ASC),
    CONSTRAINT [FK_WebsiteItem_Website] FOREIGN KEY ([WebsiteId]) REFERENCES [dbo].[Website] ([WebsiteId]) ON DELETE CASCADE
);

