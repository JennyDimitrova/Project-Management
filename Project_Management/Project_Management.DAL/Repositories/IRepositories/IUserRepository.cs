using Project_Management.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.DAL.Repositories.IRepositories
{
    public interface IUserRepository
    {
        public User GetUserById(int Id);
        public User GetUserByCredentials(string username, string password);
        public Task<bool> CreateUserAsync(User userToAdd, int creatorID);
        public Task<bool> EditUserAsync(int userToFind, User userToEdit, int editorID);
        public Task<bool> DeleteUserAsync(int userId);
        public Task<List<User>> GetUsersByTeamIdAsync(int teamId);
        public Task<bool> AssignUserToTeamAsync(int teamId, int userId);
        public Task<bool> RemoveUserFromTeamAsync(int teamId, int userId);
        public Task<List<User>> GetAllUsersAsync();
    }
}
