using System;
using System.Collections.Generic;

namespace DepartmentService.API.DTO
{
    public class ProjectInfo
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<ProjectTaskInfo> Tasks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ProjectUpsertRequest
    {
        public Guid? ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public string DepartmentId { get; set; }
    }
} 