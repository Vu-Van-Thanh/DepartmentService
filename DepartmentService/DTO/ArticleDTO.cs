using DepartmentService.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.API.DTO
{
    public class ArticleUpsertRequest
    {
        public Guid ArticleId { get; set; }

        public string? DepartmentId { get; set; } 
        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime? DateCreated { get; set; }
        public string? Type { get; set; }

    }
    public class ArticleInfo
    {       
       
        public string? DepartmentId { get; set; } 
        public string Title { get; set; } 
        public string Content { get; set; } 
        public DateTime DateCreated { get; set; }
        public string Type { get; set; } 
    }

    public static class ArticleDTOExtensions
    {
        public static ArticleInfo ToArticleInfo(this Article article)
        {
            return new ArticleInfo
            {
                DepartmentId = article.DepartmentId,
                Title = article.Title,
                Content = article.Content,
                DateCreated = article.DateCreated,
                Type = article.Type
            };
        }

        public static Article ToArticle(this ArticleUpsertRequest article)
        {
            return new Article
            {
                ArticleId = article.ArticleId,
                DepartmentId = article.DepartmentId,
                Title = article.Title,
                Content = article.Content,
                DateCreated = article.DateCreated ?? DateTime.Now,
                Type = article.Type
            };
        }
    }
}
