CREATE TABLE [dbo].[Employee]
(
	[Id] UNIQUEIDENTIFIER NOT NULL, 
    [EmployeeCode] NCHAR(100) NOT NULL, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Designationid] UNIQUEIDENTIFIER NULL, 
    [DepartmentId] UNIQUEIDENTIFIER NULL,
    [CreatedDateTime] DATETIME NOT NULL, 
    [CreatedByUserId] UNIQUEIDENTIFIER NOT NULL, 
    [UpdatedDateTime] DATETIME  NULL, 
    [UpdatedByUserId] UNIQUEIDENTIFIER  NULL,
    InActiveDateTime DATETIME NULL
    CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED (Id)
    CONSTRAINT FK_Employee_UserId FOREIGN KEY (UserId) REFERENCES dbo.[Users](Id)
    CONSTRAINT FK_Employee_DesignationId FOREIGN KEY (Designationid) REFERENCES dbo.Designation(Id)
    CONSTRAINT FK_Employee_DepartmentId FOREIGN KEY (DepartmentId) REFERENCES dbo.Department(Id)
)
GO
