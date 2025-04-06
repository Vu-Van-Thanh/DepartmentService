using DepartmentService.API.DTO;
using DepartmentService.API.Entities;
using DepartmentService.API.Repositories;
using DepartmentService.Repositories;

namespace DepartmentService.API.Services
{
    public interface IMediaService
    {
        Task<IEnumerable<MediaInfo>> GetAllMedia();
        Task<MediaInfo> GetMediaById(Guid mediaId);
        Task<MediaInfo> UpsertMedia(MediaUpsertRequest media);
        Task<bool> DeleteMedia(Guid mediaId);
    }

    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;

        public MediaService(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<bool> DeleteMedia(Guid mediaId)
        {
            return await _mediaRepository.DeleteAsync(m => m.MediaID == mediaId);
        }

        public async Task<IEnumerable<MediaInfo>> GetAllMedia()
        {
            var mediaList = await _mediaRepository.GetAllAsync();
            return mediaList.Select(m => m.ToMediaInfo());
        }

        public async Task<MediaInfo> GetMediaById(Guid mediaId)
        {
            var media = await _mediaRepository.GetByIdAsync(mediaId);
            return media?.ToMediaInfo();
        }

        public async Task<MediaInfo> UpsertMedia(MediaUpsertRequest media)
        {
            var result = await _mediaRepository.UpsertAsync(media.ToMedia(), m => m.MediaID == media.MediaID);
            return result.ToMediaInfo();
        }
    }
} 