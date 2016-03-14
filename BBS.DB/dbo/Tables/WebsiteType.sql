CREATE TABLE [dbo].[WebsiteType] (
    [WebsiteTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_WebsiteType] PRIMARY KEY CLUSTERED ([WebsiteTypeId] ASC)
);

