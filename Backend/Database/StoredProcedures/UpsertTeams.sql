CREATE PROCEDURE [dbo].[UpsertTeams]
(
 @TeamId UNIQUEIDENTIFIER = NULL,
 @TeamAdminId UNIQUEIDENTIFIER = NULL, 
 @TeamName NVARCHAR(250) = NULL,
 @Description  NVARCHAR(500) = NULL,
 @IsActive BIT = NULL
)
AS
BEGIN
	DECLARE @OperationPerformedBy UNIQUEIDENTIFIER = (SELECT TOP 1 Id From [Users] WHERE UserName LIKE '%Support%') ;
	IF(@Teamid IS NULL)
		SET @TeamId = NEWID();

	MERGE INTO TEAM AS TARGET
	USING(
	SELECT @TeamId  AS TeamId,
		   @TeamName AS TeamName,
		   @TeamAdminId AS TeamAdminId,  
		   @Description AS [Description],
		   @IsActive AS IsActive
	) AS SOURCE 
	ON SOURCE.TeamId = TARGET.Id
	WHEN MATCHED THEN
	UPDATE SET
		TARGET.TeamName = SOURCE.TeamName,
		TARGET.[Description] = SOURCE.[Description],
		TARGET.IsActive = SOURCE.IsActive,
		TARGET.UpdatedDateTime = GETUTCDATE(),
        TARGET.UpdatedByUserId = @OperationPerformedBy
	WHEN NOT MATCHED THEN
	INSERT (
		Id,
		TeamName,
		[Description],
		IsActive,
		[CreatedByUserId],
		[CreatedDateTime]
	)
	VALUES
	(
	 SOURCE.TeamId,
	 SOURCE.TeamName,
	 SOURCE.[Description],
	 SOURCE.IsActive,
	 @OperationPerformedBy,
	 GETUTCDATE()
	);

	MERGE INTO TeamAdmin AS TARGET
	USING(
	SELECT @TeamId  AS TeamId,
		   @TeamAdminId AS TeamAdminId 
	) AS SOURCE 
	ON SOURCE.TeamId = TARGET.Id
	WHEN MATCHED THEN
	UPDATE SET
		TARGET.TeamId = SOURCE.TeamId,
		TARGET.UserId = SOURCE.TeamAdminId, 
		TARGET.UpdatedDateTime = GETUTCDATE(),
        TARGET.UpdatedByUserId = @OperationPerformedBy
	WHEN NOT MATCHED THEN
	INSERT (
		Id,
		TeamId,
		UserId, 
		[CreatedByUserId],
		[CreatedDateTime]
	)
	VALUES
	(
	 NEWID(),
	 SOURCE.TeamId,
	 SOURCE.TeamAdminId, 
	 @OperationPerformedBy,
	 GETUTCDATE()
	);
END