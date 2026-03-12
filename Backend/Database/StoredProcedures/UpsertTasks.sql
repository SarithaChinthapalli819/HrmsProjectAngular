CREATE PROCEDURE [dbo].[UpsertTasks]
(
	@TaskId UNIQUEIDENTIFIER = NULL,
	@TaskName NVARCHAR(100) = NULL,
	@Description NVARCHAR(MAX) = NULL,
	@Priority INT = NULL,
	@DueDate DATETIME = NULL,
	@ProjectId UNIQUEIDENTIFIER = NULL,
	@AssignedTo UNIQUEIDENTIFIER = NULL,
	@IsUpdateStatus BIT = NULL,
	@Status INT =NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @OperationPerformedBy UNIQUEIDENTIFIER = (SELECT TOP 1 Id From [Users] WHERE UserName LIKE '%Support%') ;
	IF @TaskId IS NULL
		SET @TaskId = NEWID();

	IF(@IsUpdateStatus = 1)
	BEGIN
		UPDATE Tasks SET Status = @Status WHERE Id = @TaskId
	END
	ELSE
	BEGIN
		MERGE INTO TASKS AS TARGET
		USING
		(
			SELECT
				@TaskId AS TaskId,
				@TaskName AS TaskName,
				@Description AS Description,
				@Priority AS Priority,
				@DueDate AS DueDate,
				@ProjectId AS ProjectId,
				@AssignedTo AS AssignedTo
		) AS SOURCE
		ON TARGET.Id = SOURCE.TaskId

		WHEN MATCHED THEN 
		UPDATE SET 
			TARGET.TaskName = SOURCE.TaskName,
			TARGET.Description = SOURCE.Description,
			TARGET.Priority = SOURCE.Priority,
			TARGET.DueDate = SOURCE.DueDate,
			TARGET.ProjectId = SOURCE.ProjectId,
			TARGET.AssignedTo = SOURCE.AssignedTo,
			TARGET.UpdatedDateTime = GETUTCDATE(),
			TARGET.UpdatedByUserId = @OperationPerformedBy
		WHEN NOT MATCHED THEN
		INSERT
		(
			Id,
			TaskName,
			Description,
			Priority,
			DueDate,
			ProjectId,
			AssignedTo,
			[CreatedByUserId],
			[CreatedDateTime]
		)
		VALUES
		(
			SOURCE.TaskId,
			SOURCE.TaskName,
			SOURCE.Description,
			SOURCE.Priority,
			SOURCE.DueDate,
			SOURCE.ProjectId,
			SOURCE.AssignedTo,
			@OperationPerformedBy,
			GETUTCDATE()
		);
	END
END