using Project_Management.DAL.Entities;
using Project_Management.DAL.Repositories.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Management.BLL.Services
{
    public class TeamService
    {
        private readonly ITeamRepository _teamRepository;
    
        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<bool> CreateTeamAsync(string name, int creatorId)
        {
            return await _teamRepository.CreateTeamAsync(name, creatorId);
        }

        public async Task<bool> EditTeamAsync(int teamId, string newName, int editorId)
        {
            return await _teamRepository.EditTeamAsync(teamId, newName, editorId);
        }

        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            return await _teamRepository.DeleteTeamAsync(teamId);
        }
        public async Task<Team> GetTeamByIDAsync(int id)
        {
            return await _teamRepository.GetTeamByIDAsync(id);
        }
        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await _teamRepository.GetAllTeamsAsync();
        }

        public async Task<List<Team>> GetAllTeamsOnProjectAsync(int projectId)
        {
            return await _teamRepository.GetAllTeamsOnProjectAsync(projectId);
        }

        public async Task<bool> AssignTeamToProjectAsync(int teamId, int projectId)
        {
            return await _teamRepository.AssignTeamToProjectAsync(teamId, projectId);
        }
    }
}
