namespace Api.Models
{
    public class AttendanceModel
    { 
        public Guid? UserId { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        public DateTime Date { get; set; }
        public decimal? SpentHours { get; set; }
        public string? CheckIn { get; set; }
        public string? CheckOut { get; set; }
        public bool? isCheckedIn { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? WorkingDays { get; set; }
        public int? PresentDays { get; set; }
        public decimal? TotalHours { get; set; }
    }
}
