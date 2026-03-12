CREATE PROCEDURE [dbo].[GetTeams]
AS
BEGIN
    
	SELECT T.Id AS TeamId,T.TeamName,T.IsActive,
	T.[Description],U.FirstName + ' ' + U.LastName AS TeamAdminName,
	(SELECT COUNT(*) FROM Teammember WHERE TeamId = T.Id AND InActiveDateTime IS NULL) AS TotalCount,
	U.Id AS TeamAdminId FROM Team T WITH (NOLOCK)
	LEFT JOIN TeamAdmin TA  WITH (NOLOCK) ON TA.TeamId = T.Id
	LEFT JOIN Users U WITH (NOLOCK) ON U.Id = TA.UserId
END