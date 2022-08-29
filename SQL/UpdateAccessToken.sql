CREATE PROCEDURE [dbo].[UpdateAccessToken]
	 @accountId INT
	,@accessToken NVARCHAR(1000)
AS
BEGIN
	UPDATE [dbo].[Accounts]
	SET AccessToken = @accessToken
	WHERE Id = @accountId
END