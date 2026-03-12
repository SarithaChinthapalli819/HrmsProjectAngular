using Api.Constants;
using Api.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Api.Repositories
{
    public class TeamsRepository:ITeamsRepository
    {
        private readonly IConfiguration _configuration;
        public TeamsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool UpsertTeams(TeamsModel teams,List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@TeamId", teams.TeamId);
                    vparams.Add("@TeamAdminId", teams.TeamAdminId);
                    vparams.Add("@IsActive", teams.IsActive);
                    vparams.Add("@TeamName", teams.TeamName);
                    vparams.Add("@Description", teams.Description);
                    sqlconnection.Query<TeamsModel>(StoredProcedureContants.SP_UpsertTeams, vparams);
                }
                return true;
            }
            catch (Exception ex)
            {
                validationMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return false;
            }   
        }

        public List<TeamsModel> GetTeams(List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using(var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    return sqlConnection.Query<TeamsModel>(StoredProcedureContants.SP_GetTeams).ToList();
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
        public bool UpsertTeamMembers(TeamsModel teams,List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@TeamId", teams.TeamId);
                    vparams.Add("@TeamMemberId", teams.TeamMemberId);
                    vparams.Add("@DeleteMember", teams.DeleteMember);
                    var response = sqlConnection.Query<bool>(StoredProcedureContants.SP_UpsertTeamMembers,vparams);
                    return true;
                }
            }
            catch (Exception ex)
            {
                validationMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return false;
            }
        }
        public List<TeamsModel> GetTeamMembers(TeamsModel teams, List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@TeamId", teams.TeamId);
                   return sqlConnection.Query<TeamsModel>(StoredProcedureContants.SP_GetTeammembers, vparams).ToList();
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
    }
}
