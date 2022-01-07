using Project_Management.DAL.Entities;
using Project_Management.DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.BLL.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<bool> CreateProjectAsync(string title, int creatorId)
        {
            return await _projectRepository.CreateProjectAsync(title, creatorId);
        }

        public async Task<bool> EditProjectAsync(int projectId, string newTitle, int editorId)
        {
            return await _projectRepository.EditProjectAsync(projectId, newTitle, editorId);
        }
        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            return await _projectRepository.DeleteProjectAsync(projectId);
        }
        public async Task<bool> AssignProjectToTeamAsync(int projectId, int teamId)
        {
            return await _projectRepository.AssignProjectToTeamAsync(projectId, teamId);
        }
        public async Task<List<User>> GetProjectUsers(int projectId)
        {
            return await _projectRepository.GetProjectUsers(projectId);
        }
        public async Task<List<Project>> GetProjectsByOwnerIdAsync(int ownerId)
        {
            return await _projectRepository.GetProjectsByOwnerIdAsync(ownerId);
        }
        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            return await _projectRepository.GetProjectByIdAsync(projectId);
        }
        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllProjectsAsync();
        }
    }
}
