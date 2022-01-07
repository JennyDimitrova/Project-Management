using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project_Management.BLL.Services;
using Project_Management.DAL.Entities;
using Project_Management.DTO_Models.Requests.Teams;
using Project_Management.DTO_Models.Responses.Teams;
using Project_Management.WEB.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly TeamService _teamService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public TeamsController(TeamService teamService, UserService userService, IMapper mapper)
        {
            _teamService = teamService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/CreateTeam")]
        public async Task<IActionResult> CreateTeam(string name)
        {
            User currentUser = _userService.GetCurrentUser(Request);

            if (ModelState.IsValid)
            {
                if (currentUser.Role == Role.ADMIN)
                {
                    var created = await _teamService.CreateTeamAsync(name, currentUser.ID);
                    return CreatedAtAction(nameof(CreateTeam), new { Name = name }, name);
                }
                return Unauthorized("You are not ad ADMIN");
            }
            return BadRequest();
        }

        [HttpPatch]
        [Route("/EditTeam")]
        public async Task<IActionResult> EditTeam(int teamId, string newName)
        {
            User currentUser = _userService.GetCurrentUser(Request);

            if (ModelState.IsValid)
            {
                if (currentUser.Role == Role.ADMIN)
                {
                    await _teamService.EditTeamAsync(teamId, newName, currentUser.ID);
                    return Ok("Team edited successfully.");
                }
                return Unauthorized();
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("/DeleteTeam/{Id}")]
        public async Task<IActionResult> DeleteTeam(int Id)
        {
            User currentUser = _userService.GetCurrentUser(Request);
            Team team = await _teamService.GetTeamByIDAsync(Id);

            if (currentUser.ID == team.CreatorID)
            {
                await _teamService.DeleteTeamAsync(Id);
                return Ok("Team deleted successfully");
            }
            return Unauthorized("You cannot delete this team");
        }
        
        [HttpGet]
        [Route("/GetTeamById/{Id}")]
        public async Task<TeamResponse> GetTeamByID(int Id)
        {
            Team team = await _teamService.GetTeamByIDAsync(Id);
            TeamResponse resp = _mapper.Map<TeamResponse>(team);
            return resp;
        }

        [HttpGet]
        [Route("/GetAllTeams")]
        public async Task<List<TeamResponse>> GetAllTeams()
        {
            List<TeamResponse> resp = new();
            List<Team> teams = await _teamService.GetAllTeamsAsync();
            foreach (var team in teams)
            {
                TeamResponse teamResp = _mapper.Map<TeamResponse>(team);
                resp.Add(teamResp);
            }
            return resp;
        }

        [HttpPatch]
        [Route("/AssignTeamToProject/{teamId:int}&{projectId:int}")]
        public async Task<IActionResult> AssignProjectToTeam(int teamId, int projectId)
        {
            if (await _teamService.AssignTeamToProjectAsync(teamId, projectId))
            {
                return Ok("Team assigned to project");
            }
            return BadRequest();
        }
    }
}
