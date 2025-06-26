using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DepartmentService.API.Services;
using DepartmentService.API.DTO;

namespace DepartmentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentService departmentService, IWebHostEnvironment env)
        {
            _departmentService = departmentService;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentInfo>>> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartments();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentInfo>> GetDepartmentById(string id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentInfo>> CreateDepartment(DepartmentUpsertRequest department)
        {
            var createdDepartment = await _departmentService.UpsertDepartment(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.DepartmentId }, createdDepartment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentInfo>> UpdateDepartment(string id, DepartmentUpsertRequest department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }
            var updatedDepartment = await _departmentService.UpsertDepartment(department);
            return Ok(updatedDepartment);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(string id)
        {
            var result = await _departmentService.DeleteDepartment(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("Statistic/{*subfolder}")]
        public IActionResult GetFiles(string subfolder = "")
        {
            var rootPath = Path.Combine(_env.WebRootPath, subfolder ?? "");

            if (!Directory.Exists(rootPath))
                return NotFound("Thư mục không tồn tại.");

            var files = Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories)
                .Select(path => new
                {
                    name = Path.GetFileName(path),
                    fullPath = path.Replace(_env.WebRootPath, "").Replace("\\", "/"),
                    modified = System.IO.File.GetLastWriteTime(path),
                    size = new FileInfo(path).Length
                }).ToList();

            return Ok(files);
        }
    }
}
