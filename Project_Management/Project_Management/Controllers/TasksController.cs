using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project_Management.BLL.Services;
using Project_Management.DAL.Entities;
using Project_Management.DTO_Models.Requests.Tasks;
using Project_Management.DTO_Models.Responses.Tasks;
using Project_Management.WEB.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private readonly ProjectService _projectService;
        private readonly IMapper _mapper;

        public TasksController(TaskService taskService, UserService userService, ProjectService projectService, IMapper mapper)
        {
            _taskService = taskService;
            _userService = userService;
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/CreateTask")]
        public async Task<IActionResult> CreateTask(TaskRequest taskRequest)
        {
            User user = _userService.GetCurrentUser(Request);
            Project project = await _projectService.GetProjectByIdAsync(taskRequest.ProjectID);
            
            if (ModelState.IsValid && user != null)
            {
                if (user.ID == project.CreatorID)
                {
                    var created = await _taskService
                        .CreateTaskAsync(taskRequest.Name, taskRequest.Description, taskRequest.Status, taskRequest.ProjectID, user.ID);
                    return Ok("Task created successfully");
                }
                return Unauthorized();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("/EditTask")]
        public async Task<IActionResult> EditTaskAsync(int taskId, TaskRequest taskRequest)
        {
            User user = _userService.GetCurrentUser(Request);
            DAL.Entities.Task task = await _taskService.GetTaskByIDAsync(taskId);
            Project project = await _projectService.GetProjectByIdAsync(taskRequest.ProjectID);
            if (ModelState.IsValid && user != null)
            {
                if (user.ID == project.CreatorID)
                {
                    await _taskService
                        .EditTaskAsync(taskId, taskRequest.Name, taskRequest.Description, taskRequest.Status, taskRequest.ProjectID, user.ID);
                    return Ok("Task edited successfully.");
                }
            }
            return BadRequest();
        }

        [HttpPatch]
        [Route("/ChangeTask")]
        public async Task<IActionResult> ChangeTaskStatus(int taskId, string newStatus, int editorId)
        {
            User user = _userService.GetCurrentUser(Request);
            if (ModelState.IsValid && user != null)
            {
                await _taskService.ChangeTaskStatusAsync(taskId, newStatus, editorId);
                return Ok("Task status changed successfully");
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("/DeleteTask/{Id}")]
        public async Task<IActionResult> DeleteTaskAsync(int Id)
        {
            User user = _userService.GetCurrentUser(Request);
            if (user != null)
            {
                DAL.Entities.Task task = await _taskService.GetTaskByIDAsync(Id);
                if (user.ID == task.CreatorID)
                {
                    await _taskService.DeleteTaskAsync(Id);
                    return Ok("Task deleted successfully");
                }
                return Unauthorized("You cannot delete this task");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("/GetTaskById/{Id}")]
        public async Task<TaskResponse> GetTaskByIDAsync(int Id)
        {
            DAL.Entities.Task task = await _taskService.GetTaskByIDAsync(Id);
            TaskResponse res = _mapper.Map<TaskResponse>(task);
            return res;
        }

        [HttpGet]
        [Route("/GetAllTasksOnProject/{Id}")]
        public async Task<ActionResult<List<TaskResponse>>> GetAllTasksOnProject(int Id)
        {
            User user = _userService.GetCurrentUser(Request);
            Project project = await _projectService.GetProjectByIdAsync(Id);
            List<TaskResponse> res = new();
            if (user != null && user.ID == project.CreatorID)
            {
                List<DAL.Entities.Task> tasks = await _taskService.GetAllTasksOnProjectAsync(Id);
                foreach (var task in tasks)
                {
                    TaskResponse taskResponse = _mapper.Map<TaskResponse>(task);
                    res.Add(taskResponse);
                }
                return res;
            }
            return BadRequest();
        }

        [HttpPatch]
        [Route("/AssignTaskToUser/{taskId:int}&{assignedUserId:int}")]
        public async Task<IActionResult> AssignTaskToUser(int taskId, int assignedUserId)
        {
            User user = _userService.GetCurrentUser(Request);
            DAL.Entities.Task task = await _taskService.GetTaskByIDAsync(taskId);
            if (user.ID == task.CreatorID)
            {
                if (await _taskService.AssignTaskToUser(taskId, assignedUserId))
                {
                    return Ok("Task assigned to user");
                }
                return BadRequest();
            }
            return Unauthorized("You cannot assign task to user");
        }

        [HttpGet]
        [Route("/GetTaskTotalWorkingHours/{Id}")]
        public async Task<int> GetTotalWorkingHoursAsync(int taskId)
        {
           return await _taskService.GetTotalWorkingHoursAsync(taskId);
        }
    }
}
