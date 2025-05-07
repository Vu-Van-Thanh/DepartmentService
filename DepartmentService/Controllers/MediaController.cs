using Microsoft.AspNetCore.Mvc;
using DepartmentService.API.Services;
using DepartmentService.API.DTO;

namespace DepartmentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaInfo>>> GetAllMedia()
        {
            var media = await _mediaService.GetAllMedia();
            return Ok(media);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MediaInfo>> GetMediaById(Guid id)
        {
            var media = await _mediaService.GetMediaById(id);
            if (media == null)
            {
                return NotFound();
            }
            return Ok(media);
        }

        [HttpPost]
        public async Task<ActionResult<MediaInfo>> CreateMedia(MediaUpsertRequest media)
        {
            var createdMedia = await _mediaService.UpsertMedia(media);
            return CreatedAtAction(nameof(GetMediaById), new { id = media.MediaID }, createdMedia);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MediaInfo>> UpdateMedia(Guid id, MediaUpsertRequest media)
        {
            if (id != media.MediaID)
            {
                return BadRequest();
            }
            var updatedMedia = await _mediaService.UpsertMedia(media);
            return Ok(updatedMedia);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedia(Guid id)
        {
            var result = await _mediaService.DeleteMedia(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
} 