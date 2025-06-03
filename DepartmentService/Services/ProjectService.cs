using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentService.API.DTO;
using DepartmentService.API.Entities;
using DepartmentService.API.Repositories;
using DepartmentService.DTO;
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
        Task<IEnumerable<ProjectInfo>> GetFilteredProjects(ProjectFilterDTO projectFilter);
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
            var attachmentFileNames = new List<string>();
            if (request.ProjectAttachment != null && request.ProjectAttachment.Length > 0)
            {
                var sanitizedProjectName = SanitizeFileName(request.Name);
                var projectFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProjectAttachments", sanitizedProjectName);

                if (!Directory.Exists(projectFolderPath))
                    Directory.CreateDirectory(projectFolderPath);

                foreach (var file in request.ProjectAttachment)
                {
                    if (file != null && file.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                        var filePath = Path.Combine(projectFolderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Lưu URL tương đối
                        attachmentFileNames.Add($"/ProjectAttachments/{sanitizedProjectName}/{fileName}");
                    }
                }
            }
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
                UpdatedAt = DateTime.UtcNow,
                ProjectManager = request.ProjectManager,
                ProjectAttachment = string.Join("|",attachmentFileNames),
                Members = request.Members,
                Visibility = request.Visibility
            };

            var result = await _projectRepository.UpsertAsync(project, p => p.ProjectId == project.ProjectId);
            return ToProjectInfo(result);
        }
        private string SanitizeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name.Trim();
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

        public async Task<IEnumerable<ProjectInfo>> GetFilteredProjects(ProjectFilterDTO projectFilter)
        {
            var projects = await _projectRepository.GetByFilter(projectFilter.ToExpression());
            return projects.Select(p => ToProjectInfo(p)).ToList();
        }
    }
} 