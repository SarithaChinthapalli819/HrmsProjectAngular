using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Components.Forms;

namespace Api.Services
{
   
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool Register(UserModel model,List<ApiResponseMessages> validationMessages)
        {
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var response = _userRepository.UpsertUser(model, validationMessages);
            return response;

        }
        public async Task<UserModel> GetUserDetails(UserModel model, List<ApiResponseMessages> validationMessages)
        {
            
            var response = await _userRepository.GetUserDetails(model, validationMessages);
            return response;

        }

        public async Task<List<DepartmentsOuptputModel>> GetDepartments(List<ApiResponseMessages> validationMesssages)
        {
            var response = await _userRepository.GetDepartments(validationMesssages);
            return response;
        }
        public async Task<List<DesignationOuptputModel>> GetDesignations(List<ApiResponseMessages> validationMesssages)
        {
            var response = await _userRepository.GetDesignations(validationMesssages);
            return response;
        }

       
        public async Task<List<RoleOutputModel>> GetRoles(List<ApiResponseMessages> validationMesssages)
        {
            var response = await _userRepository.GetRoles(validationMesssages);
            return response;
        }


        public async Task<List<UserModel>> GetUserDetailsData(List<ApiResponseMessages> validationmessages)
        {
            var response = await _userRepository.GetUserDetailsData(validationmessages);
            return response;
        }
    }
}
