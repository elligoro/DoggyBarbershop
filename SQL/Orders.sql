CREATE TABLE [dbo].[Orders] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [AccountId]   INT      NOT NULL,
    [BookingDate] DATETIME NOT NULL,
    [CreatedDate] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([Id])
);

