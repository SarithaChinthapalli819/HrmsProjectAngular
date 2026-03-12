CREATE PROCEDURE [dbo].[UpsertSpentTime]
(
	@Id UNIQUEIDENTIFIER = NULL,
	@UserId UNIQUEIDENTIFIER = NULL,
	@TaskId UNIQUEIDENTIFIER = NULL,
	@StartTime DATETIME = NULL,
	@EndTime DATETIME = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	-- Case 1: Start task
	IF(@StartTime IS NOT NULL AND @EndTime IS NULL)
	BEGIN
		-- Close existing running task
		IF EXISTS (
			SELECT 1 
			FROM TaskSpentTime 
			WHERE UserId = @UserId 
			AND EndTime IS NULL
		)
		BEGIN
			UPDATE TaskSpentTime
			SET EndTime = @StartTime,SpentTimeInMin = DATEDIFF(MINUTE, StartTime, @StartTime),
			UpdatedDateTime = GETUTCDATE(),
            UpdatedByUserId = @UserId
			WHERE UserId = @UserId
			AND EndTime IS NULL
		END

		-- Insert new running task
		INSERT INTO TaskSpentTime
		(
			Id,
			UserId,
			TaskId,
			Date,
			StartTime,
			EndTime,
			CreatedDateTime,
			CreatedByUserId
		)
		VALUES
		(
			NEWID(),
			@UserId,
			@TaskId,
			GETUTCDATE(),
			@StartTime,
			NULL,
			GETUTCDATE(),
			@UserId
		)
	END

	-- Case 2: Stop task
	ELSE IF(@EndTime IS NOT NULL)
	BEGIN
		UPDATE TaskSpentTime
		SET EndTime = @EndTime,SpentTimeInMin = DATEDIFF(MINUTE, StartTime, @EndTime),
		UpdatedDateTime = GETUTCDATE(),
        UpdatedByUserId = @UserId
		WHERE UserId = @UserId
		AND TaskId = @TaskId
		AND EndTime IS NULL
	END

END
GO