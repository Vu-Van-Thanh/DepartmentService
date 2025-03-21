using DepartmentService.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.API.DTO
{
    public class JobPositionUpsertRequest
    {
        public string? JobPositionId { get; set; }
        public string PositionName { get; set; }

        public JobLevel? Level { get; set; }
        public string Description { get; set; }
        public string DepartmentId { get; set; }
        public string? Manager { get; set; }
    }

    public class JobPositionInfo
    {
        public string PositionName { get; set; }

        public string? Level { get; set; }
        public string Description { get; set; }
        public string DepartmentId { get; set; }
        public string? Manager { get; set; }
    }
    public static class JobPositionExtension
    {
        public static JobPositionInfo ToJobPositionInfo(this Entities.JobPosition jobPosition)
        {
            return new JobPositionInfo
            {
                PositionName = jobPosition.PositionName,
                Level = jobPosition.Level.ToString(),
                Description = jobPosition.Description,
                DepartmentId = jobPosition.DepartmentId,
                Manager = jobPosition.Manager
            };
        }

        public static Entities.JobPosition ToJobPosition(this JobPositionUpsertRequest jobPosition)
        {
            return new Entities.JobPosition
            {
                JobPositionId = jobPosition.JobPositionId,
                PositionName = jobPosition.PositionName,
                Level = jobPosition.Level.Value,
                Description = jobPosition.Description,
                DepartmentId = jobPosition.DepartmentId,
                Manager = jobPosition.Manager
            };
        }
    }
}
