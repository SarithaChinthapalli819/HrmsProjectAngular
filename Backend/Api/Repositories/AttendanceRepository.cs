using Api.Constants;
using Api.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Api.Repositories
{
    public class AttendanceRepository:IAttendanceRepository
    {
        private readonly IConfiguration _configuration;
        public AttendanceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool UpsertAttendanceData(AttendanceModel attendance, List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@UserId", attendance.UserId);
                    vparams.Add("@CheckInTime", attendance.CheckInTime);
                    vparams.Add("@CheckOutTime", attendance.CheckOutTime);
                    sqlConnection.Query<bool>(StoredProcedureContants.SP_UpsertAttendanceData, vparams);
                }
                return true;
            }
            catch(SqlException ex)
            {
                validationMessages.Add(new ApiResponseMessages
                {
                    Message = ex.Message,
                    MessageTypeEnum = MessageTypeEnum.Error
                });
                return false;
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

        public List<AttendanceModel> GetAttendanceData(AttendanceModel attendance, List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@UserId", attendance.UserId);
                    vparams.Add("@DateFrom", attendance.DateFrom);
                    vparams.Add("@DateTo", attendance.DateTo);
                    return sqlConnection.Query<AttendanceModel>(StoredProcedureContants.SP_GetAttendanceData, vparams).ToList();
                }
                
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
