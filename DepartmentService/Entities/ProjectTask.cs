using Microsoft.Identity.Client;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentService.API.Entities
{
    public class ProjectTask
    {
        [Key]
        public Guid TaskId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        public DateTime? CompletedAt { get; set; }

        [Required]
        public string Status { get; set; } // To Do, In Progress, Review, Completed

        [Required]
        public string Priority { get; set; } // Low, Medium, High, Urgent

        [Required]
        public Guid ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }

        public Guid? AssignedTo { get; set; } // Employee ID

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public string? Attachments { get; set; } 
    }
} 