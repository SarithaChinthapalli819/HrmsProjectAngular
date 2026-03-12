CREATE PROCEDURE [dbo].[GetDepartments]
AS
BEGIN
	SELECT Id,DepartmentName FROM Department WITH (NOLOCK)
END
GO