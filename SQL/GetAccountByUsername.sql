CREATE PROCEDURE [dbo].[GetAccountByUsername]
	@username NVARCHAR(100)
AS
BEGIN
	SELECT 
		accounts.Id
		,accounts.FirstName
		,accounts.AccessToken
		,accounts.Password
		,accounts.UserName
	FROM [dbo].[Accounts] AS accounts
	WHERE accounts.UserName = @username
END