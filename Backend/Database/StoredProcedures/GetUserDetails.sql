CREATE PROCEDURE [dbo].[GetUserDetails]
AS
BEGIN
	SELECT U.Id AS UserId,FirstName,LastName,UserName,Email,E.Id AS EmployeeId,E.EmployeeCode,Designationid,DepartmentId,DE.DepartmentName,DG.DesignationName,IsActive,R.RoleName,U.JoiningDate,R.Id AS RoleId,PasswordHash AS Password FROM [Users] U WITH (NOLOCK)
	INNER JOIN Employee E WITH (NOLOCK) ON E.UserId = U.Id
	INNER JOIN Department DE WITH (NOLOCK) ON DE.Id = E.DepartmentId
	INNER JOIN Designation DG WITH (NOLOCK) ON DG.Id = E.Designationid
	INNER JOIN UserRole UR WITH (NOLOCK) ON UR.UserId = U.Id
	INNER JOIN [Role] R WITH (NOLOCK) ON R.Id = UR.RoleId
END