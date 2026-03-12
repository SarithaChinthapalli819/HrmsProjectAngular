using Api.Constants;
using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api.Controllers
{
    
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpPost]
        [Route(RouteConstants.UpsertProject)]
        public HrmsJsonResult UpsertProject(ProjectModel project)
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMessages = new List<ApiResponseMessages>();
                var result = _projectService.UpsertProject(project, validationMessages);
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

        [HttpGet]
        [Route(RouteConstants.GetProject)]
        public HrmsJsonResult GetProject()
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationMessages = new List<ApiResponseMessages>();
                var result = _projectService.GetProject(validationMessages);
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
