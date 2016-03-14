CREATE TABLE [dbo].[CustomerProduct] (
    [CustomerProductId] INT      IDENTITY (1, 1) NOT NULL,
    [CustomerId]        INT      NOT NULL,
    [MailBoxId]         INT      NULL,
    [WebsiteId]         INT      NULL,
    [DateCreated]       DATETIME NOT NULL,
    CONSTRAINT [PK_CustomerProduct] PRIMARY KEY CLUSTERED ([CustomerProductId] ASC),
    CONSTRAINT [FK_CustomerProduct_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerProduct_Mail] FOREIGN KEY ([MailBoxId]) REFERENCES [dbo].[MailBox] ([MailBoxId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CustomerProduct_Website] FOREIGN KEY ([WebsiteId]) REFERENCES [dbo].[Website] ([WebsiteId]) ON DELETE CASCADE
);



