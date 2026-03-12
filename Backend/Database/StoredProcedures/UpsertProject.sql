CREATE PROCEDURE [dbo].[UpsertProject]
(
	@ProjectId UNIQUEIDENTIFIER = NULL, 
	@ProjectName NVARCHAR(100) = NULL,
	@ClientName NVARCHAR(100) = NULL,
	@Priority INT = NULL,
	@Description NVARCHAR(200) = NULL,
	@StartDate DATETIME = NULL,
	@Budget DECIMAL(18,2) = NULL,
	@TeamId UNIQUEIDENTIFIER = NULL,
	@Status INT = NULL
)
AS
BEGIN
	DECLARE @OperationPerformedBy UNIQUEIDENTIFIER = (SELECT TOP 1 Id From [Users] WHERE UserName LIKE '%Support%') ;
	MERGE INTO PROJECT AS TARGET 
	USING(
		SELECT 
		@ProjectId AS Id,
		@ProjectName AS ProjectName,
		@ClientName AS ClientName,
		@Priority AS Priority,
		@Description AS Description,
		@StartDate AS StartDate,
		@Budget AS Budget,
		@TeamId AS TeamId,
		@Status AS Status
	)
	AS SOURCE 
	ON TARGET.Id = SOURCE.Id
	WHEN MATCHED 
	THEN
	UPDATE SET
			TARGET.ProjectName = SOURCE.ProjectName,
			TARGET.ClientName = SOURCE.ClientName,
			TARGET.Priority = SOURCE.Priority,
			TARGET.Description = SOURCE.Description,
			TARGET.StartDate = SOURCE.StartDate,
			TARGET.Budget = SOURCE.Budget,
			TARGET.Status = SOURCE.Status,
			TARGET.UpdatedDateTime = GETUTCDATE(),
            TARGET.UpdatedByUserId = @OperationPerformedBy
	WHEN NOT MATCHED 
	THEN 
	INSERT
	(
		Id,
		ProjectName,
		ClientName,
		Priority,
		Description,
		StartDate,
		Budget,
		TeamId,
		Status,
		CreatedDateTime,
        CreatedByUserId
	)
	VALUES
	(
		NEWID(),
		SOURCE.ProjectName,
		SOURCE.ClientName,
		SOURCE.Priority,
		SOURCE.Description,
		SOURCE.StartDate,
		SOURCE.Budget,
		SOURCE.TeamId,
		SOURCE.Status,
		GETUTCDATE(),
        @OperationPerformedBy
	);
END
