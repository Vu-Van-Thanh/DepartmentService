using DepartmentService.API.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DepartmentService.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<T> UpsertAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(predicate);

            if (existingEntity == null)
            {
                await _dbSet.AddAsync(entity); 
            }
            else
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity); 
                entity = existingEntity; 
            }

            await _context.SaveChangesAsync();
            return entity; 
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await _dbSet.Where(predicate).ToListAsync();
            if (!entities.Any()) return false; 

            _dbSet.RemoveRange(entities); 
            await _context.SaveChangesAsync(); 
            return tru
        }
    }
}
