using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class ProjectService:IProjectService
    {
        private readonly IProjectRepository _repository;
        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }
        public bool UpsertProject(ProjectModel project,List<ApiResponseMessages> validationMessages)
        {
            return _repository.UpsertProject(project, validationMessages);
        }
        public List<ProjectModel> GetProject(List<ApiResponseMessages> validationMessages)
        {
            return _repository.GetProject(validationMessages);
        }
    }
}
