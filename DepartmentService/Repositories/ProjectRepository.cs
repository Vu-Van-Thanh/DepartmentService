using DepartmentService.API.AppDbContext;
using DepartmentService.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DepartmentService.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(Guid id);
        Task<Project> UpsertAsync(Project project, Func<Project, bool> predicate);
        Task<bool> DeleteAsync(Func<Project, bool> predicate);
        Task<IEnumerable<Project>> GetByDepartmentIdAsync(string departmentId);
    }

    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Department)
                .Include(p => p.Tasks)
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(Guid id)
        {
            return await _context.Projects
                .Include(p => p.Department)
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<Project> UpsertAsync(Project project, Func<Project, bool> predicate)
        {
            var existingProject = await _context.Projects.FirstOrDefaultAsync(p => predicate(p));
            if (existingProject != null)
            {
                _context.Entry(existingProject).CurrentValues.SetValues(project);
            }
            else
            {
                await _context.Projects.AddAsync(project);
            }
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteAsync(Func<Project, bool> predicate)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => predicate(p));
            if (project == null) return false;
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Project>> GetByDepartmentIdAsync(string departmentId)
        {
            return await _context.Projects
                .Include(p => p.Department)
                .Include(p => p.Tasks)
                .Where(p => p.DepartmentId == departmentId)
                .ToListAsync();
        }
    }
} 