namespace DepartmentService.DTO
{
    public class ArticleUploadDTO
    {
        public IFormFile formFile { get; set; }
        public string? DepartmentID { get; set; }
        public string? Title { get; set; }
    }
}
