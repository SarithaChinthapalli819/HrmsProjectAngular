CREATE PROCEDURE [dbo].[GetSpentTime]
(
	@UserId UNIQUEIDENTIFIER = NULL,
	@TaskId UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
	SELECT CAST(SWITCHOFFSET(
           TODATETIMEOFFSET(StartTime, '+00:00'),
            '+05:30'
        ) AS DATETIME) AS StartTime FROM TaskSpentTime WHERE UserId = @UserId AND TaskId = @TaskId AND EndTime IS NULL
END
GO