CREATE PROCEDURE [dbo].[GetTeammembers]
(
	@Teamid UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
	SELECT T.TeamName as TeamName,U.FirstName + ' ' + U.LastName AS TeamMemberName,U.Id AS TeamMemberId,U.Email AS Email FROM TEAM T WITH (NOLOCK)
	INNER JOIN Teammember TM WITH (NOLOCK) ON TM.TeamId = T.Id
	INNER JOIN Users U WITH (NOLOCK) ON U.Id = TM.UserId
	WHERE TM.InActiveDateTime IS NULL AND U.InActiveDateTime IS NULL
END