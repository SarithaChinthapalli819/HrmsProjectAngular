using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class TeamsService:ITeamsService
    {
        private readonly  ITeamsRepository _repository;
        public TeamsService(ITeamsRepository repository)
        {
            _repository = repository;
        }
        public bool UpsertTeams(TeamsModel teams, List<ApiResponseMessages> validationMessages)
        {
            return  _repository.UpsertTeams(teams, validationMessages);
        }
        public List<TeamsModel> GetTeams(List<ApiResponseMessages> validationMessages)
        {
            return _repository.GetTeams(validationMessages);
        }
        public bool UpsertTeamMembers(TeamsModel teams,List<ApiResponseMessages> validationmessages)
        {
            return _repository.UpsertTeamMembers(teams, validationmessages);
        }
        public List<TeamsModel> GetTeamMembers(TeamsModel teams, List<ApiResponseMessages> validationMessages)
        {
            return _repository.GetTeamMembers(teams, validationMessages);
        }
    }
}
