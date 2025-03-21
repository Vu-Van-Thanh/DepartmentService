using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace DepartmentService.API.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> UpsertAsync(T entity , Expression<Func<T, bool>> predicate);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate); 
    }
}