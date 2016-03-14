CREATE TABLE [dbo].[Customer] (
    [CustomerId]      INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]       NVARCHAR (50)  NOT NULL,
    [MiddleNames]     NCHAR (200)    NULL,
    [FamilyName]      NVARCHAR (50)  NOT NULL,
    [Phone]           NCHAR (10)     NULL,
    [Mobile]          NCHAR (10)     NULL,
    [Fax]             NCHAR (10)     NULL,
    [EmailAddress]    NVARCHAR (100) NOT NULL,
    [CustomerWebsite] NVARCHAR (100) NULL,
    [Status]          NVARCHAR (10)  CONSTRAINT [DF_Customer_Status] DEFAULT (N'New') NOT NULL,
    [DateCreated]     DATETIME       NOT NULL,
    [DateUpdated]     DATETIME       NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);





