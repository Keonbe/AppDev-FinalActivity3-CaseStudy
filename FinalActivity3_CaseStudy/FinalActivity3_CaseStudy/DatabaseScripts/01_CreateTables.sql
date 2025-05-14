-- Create ProductInventoryTable
--Product Table(Run Query or Add values first, run this)
CREATE TABLE [dbo].[ProductInventoryTable] (
    [Id]          INT            NOT NULL,
    [ProductID]   NCHAR (10)     NOT NULL,
    [ProductName] NVARCHAR (150) NULL,
    [Price]       DECIMAL (18)   NULL,
    [Stocks]      INT            NULL,
    CONSTRAINT [PK_ProductInventoryTable] PRIMARY KEY CLUSTERED ([ProductID] ASC)
);

--Change to "IDENTITY(1,1)" after adding values. For adding products @ admin increments id number
CREATE TABLE [dbo].[ProductInventoryTable] (
    [Id]          INT          IDENTITY(1,1) NOT NULL,
    [ProductID]   NCHAR (10)     NOT NULL,
    [ProductName] NVARCHAR (150) NULL,
    [Price]       DECIMAL (18)   NULL,
    [Stocks]      INT            NULL,
    CONSTRAINT [PK_ProductInventoryTable] PRIMARY KEY CLUSTERED ([ProductID] ASC)
);
GO

-- Create UserInfoTable
CREATE TABLE [dbo].[UserInfoTable] (
    [UserId]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (150) NOT NULL,
    [EmailAddress]   NVARCHAR (150) NOT NULL,
    [Password]       NVARCHAR (50)  NOT NULL,
    [MembershipType] NCHAR (10)     NULL,
    [IsAdmin]        BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([EmailAddress] ASC)
);
GO

-- Create TransactionsTable
CREATE TABLE [dbo].[TransactionsTable] (
    [TransactionID]  INT   IDENTITY(1,1) NOT NULL,
    [UserID]         INT          NOT NULL,
    [DateTime]       DATETIME     NOT NULL,
    [TotalAmount]    DECIMAL (18) NOT NULL,
    [MembershipType] NCHAR (10)   NULL,
    PRIMARY KEY CLUSTERED ([TransactionID] ASC)
);
GO

-- Create TransactionDetails
CREATE TABLE [dbo].[TransactionDetails] (
    [DetailID]      INT             IDENTITY (1, 1) NOT NULL,
    [TransactionID] INT             NOT NULL,
    [ProductID]     NCHAR (10)      NOT NULL,
    [Quantity]      INT             NOT NULL,
    [Unit Price]    DECIMAL (18, 2) NOT NULL,
    [Discount]      DECIMAL (18, 2) NOT NULL,
    [TotalAmount]   DECIMAL (18, 2) NOT NULL,
    [CreatedAt]     DATETIME        DEFAULT (getdate()) NOT NULL,
    [UpdatedAt]     DATETIME        DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_TransactionDetails] PRIMARY KEY CLUSTERED ([DetailID] ASC)
);
GO

-- Create AddCartTable
CREATE TABLE [dbo].[AddCartTable] (
    [CartId]    INT        IDENTITY (1, 1) NOT NULL,
    [UserID]    INT        NOT NULL,
    [ProductID] NCHAR (10) NOT NULL,
    [Quantity]  INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([CartId] ASC)
);
GO