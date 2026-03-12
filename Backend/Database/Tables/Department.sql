CREATE TABLE [dbo].[Department]
(
    [Id] UNIQUEIDENTIFIER NOT NULL, 
    [DepartmentName] NCHAR(100) NOT NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    [CreatedByUserId] UNIQUEIDENTIFIER NOT NULL, 
    [UpdatedDateTime] DATETIME  NULL, 
    [UpdatedByUserId] UNIQUEIDENTIFIER  NULL,
    CONSTRAINT PK_Department PRIMARY KEY CLUSTERED (Id)
)
GO