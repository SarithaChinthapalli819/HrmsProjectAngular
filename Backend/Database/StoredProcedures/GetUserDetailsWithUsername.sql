CREATE PROCEDURE [dbo].[GetUserDetailsWithUsername]
(
	@UserName NVarchar(max) = null
)
AS
BEGIN
	SELECT Id AS UserId,UserName,Email,PasswordHash AS [Password] FROM [Users] WITH (NOLOCK) WHERE UserName = @UserName AND IsActive = 1
END
GO