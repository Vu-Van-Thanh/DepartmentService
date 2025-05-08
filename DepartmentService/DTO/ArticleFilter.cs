using DepartmentService.API.Entities;
using System.Linq.Expressions;

namespace DepartmentService.DTO
{
    public class ArticleFilter
    {
        public string? DepartmentId { get; set; }
        public string? Author { get; set; }

        public DateTime? CreatedDate { get; set; }


        public Expression<Func<Article, bool>>? ToExpression()
        {
            return article => 
                (string.IsNullOrEmpty(DepartmentId) || article.DepartmentId == DepartmentId) &&
                (!CreatedDate.HasValue || article.DateCreated.Date == CreatedDate.Value.Date);
        }
    }
}
