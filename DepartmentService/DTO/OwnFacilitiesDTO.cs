using DepartmentService.API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.API.DTO
{
    public class OwnFacilitiesUpsertRequest
    {
        public Guid? OwnerId { get; set; }
        public string FacilityId { get; set; }

    }

    public class OwnFacilitiesDTO
    {
        public Guid? OwnerId { get; set; }
        public string FacilityId { get; set; }

    }

    public static class OwnFacilitiesExtensions
    {
        public static OwnFacilitiesDTO ToDTO(this OwnFacilities ownFacilities)
        {
            return new OwnFacilitiesDTO
            {
                OwnerId = ownFacilities.OwnerId,
                FacilityId = ownFacilities.FacilityId
            };
        }

        public static OwnFacilities ToOF(this OwnFacilitiesUpsertRequest ownFacilities)
        {
            return new OwnFacilities
            {
                OwnerId = ownFacilities.OwnerId,
                FacilityId = ownFacilities.FacilityId
            };
        }
    }
}
