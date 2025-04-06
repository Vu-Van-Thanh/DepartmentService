using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DepartmentService.API.Enums;

namespace DepartmentService.API.Entities
{
    public class JobPosition
    {
        [Key]
        [StringLength(5)]
        public string JobPositionId { get; set; } 
        [Required, StringLength(100)]
        public string PositionName { get; set; }

        public JobLevel Level { get; set; }
        public string Description { get; set; }
        [StringLength(10)]
        public string DepartmentId { get; set; }
        public string? Manager { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        

    }
}
