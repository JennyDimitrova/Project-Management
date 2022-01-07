using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Management.DAL.Repositories.IRepositories
{
    public interface ITaskRepository
    {
        public Task<bool> CreateTaskAsync(string name, string description, string status, int projectId, int creatorId);
        public Task<bool> EditTaskAsync(int taskId, string newName, string newDescription, string status, int projectId, int editorId);
        public Task<bool> ChangeTaskStatusAsync(int taskId, string newStatus, int editorId);
        public Task<bool> DeleteTaskAsync(int taskId);
        public Task<Entities.Task> GetTaskByIDAsync(int Id);
        public Task<List<Entities.Task>> GetAllTasksOnProjectAsync(int projectId);
        public Task<bool> AssignTaskToUser(int taskId, int assignedUserId);
        public Task<int> GetTotalWorkingHoursAsync(int taskId);
    }
}
