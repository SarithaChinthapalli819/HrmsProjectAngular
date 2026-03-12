using System.ComponentModel;

namespace Api.Models
{
    public class ProjectModel
    {

        public Guid? ProjectId {get;set;}
        public string? ProjectName {get;set;}
        public string? ClientName  {get;set;} 
        public int? Priority { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate  {get; set;}
        public decimal? Budget  {get; set;}
        public Guid? TeamId  {get; set;}
	    public int? Status { get; set; }
        public string? TeamName { get; set; }
    }
}
