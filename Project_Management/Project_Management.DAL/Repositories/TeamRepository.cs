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
    public class TeamRepository : ITeamRepository
    {
        private readonly ProjectManagementContext _database;

        public TeamRepository(ProjectManagementContext database)
        {
            _database = database;
        }

        public async Task<bool> CreateTeamAsync(string name, int creatorId)
        {
            if (await _database.Teams.FirstOrDefaultAsync(t => t.Name == name) != null)
            {
                return false;
            }
            var team = new Team()
            {
                Name = name,
                CreatorID = creatorId,
                Users = new List<User>(),
                Projects = new List<Project>()
            };
            await _database.Teams.AddAsync(team);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditTeamAsync(int teamId, string newName, int editorId)
        {
            Team team = await _database.Teams.FirstAsync(t => t.ID == teamId);
            if (team == null)
            {
                return false;
            }
            team.Name = newName;
            team.EditorID = editorId;
            team.EditedAt = DateTime.Now;
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            Team team = await _database.Teams.FirstAsync(t => t.ID == teamId);
            try
            {
                _database.Teams.Remove(team);
                await _database.SaveChangesAsync();
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await _database.Teams.ToListAsync();
        }

        public async Task<List<Team>> GetAllTeamsOnProjectAsync(int projectId)
        {
            Project project = await _database.Projects.FirstAsync(project => project.ID == projectId);
            return project.Teams.ToList();
        }

        public async Task<Team> GetTeamByIDAsync(int id)
        {
            return await _database.Teams.FirstAsync(t => t.ID == id);
        }

        public async Task<bool> AssignTeamToProjectAsync(int teamId, int projectId)
        {
            Team team = await _database.Teams.FirstOrDefaultAsync(t => t.ID == teamId);
            Project project = await _database.Projects.FirstOrDefaultAsync(p => p.ID == projectId);
            if (!team.Projects.Contains(project))
            {
                team.Projects.Add(project);
                await _database.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
