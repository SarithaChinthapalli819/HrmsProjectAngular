/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DECLARE @DefaultUserId UNIQUEIDENTIFIER =  NULL
SET @DefaultUserId = (SELECT TOP 1 Id From [Users] WHERE UserName LIKE '%Support%') 
IF(@DefaultUserId IS NULL)
BEGIN
   SET @DefaultUserId = NEWID()

	MERGE INTO [Users] AS TARGET
	USING 
	(VALUES
	(@DefaultUserId, 'Suport','','Support','Support@gmail.com','Test123!',1)
	)
	AS SOURCE
	( Id,FirstName,LastName,UserName,Email,PasswordHash,IsActive)
	ON TARGET.UserName = SOURCE.UserName
	WHEN MATCHED 
	THEN 
	UPDATE
		SET FirstName = SOURCE.FirstName,
			LastName = SOURCE.LastName,
			UserName = SOURCE.UserName,
			Email = SOURCE.Email,
			PasswordHash = SOURCE.PasswordHash,
			IsActive = SOURCE.IsActive,
			[UpdatedDateTime] = GETUTCDATE(),
			[UpdatedByUserId] = @DefaultUserId
	WHEN NOT MATCHED 
	THEN
	INSERT 
	( Id,FirstName,LastName,UserName,Email,PasswordHash,IsActive,[CreatedDateTime],[CreatedByUserId])
	VALUES(SOURCE.[Id],SOURCE.FirstName,SOURCE.LastName,SOURCE.UserName,SOURCE.Email,SOURCE.PasswordHash,SOURCE.IsActive,GETUTCDATE(),@DefaultUserId);
END

 

MERGE INTO [Role] AS TARGET
USING 
(VALUES
(NEWID(), 'Super Admin'),
(NEWID(), 'User')
)
AS SOURCE
([Id],[RoleName])
ON TARGET.[RoleName] = SOURCE.[RoleName]
WHEN MATCHED 
THEN 
UPDATE
	SET [RoleName] = SOURCE.[RoleName],
		[UpdatedDateTime] = GETUTCDATE(),
		[UpdatedByUserId] = @DefaultUserId
WHEN NOT MATCHED 
THEN
INSERT 
([Id],[RoleName],[CreatedDateTime],[CreatedByUserId])
VALUES(SOURCE.[Id],SOURCE.[RoleName],GETUTCDATE(),@DefaultUserId);


MERGE INTO Department AS TARGET
USING 
(VALUES
(NEWID(), 'HR'),
(NEWID(), 'IT'),
(NEWID(), 'Finance')
)
AS SOURCE
([Id],[DepartmentName])
ON TARGET.[DepartmentName] = SOURCE.[DepartmentName]
WHEN MATCHED 
THEN 
UPDATE
	SET [DepartmentName] = SOURCE.[DepartmentName],
		[UpdatedDateTime] = GETUTCDATE(),
		[UpdatedByUserId] = @DefaultUserId
WHEN NOT MATCHED 
THEN
INSERT 
([Id],[DepartmentName],[CreatedDateTime],[CreatedByUserId])
VALUES(SOURCE.[Id],SOURCE.[DepartmentName],GETUTCDATE(),@DefaultUserId);



MERGE INTO [Designation] AS TARGET
USING 
(
VALUES
(NEWID(), 'Senior Engineer'),
(NEWID(), 'HR Manager'),
(NEWID(), 'Accountant') 
)
AS SOURCE
([Id],[DesignationName])
ON TARGET.[DesignationName] = SOURCE.[DesignationName]
WHEN MATCHED 
THEN 
UPDATE
	SET [DesignationName] = SOURCE.[DesignationName],
		[UpdatedDateTime] = GETUTCDATE(),
		[UpdatedByUserId] = @DefaultUserId
WHEN NOT MATCHED 
THEN
INSERT 
([Id],[DesignationName],[CreatedDateTime],[CreatedByUserId])
VALUES(SOURCE.[Id],SOURCE.[DesignationName],GETUTCDATE(),@DefaultUserId);


MERGE INTO LeaveTypes AS TARGET 
USING(
	VALUES ('0F6884C9-416F-4104-9BDD-21B3ABA9B033','Annual Leave',24,0,24,'blue','fa-plane'),
		   ('33BF0D64-73C9-46FC-89D8-89DE0DDBB6AE','Sick Leave',24,0,24,'rose','fa-briefcase-medical'),
		   ('9D80E4C6-0CF3-427D-9651-EA0A141B8D1C','Casual Leave',24,0,24,'amber','fa-coffee')
)
AS SOURCE (Id,LeaveType,TotalAllowance,Used,Remaining,Colour,Icon)
ON TARGET.Id = SOURCE.Id
WHEN MATCHED THEN 
UPDATE
SET
	TARGET.LeaveType = SOURCE.LeaveType,
	TARGET.TotalAllowance = SOURCE.TotalAllowance,
	TARGET.Used = SOURCE.Used,
	TARGET.Remaining = SOURCE.Remaining,
	TARGET.Colour = SOURCE.Colour,
	TARGET.Icon = SOURCE.Icon,
	[UpdatedDateTime] = GETUTCDATE(),
	[UpdatedByUserId] = @DefaultUserId
WHEN NOT MATCHED 
THEN 
INSERT
(	
	Id,
	LeaveType,
	TotalAllowance,
	Used,
	Remaining,
	Colour,
	Icon,
	[CreatedDateTime],
	[CreatedByUserId]
)
VALUES
(
	SOURCE.Id,
	SOURCE.LeaveType,
	SOURCE.TotalAllowance,
	SOURCE.Used,
	SOURCE.Remaining,
	SOURCE.Colour,
	SOURCE.Icon,
	GETUTCDATE(),
	@DefaultUserId
);
GO
