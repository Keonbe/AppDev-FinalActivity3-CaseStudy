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


--Add user
INSERT INTO dbo.UserInfoTable (Name, EmailAddress, Password, MembershipType, IsAdmin)
VALUES
  ('Alice Santos',    'alice@example.com',   'alice123', 'Silver',    0),
  ('Bob Reyes',       'bob@example.com',     'bob123',   'Gold',      0),
  ('Charlie Cruz',    'charlie@example.com', 'charlie1', 'Platinum',  0),
  ('Diana Velasco',   'diana@example.com',   'diana123', 'Silver',    0),
  ('Admin User',      'admin@example.com',   'admin123', NULL,        1);

  --Add Samplew Transaction Values
  INSERT INTO dbo.TransactionsTable (UserID, DateTime, TotalAmount, MembershipType)
VALUES
  (28, '2025-01-01T10:15:00',  500.00,  'Silver'),
  (30, '2025-01-02T14:30:00', 1500.00,  'Gold'),
  (31, '2025-01-03T09:45:00', 2500.00,  'Platinum'),
  (32, '2025-01-04T16:20:00',  750.00,  'Silver'),
  (31, '2025-01-05T11:10:00', 1200.00,  'Silver');