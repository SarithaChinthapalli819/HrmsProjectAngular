using Api.Constants;
using Api.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Api.Repositories
{
    public class PayrollRepository:IPayrollRepository
    {
        private readonly IConfiguration _configuration;
        public PayrollRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool UpsertPayrollData(PayrollModel model, List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    var vparams = new DynamicParameters();
                    vparams.Add("@SalaryJson", model.SalaryJson);
                    vparams.Add("@Month", model.Month);
                    vparams.Add("@Year", model.Year);
                    vparams.Add("@PayrollId", model.PayrollId); 
                    sqlconnection.Query<bool>(StoredProcedureContants.SP_UpsertPayroll, vparams);
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
        public List<PayrollModel> GetPayroll(List<ApiResponseMessages> validationMessages)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("defaultConnection");
                using (var sqlconnection = new SqlConnection(connectionString))
                {
                    sqlconnection.Open();
                    var response = sqlconnection.Query<PayrollModel>(StoredProcedureContants.SP_GetPayroll).ToList();
                    return sqlconnection.Query<PayrollModel>(StoredProcedureContants.SP_GetPayroll).ToList();
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
