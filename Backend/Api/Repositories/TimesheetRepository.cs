using Api.Constants;
using Api.Models;
using Dapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Data.SqlClient;

namespace Api.Repositories
{
    public class TimesheetRepository:ITimesheetRepository
    {
        private readonly IConfiguration _configuration;
        public TimesheetRepository(IConfiguration configuration) {
            _configuration = configuration;
        }
        public List<TimesheetModel>  GetTimesheetData(TimesheetModel model,List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@UserId", model.UserId);
                    vparams.Add("@Month", model.Month);
                    vparams.Add("@Year", model.Year);
                    return sqlConnection.Query<TimesheetModel>(StoredProcedureContants.SP_GetTimesheetData,vparams).ToList();
                };

            }
            catch (SqlException ex)
            {
                validationMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return null;
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

        public List<TimesheetModel> GetTimesheetDetailsData(TimesheetModel model, List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@UserId", model.UserId);
                    vparams.Add("@Month", model.Month);
                    vparams.Add("@Year", model.Year);
                    return sqlConnection.Query<TimesheetModel>(StoredProcedureContants.SP_GetTimesheetDetailsData, vparams).ToList();
                };

            }
            catch (SqlException ex)
            {
                validationMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return null;
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
