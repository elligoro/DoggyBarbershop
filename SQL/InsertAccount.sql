CREATE PROCEDURE [dbo].[InsertAccount]
	 @FirstName NVARCHAR(200)
	,@UserName NVARCHAR(100)
	,@Password NVARCHAR(200)
AS
BEGIN
	INSERT INTO [dbo].[Accounts] (FirstName,UserName,Password)
				VALUES (@FirstName,@UserName,@Password)
	SELECT SCOPE_IDENTITY()
END