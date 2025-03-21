using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentService.API.AppDbContext;
using DepartmentService.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DepartmentService.API.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department?> GetDepartmentWithFacilitiesAsync(string departmentId);
    }

    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Department?> GetDepartmentWithFacilitiesAsync(string departmentId)
        {
            return await _dbSet.Include(d => d.Facilities)
                               .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

        }
    }
}
