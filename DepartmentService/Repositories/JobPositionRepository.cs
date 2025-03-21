using Microsoft.EntityFrameworkCore;
using DepartmentService.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using DepartmentService.API.AppDbContext;
using System.Linq.Expressions;
using DepartmentService.API.Enums;

namespace DepartmentService.API.Repositories
{
    public interface IJobPositionRepository : IRepository<JobPosition>
    {
        Task<IEnumerable<JobPosition>> GetPositionsByDepartmentAsync(string departmentId);
    }

    public class JobPositionRepository : Repository<JobPosition>, IJobPositionRepository
    {
        public JobPositionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<JobPosition>> GetPositionsByDepartmentAsync(string departmentId)
        {
            return await _dbSet.Where(jp => jp.DepartmentId == departmentId).ToListAsync();
        }

        public override async Task<JobPosition> UpsertAsync(JobPosition entity, Expression<Func<JobPosition, bool>> predicate)
        {
            var existingEntity = await _dbSet.FirstOrDefaultAsync(predicate);

            if (existingEntity == null)
            {
                // Nếu chưa có, thêm 5 dòng theo từng JobLevel
                var jobPositions = Enum.GetValues(typeof(JobLevel))
                                       .Cast<JobLevel>()
                                       .Select(level => new JobPosition
                                       {
                                           JobPositionId = entity.JobPositionId,  // Tạo ID mới
                                           PositionName = entity.PositionName,
                                           DepartmentId = entity.DepartmentId,
                                           Level = level,
                                           Manager = entity.Manager,
                                           Description = entity.Description
                                       }).ToList();

                await _dbSet.AddRangeAsync(jobPositions);
                await _context.SaveChangesAsync();

                return jobPositions.First(); // Trả về một job position bất kỳ trong danh sách
            }
            else
            {
                // ✅ Cập nhật thủ công thay vì dùng SetValues
                existingEntity.PositionName = entity.PositionName;
                existingEntity.DepartmentId = entity.DepartmentId;
                existingEntity.Level = entity.Level;
                existingEntity.Manager = entity.Manager;
                existingEntity.Description = entity.Description;

                _dbSet.Update(existingEntity); // Cập nhật entity vào DbContext
            }

            await _context.SaveChangesAsync();
            return existingEntity;
        }

    }
}
