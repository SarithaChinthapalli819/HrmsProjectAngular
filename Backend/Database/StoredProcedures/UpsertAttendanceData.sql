CREATE PROCEDURE [dbo].[UpsertAttendanceData]
(
	@UserId UNIQUEIDENTIFIER = NULL,
	@CheckInTime DATETIME = NULL,
	@CheckOutTime DATETIME = NULL
)
AS
BEGIN 
	IF(@CheckInTime IS NOT NULL AND @CheckOutTime IS NULL)
	BEGIN
		INSERT INTO Attendance(Id,UserId,Date,CheckIn,CheckOut)
		VALUES(NEWID(),@UserId,GETUTCDATE(),@CheckInTime,NULL)
	END
	ELSE IF(@CheckOutTime IS NOT NULL)
	BEGIN
		IF EXISTS(SELECT 1 FROM Attendance WHERE UserId = @UserId AND CheckIn IS NOT NULL)
		BEGIN
		 UPDATE Attendance SET CheckOut = @CheckOutTime,SpentTimeInMin = DATEDIFF(MINUTE,CheckIn,@CheckOutTime) WHERE UserId = @UserId
		END
	END
END