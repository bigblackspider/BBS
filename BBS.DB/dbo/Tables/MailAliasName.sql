CREATE TABLE [dbo].[MailAliasName] (
    [MailAliasNameId] INT           NOT NULL,
    [MailBoxId]       INT           NOT NULL,
    [AliasName]       NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_MailAlias] PRIMARY KEY CLUSTERED ([MailAliasNameId] ASC),
    CONSTRAINT [FK_MailAlias_Mail] FOREIGN KEY ([MailBoxId]) REFERENCES [dbo].[MailBox] ([MailBoxId]) ON DELETE CASCADE
);

