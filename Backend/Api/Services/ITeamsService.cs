using Api.Models;

namespace Api.Services
{
    public interface ITeamsService
    {
        public bool UpsertTeams(TeamsModel teams, List<ApiResponseMessages> validationMessages);
        public List<TeamsModel> GetTeams(List<ApiResponseMessages> validationMessages);
        public bool UpsertTeamMembers(TeamsModel teams, List<ApiResponseMessages> validationMessages);
        public List<TeamsModel> GetTeamMembers(TeamsModel teams, List<ApiResponseMessages> validationMessages);
    }
}
