using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project_Management.DAL.Entities;
using Project_Management.DAL.Repositories.IRepositories;

namespace Project_Management.BLL.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
       
        public void Login(string username, string password)
        {
            var user = _userRepository.GetUserByCredentials(username, password);

            if (user == null)
            {
            throw new Exception("User doesn't exist!");
            }
        }

        public User GetUserByCredentials(string username, string password)
        {
            return _userRepository.GetUserByCredentials(username, password);
        }

        public async Task<bool> CreateUser(User userToAdd, int creatorID)
        {
            return await _userRepository.CreateUserAsync(userToAdd, creatorID);
        }

        public async Task<bool> EditUser(int userToFind, User userToEdit, int editorID)
        {
            return await _userRepository.EditUserAsync(userToFind, userToEdit, editorID);
        }

        public User GetUserById(int Id)
        {
            return _userRepository.GetUserById(Id);
        }

        public Task<List<User>> GetAllUsers()
        {
            return _userRepository.GetAllUsersAsync();
        }

        public async void DeleteUser(int Id)
        {
            await _userRepository.DeleteUserAsync(Id);
        }

        public async Task<List<User>> GetUsersByTeamId(int teamId)
        {
            return await _userRepository.GetUsersByTeamIdAsync(teamId);
        }

        public async Task<bool> AssignUserToTeam(int teamId, int userId)
        {
            return await _userRepository.AssignUserToTeamAsync(teamId, userId);
        }
    }
}
