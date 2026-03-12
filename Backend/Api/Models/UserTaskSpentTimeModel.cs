namespace Api.Models
{
    public class UserTaskSpentTimeModel
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? TaskId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
