CREATE TABLE [dbo].[ProductInventoryTable] (
    [Id]          INT            NOT NULL,
    [ProductID]   NCHAR (10)     NOT NULL,
    [ProductName] NVARCHAR (150) NULL,
    [Price]       DECIMAL (18)   NULL,
    [Stocks]      INT            NULL,
    CONSTRAINT [PK_ProductInventoryTable] PRIMARY KEY CLUSTERED ([ProductID] ASC)
);

CREATE TABLE [dbo].[UserInfoTable] (
    [UserId]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (150) NOT NULL,
	[EmailAddress]   NVARCHAR (150) NOT NULL,
    [Password]       NVARCHAR (50)  NOT NULL,
    [MembershipType] NCHAR (10)     NULL,
    [IsAdmin]        NCHAR (10)     DEFAULT ('false') NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);
