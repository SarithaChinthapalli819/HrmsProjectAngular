using Api.Constants;
using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly ILeavesService _leavesService;
        public LeavesController(ILeavesService leavesService)
        {
            _leavesService = leavesService;
        }
        [HttpGet]
        [Route(RouteConstants.GetLeaveTypes)]
        public HrmsJsonResult getLeaveTypes()
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMesssages = new List<ApiResponseMessages>();
                var result = _leavesService.getLeaveTypes(validationMesssages);
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
        [Route(RouteConstants.UpsertLeave)]
        public HrmsJsonResult upsertLeaves(LeavesModel leaves)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMesssages = new List<ApiResponseMessages>();
                var result = _leavesService.upsertLeaves(leaves,validationMesssages);
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

        [HttpGet]
        [Route(RouteConstants.GetLeave)]
        public HrmsJsonResult getLeaves()
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMesssages = new List<ApiResponseMessages>();
                var result = _leavesService.getLeaves(validationMesssages);
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
