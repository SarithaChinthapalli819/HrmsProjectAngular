CREATE TABLE [dbo].[Designation]
(
    [Id] UNIQUEIDENTIFIER NOT NULL, 
    [DesignationName] NCHAR(100) NOT NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    [CreatedByUserId] UNIQUEIDENTIFIER NOT NULL, 
    [UpdatedDateTime] DATETIME  NULL, 
    [UpdatedByUserId] UNIQUEIDENTIFIER  NULL,
    CONSTRAINT PK_Designation PRIMARY KEY CLUSTERED (Id)
)
GO