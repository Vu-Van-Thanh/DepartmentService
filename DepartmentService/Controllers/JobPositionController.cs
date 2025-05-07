using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DepartmentService.API.Services;
using DepartmentService.API.DTO;

namespace DepartmentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobPositionController : ControllerBase
    {
        private readonly IJobPositionService _jobPositionService;

        public JobPositionController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPositionInfo>>> GetAllJobs()
        {
            var jobs = await _jobPositionService.GetAllJob();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobPositionInfo>> GetJobById(string id)
        {
            var job = await _jobPositionService.GetJobById(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpPost]
        public async Task<ActionResult<JobPositionInfo>> CreateJob(JobPositionUpsertRequest job)
        {
            var createdJob = await _jobPositionService.UpsertJob(job);
            return CreatedAtAction(nameof(GetJobById), new { id = job.JobPositionId }, createdJob);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JobPositionInfo>> UpdateJob(string id, JobPositionUpsertRequest job)
        {
            if (id != job.JobPositionId)
            {
                return BadRequest();
            }
            var updatedJob = await _jobPositionService.UpsertJob(job);
            return Ok(updatedJob);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJob(string id)
        {
            var result = await _jobPositionService.DeleteJob(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
