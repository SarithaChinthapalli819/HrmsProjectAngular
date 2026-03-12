using Api.Models;

namespace Api.Repositories
{
    public interface ILeavesRepository
    {
        public List<LeaveTypesModel> getLeaveTypes(List<ApiResponseMessages> validationMessages);
        public List<LeavesModel> upsertLeaves(LeavesModel leaves, List<ApiResponseMessages> validationMessages);
        public List<LeavesModel> getLeaves(List<ApiResponseMessages> validationMessages);
    }
}
