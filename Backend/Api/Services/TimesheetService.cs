using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class TimesheetService:ITimesheetService
    {
        private readonly ITimesheetRepository _timesheetRepository;
        public TimesheetService(ITimesheetRepository timesheetRepository)
        {
            _timesheetRepository = timesheetRepository;
        }
        public List<TimesheetModel> GetTimesheetData(TimesheetModel model, List<ApiResponseMessages> validationMessages)
        {
            return _timesheetRepository.GetTimesheetData(model, validationMessages);
        }
        public List<TimesheetModel> GetTimesheetDetailsData(TimesheetModel model, List<ApiResponseMessages> validationMessages)
        {
            return _timesheetRepository.GetTimesheetDetailsData(model, validationMessages);
        }
    }
}
