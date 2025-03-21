using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.API.Entities
{
    public class Department
    {
        [Key]
        [StringLength(2)]
        public string DepartmentId { get; set; } 

        [Required, StringLength(100)]
        public string DepartmentName { get; set; } = string.Empty;
        public string? Manager { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
        public string Contact { get; set; }


        // Navigation Property
        // Navigation Property - Một phòng ban có nhiều vị trí công việc
        public virtual ICollection<JobPosition>? JobPositions { get; set; } = new List<JobPosition>();

        public virtual ICollection<Facility>? Facilities { get; set; } = new List<Facility>();

    }
}
