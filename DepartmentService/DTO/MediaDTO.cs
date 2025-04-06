using DepartmentService.API.Entities;

namespace DepartmentService.API.DTO
{
    public class MediaUpsertRequest
    {
        public Guid MediaID { get; set; }
        public Guid ArticleID { get; set; }
        public string? MediaType { get; set; }
        public string? MediaUrl { get; set; }
        public string? MediaContent { get; set; }
    }

    public class MediaInfo
    {
        public Guid ArticleID { get; set; }
        public string? MediaType { get; set; }
        public string? MediaUrl { get; set; }
        public string? MediaContent { get; set; }
    }

    public static class MediaExtensions
    {
        public static MediaInfo ToMediaInfo(this Media media)
        {
            return new MediaInfo
            {
                ArticleID = media.ArticleID,
                MediaType = media.MediaType,
                MediaUrl = media.MediaUrl,
                MediaContent = media.MediaContent
            };
        }

        public static Media ToMedia(this MediaUpsertRequest media)
        {
            return new Media
            {
                MediaID = media.MediaID,
                ArticleID = media.ArticleID,
                MediaType = media.MediaType,
                MediaUrl = media.MediaUrl,
                MediaContent = media.MediaContent
            };
        }
    }
} 