using Api.Constants;
using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

   
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;
        public TimesheetController(ITimesheetService timesheetService)
        {
            _timesheetService = timesheetService;
        }

        [HttpPost]
        [Route(RouteConstants.GetTimesheetData)]
        public HrmsJsonResult GetTimesheetData(TimesheetModel model)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMesssages = new List<ApiResponseMessages>();
                var result = _timesheetService.GetTimesheetData(model, validationMesssages);
                if (UiHelper.CheckForValidationMessages(validationMesssages))
                {
                    response.success = false;
                    response.ApiResponseMessages = validationMesssages;
                }
                response.Data = result;
                return response;
            }
            catch (Exception ex)
            {
                var response = new HrmsJsonResult();
                response.success = false;
                response.ApiResponseMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return response;
            }
        }

        [HttpPost]
        [Route(RouteConstants.GetTimesheetDetailsData)]
        public HrmsJsonResult GetTimesheetDetailsData(TimesheetModel model)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMesssages = new List<ApiResponseMessages>();
                var result = _timesheetService.GetTimesheetDetailsData(model, validationMesssages);
                if (UiHelper.CheckForValidationMessages(validationMesssages))
                {
                    response.success = false;
                    response.ApiResponseMessages = validationMesssages;
                }
                response.Data = result;
                return response;
            }
            catch (Exception ex)
            {
                var response = new HrmsJsonResult();
                response.success = false;
                response.ApiResponseMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return response;
            }
        }

    }
}
