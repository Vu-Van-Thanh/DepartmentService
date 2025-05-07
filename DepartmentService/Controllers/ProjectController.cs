using Microsoft.AspNetCore.Mvc;
using DepartmentService.Services;
using DepartmentService.API.DTO;

namespace DepartmentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectInfo>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectInfo>> GetProjectById(Guid id)
        {
            var project = await _projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpGet("department/{departmentId}")]
        public async Task<ActionResult<IEnumerable<ProjectInfo>>> GetProjectsByDepartment(string departmentId)
        {
            var projects = await _projectService.GetProjectsByDepartment(departmentId);
            return Ok(projects);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectInfo>> CreateProject(ProjectUpsertRequest project)
        {
            var createdProject = await _projectService.UpsertProject(project);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.ProjectId }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectInfo>> UpdateProject(Guid id, ProjectUpsertRequest project)
        {
            if (id != project.ProjectId)
            {
                return BadRequest();
            }
            var updatedProject = await _projectService.UpsertProject(project);
            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(Guid id)
        {
            var result = await _projectService.DeleteProject(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
} 