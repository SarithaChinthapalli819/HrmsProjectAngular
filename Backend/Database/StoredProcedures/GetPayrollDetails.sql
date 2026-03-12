CREATE PROCEDURE [dbo].[GetPayrollDetails]
AS
BEGIN
	SELECT P.Id AS Id,PayrollId,Month,Year,BasicSalary,Allowances,Deductions,NetSalary,EmployeeId,U.FirstName + ' ' + U.LastName AS UserName FROM Payroll P
	INNER JOIN Employee E WITH (NOLOCK) ON E.Id = P.EmployeeId
	INNER JOIN [Users] U WITH (NOLOCK) ON U.Id = E.UserId
END
