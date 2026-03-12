using Api.Constants;
using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
     
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpPost]
        [Route(RouteConstants.UpsertTask)]
        public HrmsJsonResult UpsertTask(TaskModel model)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMessages = new List<ApiResponseMessages>();
                var result = _taskService.UpsertTask(model,validationMessages);
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

        [HttpPost]
        [Route(RouteConstants.GetTask)]
        public HrmsJsonResult GetTask(TaskModel taskModel)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMessages = new List<ApiResponseMessages>();
                var result = _taskService.GetTask(taskModel,validationMessages);
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

        [HttpPost]
        [Route(RouteConstants.UpsertSpentTime)]
        public HrmsJsonResult UpsertSpentTime(UserTaskSpentTimeModel model)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMessages = new List<ApiResponseMessages>();
                var result = _taskService.UpsertSpentTime(model, validationMessages);
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

        [HttpPost]
        [Route(RouteConstants.GetSpentTime)]
        public HrmsJsonResult GetSpentTime(UserTaskSpentTimeModel model)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMessages = new List<ApiResponseMessages>();
                var result = _taskService.GetSpentTime(model, validationMessages);
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
