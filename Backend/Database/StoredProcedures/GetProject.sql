CREATE PROCEDURE [dbo].[GetProject]
AS
BEGIN
	SELECT P.Id AS ProjectId,ProjectName,ClientName,Priority,P.Description,TeamId,StartDate,Budget,Status,T.TeamName FROM Project P WITH (NOLOCK)
	INNER JOIN Team T WITH (NOLOCK) ON T.Id = P.TeamId
END
GO