using Api.Constants;
using Api.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Api.Repositories
{
    public class ProjectRepository:IProjectRepository
    {
        private readonly IConfiguration _configuration;
        public ProjectRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool UpsertProject(ProjectModel project, List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@ProjectId", project.ProjectId);
                    vparams.Add("@ProjectName", project.ProjectName);
                    vparams.Add("@ClientName", project.ClientName);
                    vparams.Add("@Status", project.Status);
                    vparams.Add("@Description", project.Description);
                    vparams.Add("@Priority", project.Priority);
                    vparams.Add("@StartDate", project.StartDate);
                    vparams.Add("@Budget", project.Budget);
                    vparams.Add("@TeamId", project.TeamId);
                    sqlconnection.Query<bool>(StoredProcedureContants.SP_UpsertProject, vparams);
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
        public List<ProjectModel> GetProject(List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                   return sqlconnection.Query<ProjectModel>(StoredProcedureContants.SP_GetProject).ToList();
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
