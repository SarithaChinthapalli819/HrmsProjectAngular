using Api.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace Api.Repositories
{
    public interface IProjectRepository
    {
        public bool UpsertProject(ProjectModel project,List<ApiResponseMessages> validationMessages);
        public List<ProjectModel> GetProject(List<ApiResponseMessages> validationMessages);
    }
}
