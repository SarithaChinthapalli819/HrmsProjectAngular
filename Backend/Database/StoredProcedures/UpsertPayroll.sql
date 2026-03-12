CREATE PROCEDURE [dbo].[UpsertPayroll]
(
    @PayrollId UNIQUEIDENTIFIER = NULL,
    @SalaryJson NVARCHAR(MAX) = NULL,
    @Month INT = NULL,
    @Year INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OperationPerformedBy UNIQUEIDENTIFIER = (SELECT TOP 1 Id From [Users] WHERE UserName LIKE '%Support%') ;
     
    IF @PayrollId IS NULL
        SET @PayrollId = NEWID();
     

    CREATE  Table #SalaryDetails
    (
        EmployeeId UNIQUEIDENTIFIER, 
        Id UNIQUEIDENTIFIER,
        BasicSalary DECIMAL(18,2),
        Allowances DECIMAL(18,2),
        Deductions DECIMAL(18,2),
        NetSalary DECIMAL(18,2)
    )

    INSERT INTO #SalaryDetails
    (
        EmployeeId,
        Id,
        BasicSalary,
        Allowances,
        Deductions,
        NetSalary
    )
    SELECT 
        EmployeeId,
        Id,
        BasicSalary,
        Allowances,
        Deductions,
        NetSalary
    FROM OPENJSON(@SalaryJson)
    WITH
    (
        EmployeeId UNIQUEIDENTIFIER,
        Id UNIQUEIDENTIFIER,
        BasicSalary DECIMAL(18,2),
        Allowances DECIMAL(18,2),
        Deductions DECIMAL(18,2),
        NetSalary DECIMAL(18,2)
    );

    MERGE Payroll AS TARGET
    USING (
        SELECT 
            @PayrollId AS PayrollId,
            ISNULL(Id,NEWID()) AS Id,
            EmployeeId AS EmployeeId,
            @Month AS Month,
            @Year AS Year,
            BasicSalary AS BasicSalary,
            Allowances AS Allowances,
            Deductions AS Deductions,
            NetSalary AS NetSalary
            FROM #SalaryDetails
    ) AS SOURCE
    ON TARGET.PayrollId = SOURCE.PayrollId AND TARGET.EmployeeId = SOURCE.EmployeeId AND TARGET.Month = SOURCE.Month
            AND TARGET.Year = SOURCE.Year AND TARGET.Id = SOURCE.Id

    WHEN MATCHED THEN 
        UPDATE SET 
            TARGET.EmployeeId = SOURCE.EmployeeId,
            TARGET.Month = SOURCE.Month,
            TARGET.Year = SOURCE.Year,
            TARGET.BasicSalary = SOURCE.BasicSalary,
            TARGET.Allowances = SOURCE.Allowances,
            TARGET.Deductions = SOURCE.Deductions,
            TARGET.NetSalary = SOURCE.NetSalary,
            TARGET.UpdatedDateTime = GETUTCDATE(),
            TARGET.UpdatedByUserId = @OperationPerformedBy

    WHEN NOT MATCHED THEN 
        INSERT
        (
            Id,
            PayrollId,
            EmployeeId,
            Month,
            Year,
            BasicSalary,
            Allowances,
            Deductions,
            NetSalary,
            CreatedDateTime,
            CreatedByUserId
        )
        VALUES
        (
            SOURCE.Id,
            SOURCE.PayrollId,
            SOURCE.EmployeeId,
            SOURCE.Month,
            SOURCE.Year,
            SOURCE.BasicSalary,
            SOURCE.Allowances,
            SOURCE.Deductions,
            SOURCE.NetSalary,
            GETUTCDATE(),
            @OperationPerformedBy
        );

END
GO