using DepartmentService.API.DTO;
using DepartmentService.API.Entities;
using DepartmentService.API.Repositories;


namespace DepartmentService.API.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentInfo>> GetAllDepartments();
        Task<DepartmentInfo> GetDepartmentById(string departmentId);
        Task<DepartmentInfo> UpsertDepartment(DepartmentUpsertRequest department);
        Task<bool> DeleteDepartment(string departmentId);

    }
    public class DepartmentServices : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentServices(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> DeleteDepartment(string departmentId)
        {
            return (await _departmentRepository.DeleteAsync(d => d.DepartmentId == departmentId));
        }

        public async Task<IEnumerable<DepartmentInfo>> GetAllDepartments()
        {
            List<Department> de = (await _departmentRepository.GetAllAsync()).ToList();
            return de.Select(d => d.ToDepartmentInfo()).ToList();
        }

        public async Task<DepartmentInfo> GetDepartmentById(string departmentId)
        {
            return (await _departmentRepository.GetByIdAsync(Guid.Parse(departmentId))).ToDepartmentInfo();
        }

        public async Task<DepartmentInfo> UpsertDepartment(DepartmentUpsertRequest department)
        {
            return (await _departmentRepository.UpsertAsync(department.ToDepartment(), d => d.DepartmentId == department.DepartmentId)).ToDepartmentInfo();
        }
    }
}
