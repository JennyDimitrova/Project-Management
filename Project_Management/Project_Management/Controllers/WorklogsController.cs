using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project_Management.BLL.Services;
using Project_Management.DAL.Entities;
using Project_Management.DTO_Models.Responses.Worklogs;
using Project_Management.WEB.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorklogsController : ControllerBase
    {
        private readonly WorklogService _worklogService;
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public WorklogsController(WorklogService worklogService, TaskService taskService, UserService userService, IMapper mapper)
        {
            _worklogService = worklogService;
            _taskService = taskService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/CreateWorklog")]
        public async Task<IActionResult> CreateLog(int taskId, int hours, int creatorId)
        {
            User currentUser = _userService.GetCurrentUser(Request);
            DAL.Entities.Task task = await _taskService.GetTaskByIDAsync(taskId);
            if (ModelState.IsValid && currentUser != null)
            {
                if (task.AssignedUserID == currentUser.ID)
                {
                var created = await _worklogService.CreateLogAsync(taskId, hours, creatorId);
                return Ok("Worklog successfully created");
                }
                return Unauthorized();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("/EditLog")]
        public async Task<IActionResult> EditLog(int logId, int hours, int editorId)
        {
            User currentUser = _userService.GetCurrentUser(Request);
            if (ModelState.IsValid && currentUser != null)
            {
                if (editorId == currentUser.ID)
                {
                    var created = await _worklogService.EditLogAsync(logId, hours, editorId);
                    return Ok("Worklog successfully edited");
                }
                return Unauthorized();
            }
            return BadRequest();
        }


        [HttpDelete]
        [Route("/DeleteLog/{Id}")]
        public async Task<IActionResult> DeleteLog(int Id)
        {
            User user = _userService.GetCurrentUser(Request);
            if (user != null)
            {
                WorkLog log = await _worklogService.GetLogByIDAsync(Id);
                if (user.ID == log.CreatorID)
                {
                    await _worklogService.DeleteLogAsync(Id);
                    return Ok("Log deleted successfully");
                }
                return Unauthorized("You cannot delete this log");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("/GetLogById/{Id}")]
        public async Task<WorklogResponse> GetLogByID(int Id)
        {
            WorkLog log = await _worklogService.GetLogByIDAsync(Id);
            WorklogResponse res = _mapper.Map<WorklogResponse>(log);
            return res;
        }

        [HttpGet]
        [Route("/GetAllLogsOnTask/{Id}")]
        public async Task<ActionResult<List<WorklogResponse>>> GetAllLogsOnTask(int taskId)
        {
            List<WorkLog> logs = await _worklogService.GetAllLogsOnTaskAsync(taskId);
            List<WorklogResponse> res = new();
            foreach (var log in logs)
            {
                WorklogResponse dto = _mapper.Map<WorklogResponse>(log);
                res.Add(dto);
            }
            return res;
        }
    }
}
