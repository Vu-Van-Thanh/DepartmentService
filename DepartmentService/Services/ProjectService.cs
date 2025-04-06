using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentService.API.DTO;
using DepartmentService.API.Entities;
using DepartmentService.API.Repositories;
using DepartmentService.Repositories;

namespace DepartmentService.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectInfo>> GetAllProjects();
        Task<ProjectInfo> GetProjectById(Guid id);
        Task<ProjectInfo> UpsertProject(ProjectUpsertRequest project);
        Task<bool> DeleteProject(Guid id);
        Task<IEnumerable<ProjectInfo>> GetProjectsByDepartment(string departmentId);
    }

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public ProjectService(IProjectRepository projectRepository, IDepartmentRepository departmentRepository)
        {
            _projectRepository = projectRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<ProjectInfo>> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(p => ToProjectInfo(p));
        }

        public async Task<ProjectInfo> GetProjectById(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return project != null ? ToProjectInfo(project) : null;
        }

        public async Task<ProjectInfo> UpsertProject(ProjectUpsertRequest request)
        {
            var project = new Project
            {
                ProjectId = request.ProjectId ?? Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status,
                DepartmentId = request.DepartmentId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _projectRepository.UpsertAsync(project, p => p.ProjectId == project.ProjectId);
            return ToProjectInfo(result);
        }

        public async Task<bool> DeleteProject(Guid id)
        {
            return await _projectRepository.DeleteAsync(p => p.ProjectId == id);
        }

        public async Task<IEnumerable<ProjectInfo>> GetProjectsByDepartment(string departmentId)
        {
            var projects = await _projectRepository.GetByDepartmentIdAsync(departmentId);
            return projects.Select(p => ToProjectInfo(p));
        }

        private ProjectInfo ToProjectInfo(Project project)
        {
            return new ProjectInfo
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                DepartmentId = project.DepartmentId,
                DepartmentName = project.Department?.DepartmentName,
                Tasks = project.Tasks?.Select(t => new ProjectTaskInfo
                {
                    TaskId = t.TaskId,
                    Title = t.Title,
                    Description = t.Description,
                    StartDate = t.StartDate,
                    Deadline = t.Deadline,
                    Status = t.Status,
                    Priority = t.Priority,
                    ProjectId = t.ProjectId,
                    ProjectName = project.Name,
                    AssignedTo = t.AssignedTo
                }).ToList(),
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt,
                CreatedBy = project.CreatedBy,
                UpdatedBy = project.UpdatedBy
            };
        }
    }
} 