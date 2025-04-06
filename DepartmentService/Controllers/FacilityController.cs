using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DepartmentService.API.Services;
using DepartmentService.API.DTO;

namespace DepartmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacilityInfo>>> GetAllFacilities()
        {
            var facilities = await _facilityService.GetAllFacilities();
            return Ok(facilities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FacilityInfo>> GetFacilityById(string id)
        {
            var facility = await _facilityService.GetFacilityById(id);
            if (facility == null)
            {
                return NotFound();
            }
            return Ok(facility);
        }

        [HttpPost]
        public async Task<ActionResult<FacilityInfo>> CreateFacility(FacilityUpsertRequest facility)
        {
            var createdFacility = await _facilityService.UpsertFacility(facility);
            return CreatedAtAction(nameof(GetFacilityById), new { id = facility.FacId }, createdFacility);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FacilityInfo>> UpdateFacility(string id, FacilityUpsertRequest facility)
        {
            if (id != facility.FacId)
            {
                return BadRequest();
            }
            var updatedFacility = await _facilityService.UpsertFacility(facility);
            return Ok(updatedFacility);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFacility(string id)
        {
            var result = await _facilityService.DeleteFacility(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
