using Api.Constants;
using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        [HttpPost]
        [Route(RouteConstants.UpsertAttendanceData)]
        public HrmsJsonResult UpsertAttendanceData(AttendanceModel attendance)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMesssages = new List<ApiResponseMessages>();
                var result = _attendanceService.UpsertAttendanceData(attendance, validationMesssages);
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
        [Route(RouteConstants.GetAttendanceData)]
        public HrmsJsonResult GetAttendanceData(AttendanceModel attendance)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMesssages = new List<ApiResponseMessages>();
                var result = _attendanceService.GetAttendanceData(attendance, validationMesssages);
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
