CREATE TABLE [dbo].[Website] (
    [WebsiteId]     INT          IDENTITY (1, 1) NOT NULL,
    [WebsiteTypeId] INT          CONSTRAINT [DF_Website_WebsiteTypeId] DEFAULT ((1)) NOT NULL,
    [DateCreated]   DATETIME     CONSTRAINT [DF_Website_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [DateUpdated]   DATETIME     NOT NULL,
    [ExpiryDate]    DATETIME     NULL,
    [Status]        VARCHAR (10) CONSTRAINT [DF_Website_Status] DEFAULT ('New') NOT NULL,
    CONSTRAINT [PK_Website] PRIMARY KEY CLUSTERED ([WebsiteId] ASC),
    CONSTRAINT [FK_Website_WebsiteType] FOREIGN KEY ([WebsiteTypeId]) REFERENCES [dbo].[WebsiteType] ([WebsiteTypeId])
);





