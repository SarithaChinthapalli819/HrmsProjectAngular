using Api.Constants;
using Api.Models;
using Dapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.SqlClient;

namespace Api.Repositories
{
    public class UserRepository:IUserRepository
    {
        
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {  
            _configuration = configuration;
        }
        public bool UpsertUser(UserModel user,List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using(var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@UserId", user.UserId);
                    vparams.Add("@UserName", user.UserName);
                    vparams.Add("@Email", user.Email);
                    vparams.Add("@FirstName", user.FirstName);
                    vparams.Add("@LastName", user.LastName);
                    vparams.Add("@PasswordHash", user.Password);
                    vparams.Add("@RoleId", user.RoleId);
                    vparams.Add("@DepartmentId", user.DepartmentId);
                    vparams.Add("@DesignationId", user.DesignationId);
                    vparams.Add("@IsActive", user.IsActive);
                    vparams.Add("@JoiningDate", user.JoiningDate);
                    vparams.Add("@IsRegister", user.IsRegister);
                    vparams.Add("@EmployeeCode", user.EmployeeCode);
                    connection.Query<bool>(StoredProcedureContants.SP_UpsertUser, vparams);
                }
                return true;
            }
            catch (Exception ex)
            {
                validationMessages.Add(
                    new ApiResponseMessages
                    {
                        Message = ex.Message,
                        MessageTypeEnum = MessageTypeEnum.Error
                    }
                    );
                return false;
            }
        }
        public async Task<UserModel> GetUserDetails(UserModel model, List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connection = _configuration.GetConnectionString("defaultConnection");
                using(var sqlconnection =  new SqlConnection(connection))
                {
                    sqlconnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@UserName", model.UserName);
                    return await sqlconnection.QueryFirstOrDefaultAsync<UserModel>(StoredProcedureContants.SP_GetUserDetailsWithUsername, vparams);
                }
            }
            catch(Exception ex)
            {
                validationMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return null;
            }
        }
        public async Task<List<DepartmentsOuptputModel>> GetDepartments(List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionstring = _configuration.GetConnectionString("defaultConnection");
                using(var sqlConnection = new SqlConnection(connectionstring))
                {
                    sqlConnection.Open();
                    var response =  await sqlConnection.QueryAsync<DepartmentsOuptputModel>(StoredProcedureContants.SP_GetDepartments);
                    return response.ToList();
                }

            }
            catch(Exception ex)
            {
                validationMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return new List<DepartmentsOuptputModel>();
            }
        }
        public async Task<List<DesignationOuptputModel>> GetDesignations(List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionstring = _configuration.GetConnectionString("defaultConnection");
                using(var sqlConnection = new SqlConnection(connectionstring))
                {
                    sqlConnection.Open();
                    var response = await sqlConnection.QueryAsync<DesignationOuptputModel>(StoredProcedureContants.SP_GetDesignations);
                    return response.ToList();
                }
            }
            catch (Exception ex)
            {
                validationMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return null;
            }

        }
        public async Task<List<RoleOutputModel>> GetRoles(List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionstring = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionstring))
                {
                    sqlConnection.Open();
                    var response = await sqlConnection.QueryAsync<RoleOutputModel>(StoredProcedureContants.SP_Roles);
                    return response.ToList();
                }
            }
            catch (Exception ex)
            {
                validationMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return null;
            }

        }

        public async Task<List<UserModel>> GetUserDetailsData(List<ApiResponseMessages> validationmessages)
        {
            try
            {
                var connectionstring = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionstring))
                {
                    sqlConnection.Open();
                    var response = await sqlConnection.QueryAsync<UserModel>(StoredProcedureContants.SP_GetUserDetails);
                    return response.ToList();
                }
            }
            catch(Exception ex)
            {
                validationmessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return null;
            }
        }
    }
}
