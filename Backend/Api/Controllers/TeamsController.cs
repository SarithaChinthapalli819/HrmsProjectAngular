using Api.Constants;
using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
 
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _teamsService;
        public TeamsController(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }
        [HttpPost]
        [Route(RouteConstants.UpsertTeams)]
        public HrmsJsonResult UpsertTeams(TeamsModel teams)
        {
            try
            {
                var validationmessages = new List<ApiResponseMessages>();
                var response = _teamsService.UpsertTeams(teams, validationmessages);
                var result = new HrmsJsonResult();
                result.success = true;
                if (UiHelper.CheckForValidationMessages(validationmessages))
                {
                    result.success = false;
                    result.ApiResponseMessages = validationmessages;
                }
                return result;

            }
            catch (Exception ex)
            {
                var result = new HrmsJsonResult();
                result.success = true;
                result.ApiResponseMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return result;
            }
        }

        [HttpGet]
        [Route(RouteConstants.GetTeams)]
        public HrmsJsonResult GetTeams()
        {
            try
            {
                var validationMessages = new List<ApiResponseMessages>();
                var response = new HrmsJsonResult();
                var result = _teamsService.GetTeams(validationMessages);
                response.success = true;
                if (UiHelper.CheckForValidationMessages(validationMessages)){
                    response.ApiResponseMessages = validationMessages;
                    response.success = false;
                }
                response.Data = result;
                return response;
            }
            catch(Exception ex)
            {
                var result = new HrmsJsonResult();
                result.success = true;
                result.ApiResponseMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return result; 
            }
        }


        [HttpPost]
        [Route(RouteConstants.UpsertTeamMembers)]
        public HrmsJsonResult UpsertTeamMembers(TeamsModel teams)
        {
            try
            {
                var validationMessages = new List<ApiResponseMessages>();
                var response = new HrmsJsonResult();
                var result = _teamsService.UpsertTeamMembers(teams,validationMessages);
                response.success = true;
                if (UiHelper.CheckForValidationMessages(validationMessages))
                {
                    response.ApiResponseMessages = validationMessages;
                    response.success = false;
                }
                response.Data = result;
                return response;
            }
            catch (Exception ex)
            {
                var result = new HrmsJsonResult();
                result.success = true;
                result.ApiResponseMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return result;
            }
        }

        [HttpPost]
        [Route(RouteConstants.GetTeamMembers)]
        public HrmsJsonResult GetTeamMembers(TeamsModel teams)
        {
            try
            {
                var validationMessages = new List<ApiResponseMessages>();
                var response = new HrmsJsonResult();
                var result = _teamsService.GetTeamMembers(teams, validationMessages);
                response.success = true;
                if (UiHelper.CheckForValidationMessages(validationMessages))
                {
                    response.ApiResponseMessages = validationMessages;
                    response.success = false;
                }
                response.Data = result;
                return response;
            }
            catch (Exception ex)
            {
                var result = new HrmsJsonResult();
                result.success = true;
                result.ApiResponseMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return result;
            }
        }

    }
}
