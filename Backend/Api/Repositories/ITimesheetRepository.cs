using Api.Models;

namespace Api.Repositories
{
    public interface ITimesheetRepository
    {
        public List<TimesheetModel> GetTimesheetData(TimesheetModel model, List<ApiResponseMessages> validationMessages);
        public List<TimesheetModel> GetTimesheetDetailsData(TimesheetModel model, List<ApiResponseMessages> validationMessages);
    }
}
