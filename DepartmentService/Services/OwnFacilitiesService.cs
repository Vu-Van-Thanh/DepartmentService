using DepartmentService.API.DTO;
using DepartmentService.API.Entities;
using DepartmentService.API.Repositories;
using System.Runtime.InteropServices;

namespace DepartmentService.API.Services
{
    public interface IOwnFacilitiesService
    {
        Task<IEnumerable<OwnFacilitiesDTO>> GetAllOwnFacilities();
        Task<OwnFacilitiesDTO> GetOwnFacilitiesById(string FacilityId);
        Task<OwnFacilitiesDTO> UpsertOwner(OwnFacilitiesUpsertRequest ownFacilities);
        Task<bool> DeleteOwnFacilities(string jobId);
    }
    public class OwnFacilitiesService : IOwnFacilitiesService
    {
        private readonly IOwnFacilitiesRepository _ownFacilitiesRepository;

        public OwnFacilitiesService(IOwnFacilitiesRepository ownFacilitiesRepository)
        {
            _ownFacilitiesRepository = ownFacilitiesRepository;
        }

        public async Task<bool> DeleteOwnFacilities(string FacilityId)
        {
            return (await _ownFacilitiesRepository.DeleteAsync(d => d.FacilityId == FacilityId));
        }

        public  async Task<IEnumerable<OwnFacilitiesDTO>> GetAllOwnFacilities()
        {
            List<OwnFacilities> de = (await _ownFacilitiesRepository.GetAllAsync()).ToList();
            return de.Select(d => d.ToDTO()).ToList();
        }

        public async Task<OwnFacilitiesDTO> GetOwnFacilitiesById(string jobId)
        {
            return (await _ownFacilitiesRepository.GetByIdAsync(jobId)).ToDTO();
        }

        public async Task<OwnFacilitiesDTO> UpsertOwner(OwnFacilitiesUpsertRequest ownFacilities)
        {
            return (await _ownFacilitiesRepository.UpsertAsync(ownFacilities.ToOF(), d => d.FacilityId == ownFacilities.FacilityId)).ToDTO();
        }
    }
}
