using Project_Management.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.DAL.Repositories.IRepositories
{
    public interface IWorklogRepository
    {
        public Task<bool> CreateLogAsync(int taskId, int hours, int creatorId);
        public Task<bool> EditLogAsync(int logId, int hours, int editorId);
        public Task<bool> DeleteLogAsync(int logId);
        public Task<WorkLog> GetLogByIDAsync(int Id);
        public Task<List<WorkLog>> GetAllLogsOnTaskAsync(int taskId);
    }
}
