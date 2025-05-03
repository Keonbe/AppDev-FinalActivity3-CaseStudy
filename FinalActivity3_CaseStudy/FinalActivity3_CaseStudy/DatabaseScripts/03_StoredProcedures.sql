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
	--SELECT * FROM ClientInfoTable 
	SELECT EmailAddress, Password
	From UserInfoTable
	WHERE EmailAddress = @userName AND PassWord = @PassWord
RETURN 0