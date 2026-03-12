namespace Api.Models
{
    public class LeavesModel
    {
        public Guid? LeaveId { get; set; }
        public Guid? LeaveTypeId { get; set; }
        public string? Description { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Guid? UserId { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string? LeaveTypeName { get; set; }
        public string? Colour { get; set; }
        public string? Icon { get; set; }
        public bool? IsArcheive { get; set; }
        public bool? ApproveRequest { get; set; }
    }
}
