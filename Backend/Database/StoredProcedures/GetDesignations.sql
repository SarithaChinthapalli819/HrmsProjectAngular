CREATE PROCEDURE [dbo].[GetDesignations]
AS
BEGIN
	SELECT Id,DesignationName FROM Designation WITH (NOLOCK)
END
GO