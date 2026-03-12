namespace Api.Models
{
    public class TeamsModel
    {
        public  Guid? TeamId { get; set; }
        public Guid? TeamAdminId { get; set; }
        public Guid? TeamMemberId { get; set; }
        public string? Email { get; set; }
        public string? TeamName { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public string? TeamAdminName { get; set; }
        public string? TeamMemberName { get; set; }
        public int? TotalCount { get; set; }
        public bool DeleteMember { get; set; }
    }
}
