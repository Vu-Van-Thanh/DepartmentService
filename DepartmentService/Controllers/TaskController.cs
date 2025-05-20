using Microsoft.AspNetCore.Mvc;
using DepartmentService.Services;
using DepartmentService.API.DTO;
using DepartmentService.DTO;

namespace DepartmentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IProjectTaskService _taskService;

        public TaskController(IProjectTaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectTaskInfo>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectTaskInfo>> GetTaskById(Guid id)
        {
            var task = await _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectTaskInfo>>> GetTasksByProject(Guid projectId)
        {
            var tasks = await _taskService.GetTasksByProject(projectId);
            return Ok(tasks);
        }

        [HttpGet("statistic")]
        public async Task<ActionResult<IEnumerable<DepartmentPerformance>>> GetTaskStatistics( [FromQuery] EmployeeInDepartment emp)
        {
            var statistics = await _taskService.GetTaskPerformance(emp);
            return Ok(statistics);
        }

        [HttpGet("assigned/{assignedTo}")]
        public async Task<ActionResult<IEnumerable<ProjectTaskInfo>>> GetTasksByAssignedTo(Guid assignedTo)
        {
            var tasks = await _taskService.GetTasksByAssignedTo(assignedTo);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectTaskInfo>> CreateTask(ProjectTaskUpsertRequest task)
        {
            var createdTask = await _taskService.UpsertTask(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.TaskId }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectTaskInfo>> UpdateTask(Guid id, ProjectTaskUpsertRequest task)
        {
            if (id != task.TaskId)
            {
                return BadRequest();
            }
            var updatedTask = await _taskService.UpsertTask(task);
            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            var result = await _taskService.DeleteTask(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
} 