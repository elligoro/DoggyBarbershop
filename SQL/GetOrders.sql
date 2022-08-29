CREATE PROCEDURE [dbo].[GetOrders]
AS
	SELECT 
	 orders.Id as orderId
	,orders.AccountId as accountId
	,orders.BookingDate as bookingDate
	,orders.CreatedDate as CreatedDate
	,accounts.FirstName as [name]
	FROM
	[dbo].[Orders] AS orders
		INNER JOIN [dbo].[Accounts] AS accounts
	ON orders.AccountId = accounts.Id