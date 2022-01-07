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
    public class WorklogRepository : IWorklogRepository
    {
        private readonly ProjectManagementContext _database;

        public WorklogRepository(ProjectManagementContext database)
        {
            _database = database;
        }

        public async Task<bool> CreateLogAsync(int taskId, int hours, int creatorId)
        {
            Entities.Task task = await _database.Tasks.FirstOrDefaultAsync(t => t.ID == taskId);
            if (task != null)
            {
                var log = new WorkLog()
                {
                    TaskID = taskId,
                    UserId = creatorId,
                    WorkingHours = hours,
                    CreatorID = creatorId
                };
                await _database.WorkLogs.AddAsync(log);
                await _database.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> EditLogAsync(int logId, int hours, int editorId)
        {
            var log = await _database.WorkLogs.FirstOrDefaultAsync(w => w.ID == logId);
            if (log != null)
            {
                log.EditorID = editorId;
                log.EditedAt = DateTime.Now;
                log.WorkingHours = hours;
                await _database.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteLogAsync(int logId)
        {
            var log = await _database.WorkLogs.FirstOrDefaultAsync(w => w.ID == logId);
            try
            {
                _database.WorkLogs.Remove(log);
                await _database.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<WorkLog> GetLogByIDAsync(int Id)
        {
            return await _database.WorkLogs.FirstOrDefaultAsync(w => w.ID == Id);
        }
        public async Task<List<WorkLog>> GetAllLogsOnTaskAsync(int taskId)
        {
            Entities.Task task = await _database.Tasks.FirstOrDefaultAsync(t => t.ID == taskId);
            return task.WorkLogs.ToList();
        }
    }
}
