using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentService.API.Entities
{
    public class Project
    {
        [Key]
        public Guid ProjectId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // điều chỉnh
        public string? Visibility { get; set; }
        public string? Members { get; set; }
        public string? ProjectAttachment { get; set; }
        public string? ProjectManager { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Status { get; set; } // Planning, In Progress, On Hold, Completed, Cancelled

        [Required]
        [StringLength(10)]
        public string DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public virtual ICollection<ProjectTask> Tasks { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }  

        public string? UpdatedBy { get; set; }
    }
} 