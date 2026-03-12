using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public class TaskService:ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public bool UpsertTask(TaskModel task,List<ApiResponseMessages> validationMessages)
        {
            return _taskRepository.UpsertTask(task,validationMessages);
        }

        public List<TaskModel> GetTask(TaskModel taskModel, List<ApiResponseMessages> validationMessages)
        {
            return _taskRepository.GetTask(taskModel,validationMessages);
        }

        public bool UpsertSpentTime(UserTaskSpentTimeModel task, List<ApiResponseMessages> validationMessages)
        {
            return _taskRepository.UpsertSpentTime(task, validationMessages);
        }

        public UserTaskSpentTimeModel GetSpentTime(UserTaskSpentTimeModel model, List<ApiResponseMessages> validationMessages)
        {
            return _taskRepository.GetSpentTime(model,validationMessages);
        }

    }
}
