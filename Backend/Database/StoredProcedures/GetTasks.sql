CREATE PROCEDURE [dbo].[GetTasks]
(
	@UserId UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
	SELECT T.Id AS TaskId,
	P.ProjectName,
	T.ProjectId,
	t.AssignedTo,
	T.TaskName,
	T.Priority,
	T.Description,
	T.DueDate,
	T.Status,
	U.FirstName + ' ' + U.LastName AS UserName,
	(
		SELECT TOP 1 CAST(SWITCHOFFSET(
           TODATETIMEOFFSET(StartTime, '+00:00'),
            '+05:30'
        ) AS DATETIME)
		FROM TaskSpentTime TST WITH (NOLOCK)
		WHERE TST.TaskId = T.Id
			AND TST.UserId = @UserId
			AND TST.StartTime IS NOT NULL
			AND TST.EndTime IS NULL
		ORDER BY TST.StartTime DESC
	) AS StartTime, 
	U.Id AS AssignedTo FROM Tasks T WITH (NOLOCK)
	INNER JOIN Project P WITH (NOLOCK) ON P.Id = T.ProjectId
	INNER JOIN Users U WITH (NOLOCK) ON U.Id = T.AssignedTo 
END