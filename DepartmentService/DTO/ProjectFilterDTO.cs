using DepartmentService.API.Entities;
using System.Linq.Expressions;

namespace DepartmentService.DTO
{
    public class ProjectFilterDTO
    {
        public string? DeparmentID { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Status { get; set; }

        public Expression<Func<Project, bool>>? ToExpression()
        {
            return project =>
                (string.IsNullOrEmpty(DeparmentID) || project.DepartmentId == DeparmentID) &&
                (!StartTime.HasValue || project.StartDate >= StartTime.Value) &&
                (!EndTime.HasValue || project.EndDate<= EndTime.Value) &&
                (string.IsNullOrEmpty(Status) || project.Status == Status);
        }
    }
}
