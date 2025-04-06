using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DepartmentService.API.Services;
using DepartmentService.API.DTO;

namespace DepartmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnFacilitiesController : ControllerBase
    {
        private readonly IOwnFacilitiesService _ownFacilitiesService;

        public OwnFacilitiesController(IOwnFacilitiesService ownFacilitiesService)
        {
            _ownFacilitiesService = ownFacilitiesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnFacilitiesDTO>>> GetAllOwnFacilities()
        {
            var ownFacilities = await _ownFacilitiesService.GetAllOwnFacilities();
            return Ok(ownFacilities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OwnFacilitiesDTO>> GetOwnFacilitiesById(string id)
        {
            var ownFacility = await _ownFacilitiesService.GetOwnFacilitiesById(id);
            if (ownFacility == null)
            {
                return NotFound();
            }
            return Ok(ownFacility);
        }

        [HttpPost]
        public async Task<ActionResult<OwnFacilitiesDTO>> CreateOwnFacility(OwnFacilitiesUpsertRequest ownFacility)
        {
            var createdOwnFacility = await _ownFacilitiesService.UpsertOwner(ownFacility);
            return CreatedAtAction(nameof(GetOwnFacilitiesById), new { id = ownFacility.FacilityId }, createdOwnFacility);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OwnFacilitiesDTO>> UpdateOwnFacility(string id, OwnFacilitiesUpsertRequest ownFacility)
        {
            if (id != ownFacility.FacilityId)
            {
                return BadRequest();
            }
            var updatedOwnFacility = await _ownFacilitiesService.UpsertOwner(ownFacility);
            return Ok(updatedOwnFacility);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOwnFacility(string id)
        {
            var result = await _ownFacilitiesService.DeleteOwnFacilities(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
