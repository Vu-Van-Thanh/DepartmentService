using DepartmentService.API.AppDbContext;
using DepartmentService.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DepartmentService.Repositories
{
    public interface IProjectTaskRepository
    {
        Task<IEnumerable<ProjectTask>> GetAllAsync();
        Task<ProjectTask> GetByIdAsync(Guid id);
        Task<ProjectTask> UpsertAsync(ProjectTask task, Expression<Func<ProjectTask, bool>> predicate);
        Task<bool> DeleteAsync(Func<ProjectTask, bool> predicate);
        Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<ProjectTask>> GetByAssignedToAsync(Guid assignedTo);
        Task<List<ProjectTask>> GetTasksByAssignedUserIdsAsync(List<Guid> userIds);
    }

    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectTask>> GetAllAsync()
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .ToListAsync();
        }

        public async Task<ProjectTask> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.TaskId == id);
        }

        public async Task<ProjectTask> UpsertAsync(ProjectTask task, Expression<Func<ProjectTask, bool>> predicate)
        {
            var existingTask = await _context.Tasks.FirstOrDefaultAsync(predicate);
            if (existingTask != null)
            {
                _context.Entry(existingTask).CurrentValues.SetValues(task);
            }
            else
            {
                await _context.Tasks.AddAsync(task);
            }
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(Func<ProjectTask, bool> predicate)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => predicate(t));
            if (task == null) return false;
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProjectTask>> GetByAssignedToAsync(Guid assignedTo)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Where(t => t.AssignedTo == assignedTo)
                .ToListAsync();
        }
        public async Task<List<ProjectTask>> GetTasksByAssignedUserIdsAsync(List<Guid> userIds)
        {
            return await _context.Tasks
                .Where(t => t.AssignedTo.HasValue && userIds.Contains(t.AssignedTo.Value))
                .ToListAsync();
        }


    }
} 