CREATE PROCEDURE [dbo].[UpsertLeave]
(
    @LeaveId UNIQUEIDENTIFIER = NULL,
    @LeaveTypeId UNIQUEIDENTIFIER = NULL,
    @Description NVARCHAR(MAX) = NULL,
    @DateFrom DATETIME = NULL,
    @DateTo DATETIME = NULL,
    @UserId UNIQUEIDENTIFIER = NULL,
    @IsApproved BIT = NULL,
    @IsArcheive BIT = NULL,
    @ApproveRequest BIT = NULL
)
AS
BEGIN
	DECLARE @OperationPerformedBy UNIQUEIDENTIFIER = (SELECT TOP 1 Id From [Users] WHERE UserName LIKE '%Support%') ;
    IF(@LeaveId IS NULL)
        SET @LeaveId = NEWID();

    IF EXISTS(
        SELECT 1 FROM Leaves WHERE UserId = @UserId  
            AND DateFrom >= @DateFrom AND  DateTo <= @DateTo AND Id <> @LeaveId
    )
    BEGIN 
        RAISERROR('Leave already existed on selected range.', 16, 1);
        RETURN;
    END
    ELSE IF(@IsArcheive = 1)
    BEGIN
        UPDATE Leaves SET InActiveDateTime = GETUTCDATE() WHERE Id = @LeaveId
    END
    ELSE IF(@ApproveRequest = 1)
    BEGIN
        UPDATE Leaves SET IsApproved = @IsApproved WHERE Id = @LeaveId
        IF(@IsApproved = 1)
        BEGIN
            UPDATE LeaveTypes SET Used = Used + 1 WHERE Id = @LeaveTypeId
        END
        ELSE
        BEGIN
            UPDATE LeaveTypes SET Used = Used - 1 WHERE Id = @LeaveTypeId
        END
    END
    ELSE
    BEGIN
        MERGE INTO LEAVES AS TARGET
        USING(
            SELECT
                @LeaveId AS LeaveId,
                @LeaveTypeId AS LeaveTypeId,
                @Description AS Reason,
                @DateFrom AS DateFrom,
                @DateTo AS DateTo,
                @UserId AS UserId,
                @IsApproved AS IsApproved
        )
        AS SOURCE 
        ON TARGET.Id = SOURCE.LeaveId
        WHEN MATCHED THEN
        UPDATE SET
            TARGET.LeaveTypeId = SOURCE.LeaveTypeId,
	        TARGET.DateFrom = SOURCE.DateFrom,
	        TARGET.DateTo = SOURCE.DateTo,
            TARGET.Reason = SOURCE.Reason,
            TARGET.UpdatedDateTime = GETUTCDATE(),
            TARGET.UpdatedByUserId = @OperationPerformedBy
        WHEN NOT MATCHED THEN 
        INSERT 
        (
            [Id],
	        LeaveTypeId,
	        DateFrom,
	        DateTo,
	        UserId,
	        Reason,
	        IsApproved,
            [CreatedByUserId],
            [CreatedDateTime]
        )
        VALUES
        (
            NEWID(),
            SOURCE.LeaveTypeId,
            SOURCE.DateFrom,
	        SOURCE.DateTo,
	        SOURCE.UserId,
	        SOURCE.Reason,
            SOURCE.IsApproved,
            @OperationPerformedBy,
            GETUTCDATE()
        );
    END
END