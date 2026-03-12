using Api.Constants;
using Api.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Api.Repositories
{
    public class TaskRepository:ITaskRepository
    {
        private readonly IConfiguration _configuration;
        public TaskRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool UpsertTask(TaskModel task,List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@TaskId", task.TaskId);
                    vparams.Add("@TaskName", task.TaskName);
                    vparams.Add("@ProjectId", task.ProjectId);
                    vparams.Add("@Description", task.Description);
                    vparams.Add("@Priority", task.Priority);
                    vparams.Add("@DueDate", task.DueDate);
                    vparams.Add("@AssignedTo", task.AssignedTo);
                    vparams.Add("@IsUpdateStatus", task.IsUpdateStatus);
                    vparams.Add("@Status", task.Status);
                    sqlconnection.Query<bool>(StoredProcedureContants.SP_UpsertTask, vparams);
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

        public List<TaskModel> GetTask(TaskModel taskModel, List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@UserId", taskModel.UserId);
                    return sqlconnection.Query<TaskModel>(StoredProcedureContants.SP_GetTask,vparams).ToList();
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

        public bool UpsertSpentTime(UserTaskSpentTimeModel task, List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@TaskId", task.TaskId);
                    vparams.Add("@UserId", task.UserId);
                    vparams.Add("@Id", task.Id);
                    vparams.Add("@StartTime", task.StartTime);
                    vparams.Add("@EndTime", task.EndTime); 
                    sqlconnection.Query<bool>(StoredProcedureContants.SP_UpsertSpentTime, vparams);
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

        public UserTaskSpentTimeModel GetSpentTime(UserTaskSpentTimeModel task, List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@TaskId", task.TaskId);
                    vparams.Add("@UserId", task.UserId); 
                    return sqlconnection.Query<UserTaskSpentTimeModel>(StoredProcedureContants.SP_GetSpentTime, vparams).FirstOrDefault();
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
