using DepartmentService.API.AppDbContext;
using DepartmentService.API.Entities;
using DepartmentService.API.Repositories;
using DepartmentService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DepartmentService.Repositories
{

    public interface ICVRepository : IRepository<AppliedCv>
    {
        Task AddRange(IEnumerable<AppliedCv> cvs);
      

    }
    public class CVRepository : Repository<AppliedCv>, ICVRepository
    {
        private readonly ApplicationDbContext _context;

        public CVRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
       

        public async Task AddRange(IEnumerable<AppliedCv> cvs)
        {
            _context.AppliedCvs.AddRange(cvs);
            await _context.SaveChangesAsync();
        }

        

    }
}
