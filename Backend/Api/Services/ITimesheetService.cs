using Api.Models;

namespace Api.Services
{
    public interface ITimesheetService
    {
        public List<TimesheetModel> GetTimesheetData(TimesheetModel model, List<ApiResponseMessages> validationMessages);
        public List<TimesheetModel> GetTimesheetDetailsData(TimesheetModel model, List<ApiResponseMessages> validationMessages);
    }
}
