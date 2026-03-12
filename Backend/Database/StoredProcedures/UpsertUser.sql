CREATE PROCEDURE [dbo].[UpsertUser]
(
    @UserId UNIQUEIDENTIFIER = NULL, 
    @FirstName NVARCHAR(150) = NULL,
    @LastName NVARCHAR(150) = NULL,
    @UserName NVARCHAR(150) = NULL,
    @Email NVARCHAR(250) = NULL,
    @PasswordHash NVARCHAR(500) = NULL,
    @RoleId UNIQUEIDENTIFIER = NULL,
    @DepartmentId UNIQUEIDENTIFIER = NULL,
    @DesignationId UNIQUEIDENTIFIER = NULL,
    @IsActive BIT = NULL,
    @IsRegister BIT = NULL,
    @JoiningDate Date = NULL,
    @EmployeeCode NVARCHAR(MAX) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    -- If Id is NULL, generate new one
    IF @UserId IS NULL
        SET @UserId = NEWID();
    
    IF(@IsRegister =1 )
    BEGIN
        SET @RoleId = '0A602494-2947-4E9D-AB63-0B791E1ACABD';
        SET @EmployeeCode = @UserName;
    END

    DECLARE @OperationPerformedBy UNIQUEIDENTIFIER = (SELECT TOP 1 Id From [Users] WHERE UserName LIKE '%Support%') ;

    MERGE INTO Users AS TARGET
    USING (
        SELECT
            @UserId AS Id,
            @FirstName AS FirstName,
            @LastName AS LastName,
            @UserName AS UserName,
            @Email AS Email,
            @PasswordHash AS PasswordHash, 
            @JoiningDate AS JoiningDate
    ) AS SOURCE
    ON TARGET.Id = SOURCE.Id

    WHEN MATCHED THEN
        UPDATE SET
            TARGET.FirstName = SOURCE.FirstName,
            TARGET.LastName = SOURCE.LastName,
            TARGET.UserName = SOURCE.UserName,
            TARGET.Email = SOURCE.Email,
            TARGET.PasswordHash = SOURCE.PasswordHash,
            TARGET.UpdatedDateTime = GETUTCDATE(),
            TARGET.UpdatedByUserId = @OperationPerformedBy,
            TARGET.JoiningDate =SOURCE.JoiningDate

    WHEN NOT MATCHED THEN
        INSERT (Id, FirstName, LastName, UserName, Email, PasswordHash,IsActive,CreatedByUserId,CreatedDateTime,JoiningDate)
        VALUES (
            SOURCE.Id,
            SOURCE.FirstName,
            SOURCE.LastName,
            SOURCE.UserName,
            SOURCE.Email,
            SOURCE.PasswordHash,
            1,
            @OperationPerformedBy,
            GETUTCDATE(),
            JoiningDate
        );

    MERGE INTO Employee AS TARGET
    USING(
        SELECT @UserId AS UserId,
                @DepartmentId AS DepartmentId,
                @DesignationId AS DesignationId,
                @EmployeeCode AS EmployeeCode 
        ) AS SOURCE
        ON TARGET.UserId = SOURCE.UserId
        WHEN MATCHED THEN
        UPDATE 
        SET TARGET.EmployeeCode = SOURCE.EmployeeCode,
             TARGET.DepartmentId = SOURCE.DepartmentId,
             TARGET.DesignationId = SOURCE.DesignationId,
             TARGET.UpdatedDateTime = GETUTCDATE(),
             TARGET.UpdatedByUserId = @OperationPerformedBy
            
        WHEN NOT MATCHED THEN
        INSERT(
            Id,
            UserId,
            EmployeeCode,
            DepartmentId,
            DesignationId,
            [CreatedByUserId],
            [CreatedDateTime]
        )
        VALUES(
            NEWID(),
            SOURCE.UserId,
            SOURCE.EmployeeCode,
            SOURCE.DepartmentId,
            SOURCE.DesignationId,
            @OperationPerformedBy,
            GETUTCDATE()
        );


   MERGE INTO UserRole AS TARGET
    USING (
        SELECT 
            @UserId AS UserId,
            @RoleId AS RoleId
    ) AS SOURCE
    ON TARGET.UserId = SOURCE.UserId

    WHEN MATCHED THEN
        UPDATE SET 
            TARGET.RoleId = SOURCE.RoleId,
            TARGET.UpdatedDateTime = GETUTCDATE(),
            TARGET.UpdatedByUserId = @OperationPerformedBy

    WHEN NOT MATCHED THEN
        INSERT (
            Id,
            UserId,
            RoleId,
            [CreatedByUserId],
            [CreatedByDateTime]
        )
        VALUES (
            NEWID(),
            SOURCE.UserId,
            SOURCE.RoleId,
            @OperationPerformedBy,
            GETUTCDATE()
        ); 
END
GO
