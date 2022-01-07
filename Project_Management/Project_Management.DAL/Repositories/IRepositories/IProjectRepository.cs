using Project_Management.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.DAL.Repositories.IRepositories
{
    public interface IProjectRepository
    {
        public Task<bool> CreateProjectAsync(string title, int creatorId);
        public Task<bool> EditProjectAsync(int projectId, string newTitle, int editorId);
        public Task<bool> DeleteProjectAsync(int projectId);
        public Task<bool> AssignProjectToTeamAsync(int projectId, int teamId);
        public Task<List<User>> GetProjectUsers(int projectId);
        public Task<List<Project>> GetProjectsByOwnerIdAsync(int ownerId);
        public Task<Project> GetProjectByIdAsync(int projectId);
        public Task<List<Project>> GetAllProjectsAsync();
    }
}
