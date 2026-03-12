namespace Api.Models
{
    public class UserModel
    {
        public Guid? UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid?  DesignationId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsRegister { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? EmployeeCode { get; set; }
        public string? DepartmentName { get; set; }
        public string? DesignationName { get; set; } 
        public string? RoleName { get; set; }
        public string? Token { get; set; }

    }
}
