using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class PayrollService:IPayrollService
    {
        private readonly IPayrollRepository _payrollRepository;
        public PayrollService(IPayrollRepository payrollRepository)
        {
            _payrollRepository = payrollRepository;
        }

        public bool UpsertPayrollData(PayrollModel model, List<ApiResponseMessages> validationMessages)
        {
            return _payrollRepository.UpsertPayrollData(model, validationMessages);
        }
        public List<PayrollModel> GetPayroll(List<ApiResponseMessages> validationMessages)
        {
            return _payrollRepository.GetPayroll(validationMessages);
        }
    }
}
