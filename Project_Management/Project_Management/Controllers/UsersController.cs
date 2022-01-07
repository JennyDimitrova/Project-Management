using Microsoft.AspNetCore.Mvc;
using Project_Management.DAL.Database;
using Project_Management.DAL.Entities;
using Project_Management.BLL.Services;
using Project_Management.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Management.DTO_Models.Responses.Users;
using Project_Management.DTO_Models.Requests.Users;
using Project_Management.WEB.Auth;
using AutoMapper;

namespace Project_Management.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public UsersController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/Login")]
        public void Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                User current = _mapper.Map<User>(user);
                _userService.Login(user.Username, user.Password);
                Ok("Successfully logged in.");
            }
            else
            {
                BadRequest();
            }
        }

        [HttpPost]
        [Route("/CreateUser")]
        public async Task<IActionResult> CreateUser(UserReg userRegister)
        {
            User currentUser = _userService.GetCurrentUser(Request);

            if (ModelState.IsValid)
            {
                if (currentUser.Role == Role.ADMIN)
                {
                    var userToAdd = _mapper.Map<User>(userRegister);
                    var created = await _userService.CreateUser(userToAdd, currentUser.ID);
                    return CreatedAtAction(nameof(CreateUser), new { Username = userToAdd.Username }, userToAdd.Username);
                }
                return Unauthorized("You are not ad ADMIN");
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("/{Id}")]
        public async Task<IActionResult> EditUser(int Id, UserReg user)
        {
            User currentUser = _userService.GetCurrentUser(Request);

            if (ModelState.IsValid)
            {
                if (currentUser.Role == Role.ADMIN)
                {
                    var userToEdit = _mapper.Map<User>(user);
                    await _userService.EditUser(Id, userToEdit, currentUser.ID);
                    return Ok("User edited successfully.");
                }
                return Unauthorized();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("/{Id}")]
        public UserResponse Get(int Id)
        {
            User user = _userService.GetUserById(Id);
            UserResponse dto = _mapper.Map<UserResponse>(user);
            return dto;
        }

        [HttpGet]
        [Route("All")]
        public async Task<List<UserResponse>> GetAll()
        {
            List<UserResponse> mappedUsers = new();
            List<User> dbUsers = await _userService.GetAllUsers();
            foreach (var user in dbUsers)
            {
                UserResponse dto = _mapper.Map<UserResponse>(user);
                mappedUsers.Add(dto);
            }
            return mappedUsers;
        }

        [HttpDelete]
        [Route("/{Id}")]
        public IActionResult DeleteUser(int Id)
        {
            User currentUser = _userService.GetCurrentUser(Request);

            if (currentUser.ID == Id)
            {
                return Forbid("You are not allowed to delete yourself");
            }

            if (currentUser.Role == Role.ADMIN)
            {
                _userService.DeleteUser(Id);
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("GetByTeam/{Id}")]
        public async Task<List<UserResponse>> GetUsersByTeam(int Id)
        {
            List<UserResponse> mappedUsers = new();
            List<User> dbUsers = await _userService.GetUsersByTeamId(Id);
            foreach (var user in dbUsers)
            {
                UserResponse dto = _mapper.Map<UserResponse>(user);
                mappedUsers.Add(dto);
            }
            return mappedUsers;
        }

        [HttpPatch]
        [Route("AssignUserToTeam/{teamId:int}&{userId:int}")]
        public async Task<IActionResult> AssignUserToTeam(int teamId, int userId)
        {
            User currentUser = _userService.GetCurrentUser(Request);

            if (currentUser.Role == Role.ADMIN)
            {
                if (await _userService.AssignUserToTeam(teamId, userId))
                {
                    return Ok("User assigned to the team!");
                }
                return BadRequest("User already assigned!");
            }
            return Unauthorized();
        }
    }
}
