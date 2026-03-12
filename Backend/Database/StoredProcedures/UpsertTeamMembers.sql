CREATE PROCEDURE [dbo].[UpsertTeamMembers]
(
	@TeamId UNIQUEIDENTIFIER = NULL,
	@TeamMemberId UNIQUEIDENTIFIER = NULL,
	@DeleteMember BIT = NULL
)
AS
BEGIN

    DECLARE @OperationPerformedBy UNIQUEIDENTIFIER = (SELECT TOP 1 Id From [Users] WHERE UserName LIKE '%Support%') ;

	IF(@DeleteMember = 1)
	BEGIN
		UPDATE Teammember SET InActiveDateTime = GETUTCDATE() WHERE TeamId =@TeamId AND UserId = @TeamMemberId;
	END
	ELSE
	BEGIN
		MERGE INTO TEAMMEMBER AS TARGET
		USING(
		 SELECT @TeamId AS TeamId,
				@TeamMemberId AS UserId  
		) AS SOURCE
		ON TARGET.TeamId = SOURCE.TeamId AND TARGET.UserId = SOURCE.UserId
		WHEN MATCHED THEN 
		UPDATE SET  
			TARGET.UpdatedDateTime = GETUTCDATE(),
			TARGET.UpdatedByUserId = @OperationPerformedBy,
			TARGET.InActiveDateTime = NULL
		WHEN NOT MATCHED THEN
		 INSERT (Id,TeamId,UserId,CreatedByUserId,CreatedDateTime )
		 VALUES (
			 NEWID(),
			 SOURCE.TeamId,
			 SOURCE.UserId,
			 @OperationPerformedBy,
			 GETUTCDATE()
		 );
	 END

END