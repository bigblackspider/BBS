CREATE TABLE [dbo].[MailAccount] (
    [MailAccountId] INT           IDENTITY (1, 1) NOT NULL,
    [MailBoxId]     INT           NOT NULL,
    [AccountName]   NVARCHAR (50) NOT NULL,
    [Forwarding]    NVARCHAR (50) NULL,
    [MaxSizeMB]     INT           NOT NULL,
    [DateCreated]   DATETIME      NOT NULL,
    [DateUpdated]   DATETIME      NULL,
    [Status]        NCHAR (10)    NULL,
    CONSTRAINT [PK_MailAccount] PRIMARY KEY CLUSTERED ([MailAccountId] ASC),
    CONSTRAINT [FK_MailAccount_Mail] FOREIGN KEY ([MailBoxId]) REFERENCES [dbo].[MailBox] ([MailBoxId]) ON DELETE CASCADE
);



