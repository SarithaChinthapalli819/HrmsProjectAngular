using Api.Models;

namespace Api.Repositories
{
    public interface IPayrollRepository
    {
        public bool UpsertPayrollData(PayrollModel model, List<ApiResponseMessages> validationMessages);
        public List<PayrollModel> GetPayroll(List<ApiResponseMessages> validationMessages);
    }
}
