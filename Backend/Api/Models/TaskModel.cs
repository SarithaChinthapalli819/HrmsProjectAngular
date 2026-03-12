namespace Api.Models
{
    public class TaskModel
    {
        public Guid? TaskId { get; set; }
        public Guid? UserId { get; set; }
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public int? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? AssignedTo { get; set; }
        public int? Status { get; set; }
        public string? UserName { get; set; }
        public bool? IsUpdateStatus { get; set; }
        public DateTime? StartTime { get; set; }
    }
}
