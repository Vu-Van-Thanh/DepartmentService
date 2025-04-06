using DepartmentService.API.AppDbContext;
using DepartmentService.API.Entities;
using DepartmentService.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DepartmentService.Repositories
{
    public interface IMediaRepository : IRepository<Media>
    {
        Task<IEnumerable<Media>> GetMediaByArticleIdAsync(Guid articleId);
    }

    public class MediaRepository : Repository<Media>, IMediaRepository
    {
        public MediaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Media>> GetMediaByArticleIdAsync(Guid articleId)
        {
            return await _dbSet.Where(m => m.ArticleID == articleId).ToListAsync();
        }
    }
}
