CREATE TABLE [dbo].[Accounts] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (200)  NOT NULL,
    [UserName]    NVARCHAR (100)  NOT NULL,
    [Password]    NVARCHAR (200)  NOT NULL,
    [AccessToken] NVARCHAR (1000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
