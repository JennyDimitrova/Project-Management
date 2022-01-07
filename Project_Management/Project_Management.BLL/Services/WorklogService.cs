using Project_Management.DAL.Entities;
using Project_Management.DAL.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Management.BLL.Services
{
    public class WorklogService
    {
        private readonly IWorklogRepository _worklogRepository;

        public WorklogService(IWorklogRepository worklogRepository)
        {
            _worklogRepository = worklogRepository;
        }

        public async Task<bool> CreateLogAsync(int taskId, int hours, int creatorId)
        {
            return await _worklogRepository.CreateLogAsync(taskId, hours, creatorId);
        }
        public async Task<bool> EditLogAsync(int logId, int hours, int editorId)
        {
            return await _worklogRepository.EditLogAsync(logId, hours, editorId);
        }
        public async Task<bool> DeleteLogAsync(int logId)
        {
            return await _worklogRepository.DeleteLogAsync(logId);
        }
        public async Task<WorkLog> GetLogByIDAsync(int Id)
        {
            return await _worklogRepository.GetLogByIDAsync(Id);
        }
        public async Task<List<WorkLog>> GetAllLogsOnTaskAsync(int taskId)
        {
            return await _worklogRepository.GetAllLogsOnTaskAsync(taskId);
        }
    }
}
