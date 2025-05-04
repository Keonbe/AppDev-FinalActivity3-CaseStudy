-- Stored Procedure: Save User Records from Registration to the database table.
CREATE PROCEDURE [dbo].SaveUserRegisration
    @Name NVARCHAR(150),
    @EmailAddress NVARCHAR(150),
    @Password NVARCHAR(50),
	@MembershipType NCHAR(10),
	@IsAdmin NCHAR(10)
AS
BEGIN
    INSERT INTO UserInfoTable (Name, EmailAddress, Password, MembershipType, IsAdmin)
    VALUES (@Name, @EmailAddress, @Password, @MembershipType, @IsAdmin)
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