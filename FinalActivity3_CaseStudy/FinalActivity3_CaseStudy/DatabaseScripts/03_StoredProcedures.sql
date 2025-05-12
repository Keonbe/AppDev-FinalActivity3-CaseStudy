--Start Here
-- Stored Procedure: Save User Records from Registration to the database table.
CREATE PROCEDURE [dbo].SaveUserRegisration
    @Name           NVARCHAR(150),
    @EmailAddress   NVARCHAR(150),
    @Password       NVARCHAR(50),
    @MembershipType NCHAR(10),
    @IsAdmin        BIT
AS
BEGIN
    INSERT INTO UserInfoTable
        (Name, EmailAddress, Password, MembershipType, IsAdmin)
    VALUES
        (@Name, @EmailAddress, @Password, @MembershipType, @IsAdmin);
END
RETURN 0;


--Start Here
-- Stored Procedure: Login Check
CREATE PROCEDURE [dbo].LoginAccountCheck
	@userName NVARCHAR(255),
	@PassWord NVARCHAR(255)
AS
	SELECT EmailAddress, Password
	FROM UserInfoTable
	WHERE EmailAddress = @userName AND PassWord = @PassWord
RETURN 0


--Start Here
-- Stored Procedure: PROFILE MANAGER(CHANGE PASSWORD ONLY)
CREATE PROCEDURE [dbo].UpdateUserPassword
    @EmailAddress NVARCHAR(150),  -- Unique identifier to find the record
    @Password NVARCHAR(50),        -- Current password to verify before updating
	@newpassword NVARCHAR(50)
AS
BEGIN
    UPDATE UserInfoTable
    SET 
		Password = @newpassword
    WHERE EmailAddress = @EmailAddress
      AND Password = @Password;

    IF @@ROWCOUNT = 0
        RETURN 0;  -- No rows updated, possibly wrong email or password
    ELSE
        RETURN 1;  -- Update successful
END


--Start Here
-- Stored Procedure: Admin Login Account Check
CREATE PROCEDURE [dbo].AdminLoginAccountCheck
	@userName NVARCHAR(255),
	@PassWord NVARCHAR(255)
AS
	SELECT EmailAddress, Password, IsAdmin
	FROM UserInfoTable
	WHERE EmailAddress = @userName 
	AND PassWord = @PassWord
RETURN 0


--Start Here
--Get View All Records
CREATE PROCEDURE [dbo].GetAllMembers
	--@userID,
	--@Name,
	--@emailAddress,
	--@membershipType,
	--@IsAdmin
AS
	SELECT UserID, Name, EmailAddress, MembershipType, IsAdmin
	FROM UserInfoTable;
RETURN 0

CREATE PROCEDURE [dbo].GetAllProducts
	--@productID,
	--@productName,
	--@price,
	--@stocks
AS
	SELECT ProductID, ProductName, Price, Stocks 
	FROM ProductInventoryTable;
RETURN 0

CREATE PROCEDURE [dbo].GetAllProducts
	--@productID,
	--@productName,
	--@price,
	--@stocks
AS
	SELECT ProductID, ProductName, Price, Stocks 
	FROM ProductInventoryTable;
RETURN 0


--Start Here
--Add New Products
CREATE PROCEDURE [dbo].AddNewProducts
    @productID  NCHAR (10),
    @ProductName NVARCHAR (150),
    @Price DECIMAL (18),
	@Stocks INT

AS
BEGIN
    INSERT INTO ProductInventoryTable(ProductID, ProductName, Price, Stocks)
    VALUES (@productID, @ProductName, @Price, @Stocks)
END

RETURN 0;


--Start Here
--GetOrderHistoryUser
CREATE PROCEDURE [dbo].GetOrderHistoryUser
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        t.TransactionID,
        t.DateTime,
        t.TotalAmount,
        t.MembershipType,
        u.UserId,
        u.Name        AS UserName,
        u.EmailAddress,
        u.MembershipType AS UserMembership
    FROM dbo.TransactionsTable AS t
    INNER JOIN dbo.UserInfoTable AS u
        ON t.UserID = u.UserId
    WHERE t.UserID = @UserID
    ORDER BY t.DateTime;
END
/*
Explain: Gets info from 2 tables: TransactionsTable(Transactions) & UserTable(For user)
Joins both tables where UserID is same in TransactionsTable & UserID - Acts as FK
*/


--Start Here
--Add product to cart
CREATE PROCEDURE dbo.AddProductToCart
    @UserID    INT,
    @ProductID NCHAR(10),
    @Quantity  INT
