using Microsoft.EntityFrameworkCore;
using Project_Management.DAL.Database;
using Project_Management.DAL.Entities;
using Project_Management.DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ProjectManagementContext _database;

        public TaskRepository(ProjectManagementContext database)
        {
            _database = database;
        }

        public async Task<bool> CreateTaskAsync(string name, string description, string status, int projectId, int creatorId)
        {
            if (await _database.Tasks.FirstOrDefaultAsync(t => t.Name == name) != null)
            {
                return false;
            }

            var task = new Entities.Task()
            {
                Name = name,
                Description = description,
                Status = status,
                AssignedUserID = creatorId,
                ProjectID = projectId,
                CreatorID = creatorId,
            };

            await _database.Tasks.AddAsync(task);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditTaskAsync(int taskId, string newName, string newDescription, string status, int projectId, int editorId)
        {
            Entities.Task taskToEdit = await _database.Tasks.FirstOrDefaultAsync(t => t.ID == taskId);

            if (taskToEdit == null)
            {
                return false;
            }
            taskToEdit.Name = newName;
            taskToEdit.Description = newDescription;
            taskToEdit.Status = status;
            taskToEdit.ProjectID = projectId;
            taskToEdit.EditorID = editorId;
            taskToEdit.EditedAt = DateTime.Now;
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeTaskStatusAsync(int taskId, string newStatus, int editorId)
        {
            Entities.Task task = await _database.Tasks.FirstOrDefaultAsync(t => t.ID == taskId);

            if (task == null)
            {
                return false;
            }
            task.Status = newStatus;
            task.EditorID = editorId;
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            Entities.Task task = await _database.Tasks.FirstOrDefaultAsync(t => t.ID == taskId);

            try
            {
                _database.Tasks.Remove(task);
                await _database.SaveChangesAsync();
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public async Task<Entities.Task> GetTaskByIDAsync(int Id)
        {
            return await _database.Tasks.FirstOrDefaultAsync(t => t.ID == Id);
        }

        public async Task<List<Entities.Task>> GetAllTasksOnProjectAsync(int projectId)
        {
            List<Entities.Task> tasks = await _database.Tasks.ToListAsync();
            List<Entities.Task> result = new();
            foreach (var task in tasks)
            {
                if (task.ProjectID == projectId)
                {
                    result.Add(task);
                }
            }
            return result;
        }

        public async Task<bool> AssignTaskToUser(int taskId, int assignedUserId)
        {
            Entities.Task task = await _database.Tasks.FirstOrDefaultAsync(t => t.ID == taskId);
            User user = await _database.Users.FirstOrDefaultAsync(u => u.ID == assignedUserId);
            if (task != null && user != null && !user.Tasks.Contains(task))
            {
                task.AssignedUserID = assignedUserId;
                await _database.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<int> GetTotalWorkingHoursAsync(int taskId)
        {
            Entities.Task task = await _database.Tasks.FirstOrDefaultAsync(t => t.ID == taskId);
            return task.WorkLogs.Sum(w => w.WorkingHours);
        }
    }
}
