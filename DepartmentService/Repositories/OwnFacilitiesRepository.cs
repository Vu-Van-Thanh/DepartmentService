using Microsoft.EntityFrameworkCore;
using DepartmentService.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DepartmentService.API.AppDbContext;
using System.Linq.Expressions;

namespace DepartmentService.API.Repositories
{

    public interface IOwnFacilitiesRepository : IRepository<OwnFacilities>
    {
        Task<IEnumerable<OwnFacilities>> GetFacilitiesByOwnerAsync(Guid ownerId);
    }

    public class OwnFacilitiesRepository : Repository<OwnFacilities>, IOwnFacilitiesRepository
    {
        public OwnFacilitiesRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<OwnFacilities>> GetFacilitiesByOwnerAsync(Guid ownerId)
        {
            return await _dbSet.Where(of => of.OwnerId == ownerId).ToListAsync();
        }
        
    }
}
