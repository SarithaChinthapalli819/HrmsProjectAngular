using Api.Models;

namespace Api.Services
{
    public interface ITaskService
    {
        public bool UpsertTask(TaskModel task, List<ApiResponseMessages> validationMessages);
        public List<TaskModel> GetTask(TaskModel taskModel, List<ApiResponseMessages> validationMessages);
        public bool UpsertSpentTime(UserTaskSpentTimeModel task, List<ApiResponseMessages> validationMessages);
        public UserTaskSpentTimeModel GetSpentTime(UserTaskSpentTimeModel model, List<ApiResponseMessages> validationMessages);
    }
}
