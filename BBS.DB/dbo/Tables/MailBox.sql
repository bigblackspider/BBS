CREATE TABLE [dbo].[MailBox] (
    [MailBoxId]        INT           IDENTITY (1, 1) NOT NULL,
    [DomainName]       NVARCHAR (50) NOT NULL,
    [MaxMessageSizeKB] INT           NOT NULL,
    [MaxAccountSizeMB] INT           NOT NULL,
    [MaxAccounts]      INT           NOT NULL,
    [CatchAllAddress]  NVARCHAR (50) NULL,
    [DateCreated]      DATETIME      NOT NULL,
    [DateUpdated]      DATETIME      NOT NULL,
    [ExpiryDate]       DATETIME      NULL,
    [Status]           NCHAR (10)    NOT NULL,
    CONSTRAINT [PK_Mail] PRIMARY KEY CLUSTERED ([MailBoxId] ASC)
);

