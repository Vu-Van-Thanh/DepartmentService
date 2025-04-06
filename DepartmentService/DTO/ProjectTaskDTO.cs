using System;

namespace DepartmentService.API.DTO
{
    public class ProjectTaskInfo
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Guid? AssignedTo { get; set; }
        public string AssignedToName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class ProjectTaskUpsertRequest
    {
        public Guid? TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? AssignedTo { get; set; }
    }
} 