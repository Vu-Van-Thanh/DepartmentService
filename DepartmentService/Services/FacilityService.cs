using DepartmentService.API.DTO;
using DepartmentService.API.Entities;
using DepartmentService.API.Repositories;

namespace DepartmentService.API.Services
{
    public interface IFacilityService
    {
        Task<IEnumerable<FacilityInfo>> GetAllFacilities();
        Task<FacilityInfo> GetFacilityById(string facId);
        Task<FacilityInfo> UpsertFacility(FacilityUpsertRequest facility);
        Task<bool> DeleteFacility(string facId);
    }
    public class FacilityService : IFacilityService
    {
        private readonly IFacilityRepository _facilityRepository;

        public FacilityService(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        public async Task<bool> DeleteFacility(string facId)
        {
            return (await _facilityRepository.DeleteAsync(d => d.FacId== facId));
        }

        public async Task<IEnumerable<FacilityInfo>> GetAllFacilities()
        {
            List<Facility> de = (await _facilityRepository.GetAllAsync()).ToList();
            return de.Select(d => d.ToFacilityDTO()).ToList();
        }

        public async Task<FacilityInfo> GetFacilityById(string facId)
        {
            return (await _facilityRepository.GetByIdAsync(facId)).ToFacilityDTO();
        }

        public async Task<FacilityInfo> UpsertFacility(FacilityUpsertRequest facility)
        {
            return (await _facilityRepository.UpsertAsync(facility.ToFacility(), f => f.FacId == facility.FacId)).ToFacilityDTO();
        }
    }
}
