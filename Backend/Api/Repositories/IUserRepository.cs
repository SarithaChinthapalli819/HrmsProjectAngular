using Api.Models;

namespace Api.Repositories
{
    public interface IUserRepository
    {
        public bool UpsertUser(UserModel model, List<ApiResponseMessages> validationMessages);
        public Task<UserModel> GetUserDetails(UserModel model, List<ApiResponseMessages> validationMessages);
        public Task<List<DesignationOuptputModel>> GetDesignations(List<ApiResponseMessages> validationMessages);
        public Task<List<DepartmentsOuptputModel>> GetDepartments(List<ApiResponseMessages> validationMessages);
        public Task<List<RoleOutputModel>> GetRoles(List<ApiResponseMessages> validationMessages);
        public Task<List<UserModel>> GetUserDetailsData(List<ApiResponseMessages> validationmessages);
    }

}
