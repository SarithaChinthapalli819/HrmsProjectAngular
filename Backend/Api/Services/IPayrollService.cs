using Api.Models;

namespace Api.Services
{
    public interface IPayrollService
    {
        public bool UpsertPayrollData(PayrollModel model, List<ApiResponseMessages> validationMessages);
        public List<PayrollModel> GetPayroll(List<ApiResponseMessages> validationMessages);
    }
}
