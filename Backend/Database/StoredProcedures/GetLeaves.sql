CREATE PROCEDURE [dbo].[GetLeaves]
AS
BEGIN
	SELECT L.Id AS LeaveId,LT.Id AS LeaveTypeId,LT.LeaveType AS LeaveTypeName,L.UserId AS UserId,L.DateFrom,L.DateTo,L.Reason AS [Description],L.CreatedDateTime,LT.Icon,LT.Colour,LT.LeaveType AS LeaveTypeName,L.IsApproved FROM Leaves L
	INNER JOIN LeaveTypes LT WITH (NOLOCK) ON LT.Id = L.LeaveTypeId
	WHERE L.InActiveDateTime IS NULL AND LT.InActiveDateTime IS NULL
END