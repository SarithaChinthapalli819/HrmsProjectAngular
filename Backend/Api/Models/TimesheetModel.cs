namespace Api.Models
{
    public class TimesheetModel
    {
        public DateTime? Date { get; set; }
        public string? ProjectName { get; set; } 
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string? UserName { get; set; }
        public string? TaskName { get; set; }
        public decimal? SpentHours { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TaskId { get; set; }
        public DateTime? MonthEnd { get; set; }
        public DateTime? MonthStart { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }

}
