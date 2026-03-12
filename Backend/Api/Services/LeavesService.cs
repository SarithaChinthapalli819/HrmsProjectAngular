using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class LeavesService:ILeavesService
    {
        private readonly ILeavesRepository _leavesRepository;
        public LeavesService(ILeavesRepository leavesRepository)
        {
            _leavesRepository = leavesRepository;
        }
        public List<LeaveTypesModel> getLeaveTypes(List<ApiResponseMessages> validationMessages)
        {
            return _leavesRepository.getLeaveTypes(validationMessages);
        }
        public List<LeavesModel> upsertLeaves(LeavesModel leaves,List<ApiResponseMessages> validationMessages)
        {
            return _leavesRepository.upsertLeaves(leaves, validationMessages);
        }
        public List<LeavesModel> getLeaves(List<ApiResponseMessages> validationMessages)
        {
            return _leavesRepository.getLeaves(validationMessages);
        }
    }
}
