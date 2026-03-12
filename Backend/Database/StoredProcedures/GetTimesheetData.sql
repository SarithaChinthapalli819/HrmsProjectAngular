CREATE PROCEDURE [dbo].[GetTimesheetData]
(
    @UserId UNIQUEIDENTIFIER = NULL,
    @Month INT = NULL,
    @Year INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        [Date], 
        CAST(SpentTimeInMin / 60.0 AS DECIMAL(10,2)) AS SpentHours,
        MonthStart,
        MonthEnd,
        UserName
    FROM
    (
        SELECT 
            CAST(TS.Date AS DATE) AS [Date], 
            SUM(TS.SpentTimeInMin) AS SpentTimeInMin, 
            DATEFROMPARTS(PR.Year, PR.Month, 1) AS MonthStart,
            EOMONTH(DATEFROMPARTS(PR.Year, PR.Month, 1)) AS MonthEnd,
            U.FirstName + ' ' + U.LastName AS UserName
        FROM TaskSpentTime TS 
        INNER JOIN Users U WITH (NOLOCK) ON U.Id = TS.UserId
        INNER JOIN Employee E WITH (NOLOCK) ON E.UserId = TS.UserId
        INNER JOIN Payroll PR WITH (NOLOCK) ON PR.EmployeeId = E.Id
        WHERE (TS.UserId = @UserId OR @UserId IS NULL) AND PR.Month = @Month AND PR.Year = @Year
        GROUP BY 
            CAST(TS.Date AS DATE), 
            PR.Month,
            PR.Year,
            U.FirstName,
			U.LastName,
			E.EmployeeCode
    ) AS SpentTime
    ORDER BY [Date] DESC;
END