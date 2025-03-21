using DepartmentService.API.Entities;
using DepartmentService.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.API.DTO
{
    public class FacilityUpsertRequest
    {
        public string? FacId { get; set; }
        public string DepartmentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public int UsedQuantity { get; set; }
        public FacilityCondition Condition { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
    }

    public class FacilityInfo
    {
        
        public string DepartmentId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public int UsedQuantity { get; set; }
        public FacilityCondition Condition { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? LastMaintenanceDate { get; set; }
    }

    public static class FacilityExtension
    {   public static FacilityInfo ToFacilityDTO(this Facility facility)
        {
            return new FacilityInfo
            {
                DepartmentId = facility.DepartmentId,
                Name = facility.Name,
                Description = facility.Description,
                Quantity = facility.Quantity,
                UsedQuantity = facility.UsedQuantity,
                Condition = facility.Condition,
                PurchaseDate = facility.PurchaseDate,
                LastMaintenanceDate = facility.LastMaintenanceDate
            };
        }
        public static Facility ToFacility(this FacilityUpsertRequest facility)
        {
            return new Facility
            {
                FacId = facility.FacId,
                DepartmentId = facility.DepartmentId,
                Name = facility.Name,
                Description = facility.Description,
                Quantity = facility.Quantity,
                UsedQuantity = facility.UsedQuantity,
                Condition = facility.Condition,
                PurchaseDate = facility.PurchaseDate,
                LastMaintenanceDate = facility.LastMaintenanceDate
            };
        }
    }
}
