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


-- Stored Procedure: Login Check
CREATE PROCEDURE [dbo].LoginAccountCheck
	@userName NVARCHAR(255),
	@PassWord NVARCHAR(255)
AS
	SELECT EmailAddress, Password
	FROM UserInfoTable
	WHERE EmailAddress = @userName AND PassWord = @PassWord
RETURN 0


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