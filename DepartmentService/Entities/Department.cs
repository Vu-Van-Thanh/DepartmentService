using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DepartmentService.API.Entities
{
    public class Department
    {
        [Key]
        [StringLength(10)]
        public string DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        public string Manager { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public string Location { get; set; } 
        public string Contact { get; set; }

        public virtual ICollection<JobPosition> JobPositions { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

    }
}
