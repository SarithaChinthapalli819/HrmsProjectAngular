namespace Api.Models
{
    public class LeaveTypesModel
    {
        public Guid? LeaveTypeId { get; set; }
		public string LeaveTypeName { get; set; }
		public int TotalAllowance { get; set; }
		public int Used { get; set; }
		public int Remaining { get; set; }
		public string Colour { get; set; }
		public string Icon { get; set; }
    }
}
