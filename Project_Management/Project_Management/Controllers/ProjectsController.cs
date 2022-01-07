using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project_Management.BLL.Services;
using Project_Management.DAL.Entities;
using Project_Management.DTO_Models.Responses.Projects;
using Project_Management.DTO_Models.Responses.Users;
using Project_Management.WEB.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public ProjectsController(ProjectService projectService, UserService userService, IMapper mapper)
        {
            _projectService = projectService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/CreateProject")]
        public async Task<IActionResult> CreateProject(string title)
        {
            User user = _userService.GetCurrentUser(Request);

            if (ModelState.IsValid && user != null)
            {
                var created = await _projectService.CreateProjectAsync(title, user.ID);
                return CreatedAtAction(nameof(CreateProject), new { Title = title }, title);
            }
            return BadRequest();
        }

        [HttpPatch]
        [Route("/EditProject")]
        public async Task<IActionResult> EditProject(int projectId, string newTitle)
        {
            User user = _userService.GetCurrentUser(Request);
            Project project = await _projectService.GetProjectByIdAsync(projectId);
            var projectUsers = await _projectService.GetProjectUsers(projectId);
            
            if (ModelState.IsValid && user != null)
            {
                if (user.ID == project.CreatorID || projectUsers.Contains(user))
                {
                    await _projectService.EditProjectAsync(projectId, newTitle, user.ID);
                    return Ok("Project edited successfully.");
                }
                return Unauthorized("You cannot edit list which is not yours");
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("/DeleteProject/{Id}")]
        public async Task<IActionResult> DeleteProject(int Id)
        {
            User user = _userService.GetCurrentUser(Request);
            if (user != null)
            {
                Project project = await _projectService.GetProjectByIdAsync(Id);

                if (user.ID == project.CreatorID)
                {
                    await _projectService.DeleteProjectAsync(Id);
                    return Ok("Project deleted successfully");
                }
                return Unauthorized("You don't have permissions to delete this project");
            }
            return Unauthorized("You must be logged in");
        }

        [HttpPatch]
        [Route("/AssignProjectToTeam/{projectId:int}&{teamId:int}")]
        public async Task<IActionResult> AssignProjectToTeam(int projectId, int teamId)
        {
            User user = _userService.GetCurrentUser(Request);
            Project project = await _projectService.GetProjectByIdAsync(projectId);
            if (user.ID == project.CreatorID)
            {
                if (await _projectService.AssignProjectToTeamAsync(projectId, teamId))
                {
                    return Ok("Project assigned to team");
                }
                return BadRequest();
            }
            return Unauthorized("You cannot assign project to team");
        }

        [HttpGet]
        [Route("/GetProjectUsers/{Id}")]
        public async Task<List<UserResponse>> GetProjectUsers(int Id)
        {
            List<UserResponse> res = new();
            List<User> users = await _projectService.GetProjectUsers(Id);
            foreach (var user in users)
            {
                UserResponse dto = _mapper.Map<UserResponse>(user);
                res.Add(dto);
            }
            return res;
        }

        [HttpGet]
        [Route("/GetProjectsByOwner/{Id}")]
        public async Task<List<ProjectResponse>> GetProjectsByOwnerId(int Id)
        {
            List<ProjectResponse> res = new();
            List<Project> projects = await _projectService.GetProjectsByOwnerIdAsync(Id);
            foreach (var project in projects)
            {
                ProjectResponse dto = _mapper.Map<ProjectResponse>(project);
                res.Add(dto);
            }
            return res;
        }

        [HttpGet]
        [Route("/GetProjectById/{Id}")]
        public async Task<ProjectResponse> GetProjectById(int Id)
        {
            Project project = await _projectService.GetProjectByIdAsync(Id);
            ProjectResponse dto = _mapper.Map<ProjectResponse>(project);
            return dto;
        }

        [HttpGet]
        [Route("/GetAllProjects")]
        public async Task<List<ProjectResponse>> GetAllProjects()
        {
            List<ProjectResponse> res = new();
            List<Project> projects = await _projectService.GetAllProjectsAsync();
            foreach (var project in projects)
            {
                ProjectResponse dto = _mapper.Map<ProjectResponse>(project);
                res.Add(dto);
            }
            return res;
        }
    }
}
