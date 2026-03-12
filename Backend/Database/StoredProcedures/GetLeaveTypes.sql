CREATE PROCEDURE [dbo].[GetLeaveTypes]
AS
BEGIN
	SELECT Id AS LeaveTypeId,LeaveType AS LeaveTypeName,TotalAllowance,Used, Remaining,Colour,Icon  FROM LeaveTypes
END
GO