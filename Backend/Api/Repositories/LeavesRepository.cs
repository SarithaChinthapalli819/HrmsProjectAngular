using Api.Constants;
using Api.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Api.Repositories
{
    
    public class LeavesRepository:ILeavesRepository
    {
        private readonly IConfiguration _configuration;
        
        public LeavesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<LeaveTypesModel> getLeaveTypes(List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var vparams = new DynamicParameters();
                    return sqlConnection.Query<LeaveTypesModel>(StoredProcedureContants.SP_GetLeaveTypes).ToList();
                };
               
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
        public List<LeavesModel> upsertLeaves(LeavesModel leaves,List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@LeaveId", leaves.LeaveId);
                    vparams.Add("@LeaveTypeId", leaves.LeaveTypeId);
                    vparams.Add("@DateFrom", leaves.DateFrom);
                    vparams.Add("@DateTo", leaves.DateTo);
                    vparams.Add("@UserId", leaves.UserId);
                    vparams.Add("@Description", leaves.Description);
                    vparams.Add("@IsApproved", leaves.IsApproved);
                    vparams.Add("@IsArcheive", leaves.IsArcheive);
                    vparams.Add("@ApproveRequest", leaves.ApproveRequest);
                    return sqlConnection.Query<LeavesModel>(StoredProcedureContants.SP_UpsertLeave,vparams).ToList();
                };

            }
            catch(SqlException ex)
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

        public List<LeavesModel> getLeaves(List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    return sqlConnection.Query<LeavesModel>(StoredProcedureContants.SP_GetLeave).ToList();
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
