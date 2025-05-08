--Only Query Here. All storred procesure and functions are in the 03_StoredProcedure.sql file

-- Insert sample data into UserInfoTable
INSERT INTO [dbo].[ProductInventoryTable] 
    ([ProductID], [ProductName], [Price], [Stocks])
VALUES
    ('MSE', 'Mouse', 350.00, 100),
    ('PRN', 'Printer Ink', 7500.00, 100),
    ('PRNDT', 'Printer Dot Matrix', 5000.00, 100),
    ('MNTRLc', 'LCD Monitor', 6500.00, 100),
    ('MNTRLe', 'LED Monitor', 7500.00, 100);


    ---
    ---Test/Execute Stored Procedure Query

    --UpdateUserPassword Change Password [WORKING]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[UpdateUserPassword]
		@EmailAddress = N'test123@email.com',
		@Password = N'123',
		@newpassword = N'321'

SELECT	@return_value as 'Return Value'

GO

--Add user admin
DECLARE	@return_value Int

EXEC	@return_value = [dbo].[SaveUserRegisration]
		@Name = N'admin123',
		@EmailAddress = N'admin123@email.com',
		@Password = N'123',
		@MembershipType = N'Silver',
		@IsAdmin = 1

SELECT	@return_value as 'Return Value'

GO