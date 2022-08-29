CREATE PROCEDURE [dbo].[InsertOrder]
	 @AccountId int
	,@BookingDate DATETIME
AS
BEGIN
	DECLARE @now DATETIME = GETUTCDATE()
	Insert INTO [dbo].[Orders] (AccountId,BookingDate,CreatedDate) 
								VALUES (@AccountId,@BookingDate,@now)
END