using Microsoft.EntityFrameworkCore;
using Project_Management.DAL.Entities;
using Project_Management.DAL.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Management.DAL.Repositories.IRepositories;

namespace Project_Management.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectManagementContext _database;

        public UserRepository(ProjectManagementContext database)
        {
            _database = database;
        }

        public User GetUserById(int Id)
        {
            return _database.Users.FirstOrDefault(u => u.ID == Id);
        }

        public User GetUserByCredentials(string username, string password)
        {
            var user = _database.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                if (user.Password != password)
                {
                    throw new Exception("Passwords does not match");
                }
            }
            return user;
        }
        public async Task<bool> CreateUserAsync(User userToAdd, int creatorID)
        {
            if (await _database.Users.FirstOrDefaultAsync(u => u.Username == userToAdd.Username) != null)
            {
                return false;
            }
            var user = new User
            {
                Username = userToAdd.Username,
                Password = userToAdd.Password,
                FirstName = userToAdd.FirstName,
                LastName = userToAdd.LastName,
                CreatorID = creatorID,
                Role = userToAdd.Role
            };
            await _database.AddAsync(user);
            await _database.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditUserAsync(int userToFind, User userToEdit, int editorID)
        {
            User user = await _database.Users.FirstAsync(u => u.ID == userToFind);

            if (user == null)
            {
                return false;
            }

            user.Username = userToEdit.Username;
            user.Password = userToEdit.Password;
            user.FirstName = userToEdit.FirstName;
            user.LastName = userToEdit.LastName;
            user.EditorID = editorID;
            user.EditedAt = DateTime.Now;
            user.Role = userToEdit.Role;
            await _database.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUserAsync(int Id)
        {
            User user = await _database.Users.FirstAsync(u => u.ID == Id);
            try
            {
                _database.Users.Remove(user);
                await _database.SaveChangesAsync();
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }
        public async Task<List<User>> GetUsersByTeamIdAsync(int teamId)
        {
            Team team = await _database.Teams.FirstAsync(t => t.ID == teamId);
            return team.Users.ToList();
        }

        public async Task<bool> AssignUserToTeamAsync(int teamId, int userId)
        {
            Team team = await _database.Teams.FirstAsync(t => t.ID == teamId);

            if (!team.Users.Any(u => u.ID == userId))
            {
                User user = await _database.Users.FirstAsync(u => u.ID == userId);
                team.Users.Add(user);
                await _database.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveUserFromTeamAsync(int userId, int teamId)
        {
            User user = await _database.Users.FirstAsync(u => u.ID == userId);
            Team team = await _database.Teams.FirstAsync(t => t.ID == teamId);
            if (team.Users.Contains(user))
            {
            team.Users.Remove(user);
            await _database.SaveChangesAsync();
            return true;
            }
            return false;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _database.Users.ToListAsync();
        }
    }
}
