using Api.Constants;
using Api.Helpers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
     
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService,IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpPost(RouteConstants.register)]
        public async Task<HrmsJsonResult> Register(UserModel model)
        {
            try
            {
                var validationMessages = new List<ApiResponseMessages>();
                var result = new HrmsJsonResult();
                result.success = true;
                var response = _userService.Register(model, validationMessages);
                if (UiHelper.CheckForValidationMessages(validationMessages))
                {
                    result.success = false;
                    result.ApiResponseMessages = validationMessages;
                }
                result.Data = response;
                return result;
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

        [HttpPost(RouteConstants.login)]
        public async Task<HrmsJsonResult> Login(UserModel model)
        {
            try
            {
                var response = new HrmsJsonResult();
                var validationMessages = new List<ApiResponseMessages>();

                var userFromDb = await _userService.GetUserDetails(model, validationMessages);

                if (userFromDb == null)
                {
                    response.success = false;
                    response.ApiResponseMessages.Add(new ApiResponseMessages
                    {
                        Message = "Invalid username or password",
                        MessageTypeEnum = MessageTypeEnum.Error
                    });
                    return response;
                }

                 
                bool isValidPassword = BCrypt.Net.BCrypt.Verify(
                    model.Password,
                    userFromDb.Password
                );

                if (!isValidPassword)
                {
                    response.success = false;
                    response.ApiResponseMessages.Add(new ApiResponseMessages
                    {
                        Message = "Invalid username or password",
                        MessageTypeEnum = MessageTypeEnum.Error
                    });
                    return response;
                }

                userFromDb.Token = GenerateJwtToken(userFromDb);
                response.success = true;
                response.Data = userFromDb;

                return response;
            }
            catch (Exception ex)
            {
                return new HrmsJsonResult
                {
                    success = false,
                    ApiResponseMessages =
            {
                new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                }
            }
                };
            }
        }


        private string GenerateJwtToken(UserModel user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName), 
                new Claim("UserId", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet(RouteConstants.department)]
        public async Task<HrmsJsonResult> GetDepartments()
        {
            try
            {
                var validationMessages = new List<ApiResponseMessages>();
                var response = await _userService.GetDepartments(validationMessages);
                var result = new HrmsJsonResult();
                result.success = true;
                if (UiHelper.CheckForValidationMessages(validationMessages))
                {
                    result.ApiResponseMessages = validationMessages;
                    return result;
                }
                result.Data = response;
                return result;
            }
            catch(Exception ex)
            {
                var response = new HrmsJsonResult();
                response.success = false;
                response.ApiResponseMessages.Add(
                    new ApiResponseMessages
                    {
                        Message = ex.Message,
                        MessageTypeEnum = MessageTypeEnum.Error
                    });
                return null;
            }
        }

        [HttpGet(RouteConstants.designation)]
        public async Task<HrmsJsonResult> GetDesignations()
        {
            try
            {
                var validationMessages = new List<ApiResponseMessages>();
                var response = await _userService.GetDesignations(validationMessages);
                var result = new HrmsJsonResult();
                result.success = true;
                if (UiHelper.CheckForValidationMessages(validationMessages))
                {
                    result.ApiResponseMessages = validationMessages;
                    return result;
                }
                result.Data = response;
                return result;
            }
            catch (Exception ex)
            {
                var response = new HrmsJsonResult();
                response.success = false;
                response.ApiResponseMessages.Add(
                    new ApiResponseMessages
                    {
                        Message = ex.Message,
                        MessageTypeEnum = MessageTypeEnum.Error
                    });
                return null;
            }
        }

        [HttpGet(RouteConstants.roles)]
        public async Task<HrmsJsonResult> GetRoles()
        {
            try
            {
                var validationMessages = new List<ApiResponseMessages>();
                var response = await _userService.GetRoles(validationMessages);
                var result = new HrmsJsonResult();
                result.success = true;
                if (UiHelper.CheckForValidationMessages(validationMessages))
                {
                    result.ApiResponseMessages = validationMessages;
                    return result;
                }
                result.Data = response;
                return result;
            }
            catch (Exception ex)
            {
                var response = new HrmsJsonResult();
                response.success = false;
                response.ApiResponseMessages.Add(
                    new ApiResponseMessages
                    {
                        Message = ex.Message,
                        MessageTypeEnum = MessageTypeEnum.Error
                    });
                return null;
            }
        }
        [HttpGet(RouteConstants.userdetails)]
        public async Task<HrmsJsonResult> GetUserDetailsData()
        {
            try
            {
                var response = new HrmsJsonResult();
                response.success = true;
                var validationmessages = new List<ApiResponseMessages>();
                var result = await _userService.GetUserDetailsData(validationmessages);
                if (UiHelper.CheckForValidationMessages(validationmessages))
                {
                    response.success = false;
                    response.ApiResponseMessages = validationmessages;
                    return response;
                }
                response.Data = result;
                return response;
            }
            catch(Exception ex)
            {
                var response = new HrmsJsonResult();
                response.success = false;
                response.ApiResponseMessages.Add(
                    new ApiResponseMessages
                    {
                        Message = ex.Message,
                        MessageTypeEnum = MessageTypeEnum.Error
                    });
                return null;
            }
        }
    }
}
