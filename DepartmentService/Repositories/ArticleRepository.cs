using DepartmentService.API.AppDbContext;
using DepartmentService.API.Entities;
using DepartmentService.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DepartmentService.Repositories
{

    public interface IArticleRepository : IRepository<Article>
    {
        Task<Article> GetArticleByType(string articleType);
        Task<IEnumerable<Article>> GetByFilter(Expression<Func<Article,bool>> expression);
    }
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implement any specific methods for Article repository here
        public async Task<Article> GetArticleByType(string articleType)
        {
            return await _context.Articles.FirstOrDefaultAsync(a => a.Type == articleType);
        }

        public async Task<IEnumerable<Article>> GetByFilter(Expression<Func<Article, bool>> expression)
        {
            return await _context.Articles
                             .Where(expression)
                             .ToListAsync();
        }
    }
}
