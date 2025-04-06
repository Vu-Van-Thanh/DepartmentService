using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DepartmentService.API.Enums;

namespace DepartmentService.API.Entities
{
    public class Facility
    {
        [Key]

        [StringLength(6)]
        public string FacId { get; set; }
        [StringLength(10)]
        public string DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public int UsedQuantity { get; set; }

        public FacilityCondition Condition { get; set; } = FacilityCondition.New;

        public DateTime? PurchaseDate { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }

        // Navigation Property

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }
    }
}