AS
BEGIN
    INSERT INTO dbo.AddCartTable (UserID, ProductID, Quantity)
    VALUES (@UserID, @ProductID, @Quantity);
END
RETURN 0;


------------------------------------------------- Main Cart Procedures -------------------------------------------------

--Start Here
-- Remove a single item from the cart
CREATE PROCEDURE dbo.SC_RemoveCartItem
    @UserID INT,
    @CartId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM AddCartTable
    WHERE UserID = @UserID
      AND CartId = @CartId;
END


--Start Here
-- Fetch the cart items for display
CREATE PROCEDURE [dbo].SC_GetCartItems
    @UserID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        c.CartId,
        c.ProductID,
        p.ProductName,
        p.Price       AS Price,
        c.Quantity,
        (p.Price * c.Quantity) AS SubTotal
    FROM AddCartTable c
    JOIN ProductInventoryTable p
      ON c.ProductID = p.ProductID
    WHERE c.UserID = @UserID;
END


--Start Here
-- ProcessCheckout: Producttable, Transactionstable, AddCartTable 
CREATE PROCEDURE [dbo].SC_ProcessCheckout
    @UserID        INT,           -- The ID of the user performing the checkout
    @SubTotal      DECIMAL(18,2), -- The sum of (Price × Quantity) from the user’s cart
    @MemType       NCHAR(10),     -- The user’s membership tier (e.g. 'Silver', 'Gold', 'Platinum')
    @DiscountRate  DECIMAL(18,2), -- The decimal discount rate (e.g. 0.10 for 10%)
    @FinalTotal    DECIMAL(18,2)  -- The final amount to charge: (SubTotal + VAT) – Discount
AS
BEGIN
    SET NOCOUNT ON;  -- Suppress “n rows affected” messages

    BEGIN TRY
        BEGIN TRANSACTION;  -- Start an atomic transaction

        ----------------------------------------------------------------
        -- 1) Insert the transaction header (one row in TransactionsTable)
        ----------------------------------------------------------------
        INSERT INTO TransactionsTable
            (UserID, DateTime, TotalAmount, MembershipType)
        VALUES
            (@UserID,          -- which user
             GETDATE(),        -- current timestamp
             @FinalTotal,      -- final computed total
             @MemType);        -- membership tier

        -- Capture the newly generated TransactionID for use in detail rows
        DECLARE @TransID INT = SCOPE_IDENTITY();

        ----------------------------------------------------------------
        -- 2) Insert all cart items as detail rows
        ----------------------------------------------------------------
        INSERT INTO TransactionDetails
            (TransactionID, ProductID, Quantity, [Unit Price], Discount, TotalAmount)
        SELECT
            @TransID,                   -- link back to the header
            c.ProductID,                -- product code
            c.Quantity,                 -- quantity purchased
            p.Price,                    -- unit price from inventory
            @DiscountRate,              -- same rate applied to all lines
            (p.Price * c.Quantity)      -- gross line total
              * (1 - @DiscountRate)      -- net after discount
        FROM
            AddCartTable    AS c
            JOIN ProductInventoryTable AS p
              ON c.ProductID = p.ProductID
        WHERE
            c.UserID = @UserID;         -- only this user’s cart items

        ----------------------------------------------------------------
        -- 3) Decrement inventory stocks for each purchased item
        ----------------------------------------------------------------
        UPDATE p
        SET
            Stocks = p.Stocks - c.Quantity  -- subtract quantity sold
        FROM
            ProductInventoryTable AS p
            JOIN AddCartTable AS c
              ON p.ProductID = c.ProductID
        WHERE
            c.UserID = @UserID;        -- only this user’s cart

        ----------------------------------------------------------------
        -- 4) Clear out the user’s cart (they’ve just checked out)
        ----------------------------------------------------------------
        DELETE FROM AddCartTable
        WHERE UserID = @UserID;

        COMMIT TRANSACTION;  -- Everything succeeded, persist changes
    END TRY
    BEGIN CATCH
        -- On any error, roll back and re-throw
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

--Start Here
--GetOrderHistory-TransactionDetails
CREATE PROCEDURE dbo.GetTransactionDetails
    @TransactionID INT
AS
BEGIN
    SET NOCOUNT ON;
		-- adjust the JOIN in your SP:
		SELECT
		  td.DetailID,
		  td.TransactionID,
		  td.ProductID,
		  p.ProductName,
		  td.Quantity,
		  td.[Unit Price],
		  td.Discount,
		  td.TotalAmount
		FROM TransactionDetails AS td
		JOIN ProductInventoryTable AS p
		  ON td.ProductID = p.ProductID
		WHERE td.TransactionID = @TransactionID
		ORDER BY td.DetailID;
END