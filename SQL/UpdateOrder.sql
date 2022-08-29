CREATE PROCEDURE [dbo].[UpdateOrder]
	@Id int
	,@BookingDate DateTime2
AS
BEGIN
	DECLARE @now DATETIME = GETUTCDATE()
	UPDATE [dbo].[Orders] 
	SET BookingDate = @BookingDate
	WHERE Id = @Id
END