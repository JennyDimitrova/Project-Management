using Project_Management.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.DAL.Repositories.IRepositories
{
    public interface ITeamRepository
    {
        public Task<bool> CreateTeamAsync(string name, int creatorId);
        public Task<bool> EditTeamAsync(int teamId, string newName, int editorId);
        public Task<bool> DeleteTeamAsync(int teamId);
        public Task<Team> GetTeamByIDAsync(int id);
        public Task<List<Team>> GetAllTeamsAsync();
        public Task<List<Team>> GetAllTeamsOnProjectAsync(int projectId);
        public Task<bool> AssignTeamToProjectAsync(int teamId, int projectId);
    }
}
