namespace Api.Models
{
    public class PayrollModel
    {
        public Guid? Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? PayrollId { get; set; }
        public string? SalaryJson { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public decimal?    BasicSalary { get; set; }
        public decimal?    Allowances { get; set; }
        public decimal?   Deductions { get; set; }
        public decimal?    NetSalary { get; set; }
        public string? UserName { get; set; }

    }
}
