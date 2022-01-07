using Project_Management.DAL.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Management.BLL.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> CreateTaskAsync(string name, string description, string status, int projectId, int creatorId)
        {
            return await _taskRepository.CreateTaskAsync(name, description, status, projectId, creatorId);
        }
        public async Task<bool> EditTaskAsync(int taskId, string newName, string newDescription, string status, int projectId, int editorId)
        {
            return await _taskRepository.EditTaskAsync(taskId, newName, newDescription, status, projectId, editorId);
        }
        public async Task<bool> ChangeTaskStatusAsync(int taskId, string newStatus, int editorId)
        {
            return await _taskRepository.ChangeTaskStatusAsync(taskId, newStatus, editorId);
        }
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            return await _taskRepository.DeleteTaskAsync(taskId);
        }
        public async Task<DAL.Entities.Task> GetTaskByIDAsync(int Id)
        {
            return await _taskRepository.GetTaskByIDAsync(Id);
        }
        public async Task<List<DAL.Entities.Task>> GetAllTasksOnProjectAsync(int projectId)
        {
            return await _taskRepository.GetAllTasksOnProjectAsync(projectId);
        }
        public async Task<bool> AssignTaskToUser(int taskId, int assignedUserId)
        {
            return await _taskRepository.AssignTaskToUser(taskId, assignedUserId);
        }

        public async Task<int> GetTotalWorkingHoursAsync(int taskId)
        {
            return await _taskRepository.GetTotalWorkingHoursAsync(taskId);
        }
    }
}
