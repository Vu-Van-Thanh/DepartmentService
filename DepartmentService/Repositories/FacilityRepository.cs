using Microsoft.EntityFrameworkCore;
using DepartmentService.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DepartmentService.API.AppDbContext;

namespace DepartmentService.API.Repositories
{

    public interface IFacilityRepository : IRepository<Facility>
    {
        Task<IEnumerable<Facility>> GetFacilitiesByDepartmentAsync(string departmentId);
    }

    public class FacilityRepository : Repository<Facility>, IFacilityRepository
    {
        public FacilityRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Facility>> GetFacilitiesByDepartmentAsync(string departmentId)
        {
            return await _dbSet.Where(f => f.DepartmentId == departmentId).ToListAsync();
        }
    }
}
