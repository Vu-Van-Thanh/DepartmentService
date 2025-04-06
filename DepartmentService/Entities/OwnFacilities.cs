using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentService.API.Entities
{
    public class OwnFacilities
    {
        
        public Guid? OwnerId { get; set; }
        
        [StringLength(6)]
        public string FacilityId { get; set; }

        [ForeignKey("FacilityId")]
        public virtual Facility? Facility { get; set; }
    }
}
