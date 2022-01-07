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
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectManagementContext _database;

        public ProjectRepository(ProjectManagementContext database)
        {
            _database = database;
        }

        public async Task<bool> CreateProjectAsync(string title, int creatorId)
        {
            if (await _database.Projects.FirstOrDefaultAsync(p => p.Title == title) != null)
            {
                return false;
            }

            var project = new Project()
            {
                Title = title,
                CreatorID = creatorId                
            };
            await _database.Projects.AddAsync(project);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditProjectAsync(int projectId, string newTitle, int editorId)
        {
            Project toEdit = await _database.Projects.FirstOrDefaultAsync(p => p.ID == projectId);
            if (toEdit == null)
            {
                return false;
            }
            toEdit.Title = newTitle;
            toEdit.EditorID = editorId;
            toEdit.EditedAt = DateTime.Now;
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            Project toDelete = await _database.Projects.FirstOrDefaultAsync(p => p.ID == projectId);
            try
            {
                _database.Projects.Remove(toDelete);
                await _database.SaveChangesAsync();
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        public async Task<bool> AssignProjectToTeamAsync(int projectId, int teamId)
        {
            Project project = await _database.Projects.FirstOrDefaultAsync(p => p.ID == projectId);
            Team team = await _database.Teams.FirstOrDefaultAsync(t => t.ID == teamId);
            if (!project.Teams.Contains(team))
            {
                project.Teams.Add(team);
                await _database.SaveChangesAsync();
                return true;
            }
            return false;           
        }

        public async Task<List<User>> GetProjectUsers(int projectId)
        {
            Project project = await GetProjectByIdAsync(projectId);
            List<Team> teams = project.Teams.ToList();
            List<User> users = new();
            foreach (var team in teams)
            {
                users.AddRange(team.Users.ToList());
            }
            return users;
        }
        public async Task<List<Project>> GetProjectsByOwnerIdAsync(int ownerId)
        {
            List<Project> list = await _database.Projects.ToListAsync();
            return list.FindAll(p => p.CreatorID == ownerId);
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            return await _database.Projects.FirstOrDefaultAsync(p => p.ID == projectId);
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _database.Projects.ToListAsync();
        }
    }
}
