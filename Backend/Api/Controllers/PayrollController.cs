using Api.Constants;
using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpPost]
        [Route(RouteConstants.UpsertPayroll)]
        public HrmsJsonResult UpsertPayroll(PayrollModel model)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMessages = new List<ApiResponseMessages>();
                var result = _payrollService.UpsertPayrollData(model, validationMessages);
                if (UiHelper.CheckForValidationMessages(validationMessages)) {
                    response.success = false;
                    response.ApiResponseMessages = validationMessages;
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

        [HttpGet]
        [Route(RouteConstants.GetPayroll)]
        public HrmsJsonResult GetPayroll()
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMessages = new List<ApiResponseMessages>();
                var result = _payrollService.GetPayroll(validationMessages);
                if (UiHelper.CheckForValidationMessages(validationMessages))
                {
                    response.success = false;
                    response.ApiResponseMessages = validationMessages;
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
