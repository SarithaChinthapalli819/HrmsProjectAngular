using Api.Models;

namespace Api.Services
{
    public interface IProjectService
    {
        public bool UpsertProject(ProjectModel project, List<ApiResponseMessages> validationMessages);
        public List<ProjectModel> GetProject(List<ApiResponseMessages> validationMessages);
    }
}
