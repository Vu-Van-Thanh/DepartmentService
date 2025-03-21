using DepartmentService.API.DTO;
using DepartmentService.API.Entities;
using DepartmentService.API.Repositories;

namespace DepartmentService.API.Services
{
    public interface IJobPositionService
    {
        Task<IEnumerable<JobPositionInfo>> GetAllJob();
        Task<JobPositionInfo> GetJobById(string jobId);
        Task<JobPositionInfo> UpsertJob(JobPositionUpsertRequest facility);
        Task<bool> DeleteJob(string jobId);
    }
    public class JobPositionService : IJobPositionService
    {
        private readonly IJobPositionRepository _jobPositionRepository;

        public JobPositionService(IJobPositionRepository jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }

        public async Task<bool> DeleteJob(string jobId)
        {
            return (await _jobPositionRepository.DeleteAsync(j => j.JobPositionId == jobId));
        }

        public async Task<IEnumerable<JobPositionInfo>> GetAllJob()
        {
            List<JobPosition> de = (await _jobPositionRepository.GetAllAsync()).ToList();
            return de.Select(d => d.ToJobPositionInfo()).ToList();
        }

        public async Task<JobPositionInfo> GetJobById(string jobId)
        {
            return (await _jobPositionRepository.GetByIdAsync(jobId)).ToJobPositionInfo();
        }

        public async Task<JobPositionInfo> UpsertJob(JobPositionUpsertRequest job)
        {
            return (await _jobPositionRepository.UpsertAsync(job.ToJobPosition(), d => d.JobPositionId == job.JobPositionId)).ToJobPositionInfo();
        }
    }
}
