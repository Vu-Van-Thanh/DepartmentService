using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DepartmentService.API.DTO;
using DepartmentService.API.Entities;
using DepartmentService.DTO;
using DepartmentService.Repositories;

namespace DepartmentService.Services
{
    public interface IProjectTaskService
    {
        Task<IEnumerable<ProjectTaskInfo>> GetAllTasks();
        Task<ProjectTaskInfo> GetTaskById(Guid id);
        Task<ProjectTaskInfo> UpsertTask(ProjectTaskUpsertRequest task);
        Task<bool> DeleteTask(Guid id);
        Task<IEnumerable<ProjectTaskInfo>> GetTasksByProject(Guid projectId);
        Task<IEnumerable<ProjectTaskInfo>> GetTasksByAssignedTo(Guid assignedTo);
        Task<List<DepartmentPerformance>> GetTaskPerformance(EmployeeInDepartment emp);
    }

    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;

        public ProjectTaskService(IProjectTaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectTaskInfo>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(t => ToTaskInfo(t));
        }

        public async Task<ProjectTaskInfo> GetTaskById(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return task != null ? ToTaskInfo(task) : null;
        }

        public async Task<ProjectTaskInfo> UpsertTask(ProjectTaskUpsertRequest request)
        {
            var task = new ProjectTask
            {
                TaskId = request.TaskId ?? Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                StartDate = request.StartDate,
                Deadline = request.Deadline,
                Status = request.Status,
                Priority = request.Priority,
                ProjectId = request.ProjectId,
                AssignedTo = request.AssignedTo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _taskRepository.UpsertAsync(task, t => t.TaskId == task.TaskId);
            return ToTaskInfo(result);
        }

        public async Task<bool> DeleteTask(Guid id)
        {
            return await _taskRepository.DeleteAsync(t => t.TaskId == id);
        }

        public async Task<IEnumerable<ProjectTaskInfo>> GetTasksByProject(Guid projectId)
        {
            var tasks = await _taskRepository.GetByProjectIdAsync(projectId);
            return tasks.Select(t => ToTaskInfo(t));
        }

        public async Task<IEnumerable<ProjectTaskInfo>> GetTasksByAssignedTo(Guid assignedTo)
        {
            var tasks = await _taskRepository.GetByAssignedToAsync(assignedTo);
            return tasks.Select(t => ToTaskInfo(t));
        }

        private ProjectTaskInfo ToTaskInfo(ProjectTask task)
        {
            return new ProjectTaskInfo
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                StartDate = task.StartDate,
                Deadline = task.Deadline,
                Status = task.Status,
                Priority = task.Priority,
                ProjectId = task.ProjectId,
                ProjectName = task.Project?.Name,
                AssignedTo = task.AssignedTo,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                CreatedBy = task.CreatedBy,
                UpdatedBy = task.UpdatedBy
            };
        }

        public async Task<List<DepartmentPerformance>> GetTaskPerformance(EmployeeInDepartment emp)
        {
            List<DepartmentPerformance> result = new List<DepartmentPerformance>();
            for(int i = 0; i < emp.DepartmentID.Count; i++)
            {
                string employeeID = emp.employeeIDList[i];
                List<Guid> guids = employeeID.Split(',').Select(Guid.Parse).ToList();
                List<ProjectTask> tasks = (await _taskRepository.GetTasksByAssignedUserIdsAsync(guids)).ToList();
                var performanceScore = new double[4];
                for (int quarter = 1; quarter <= 4; quarter++)
                {
                    var tasksInQuarter = tasks.Where(task =>
                    {
                        int taskQuarter = (task.StartDate.Month - 1) / 3 + 1;
                        return taskQuarter == quarter;
                    }).ToList();

                    int totalTasks = tasksInQuarter.Count;
                    int completedOnTime = tasksInQuarter.Count(t =>
                        t.Status == "Completed" &&
                        t.CompletedAt.HasValue &&
                        t.CompletedAt.Value <= t.Deadline);

                    double score = totalTasks == 0 ? 0 : (double)completedOnTime / totalTasks * 100;
                    performanceScore[quarter - 1] = Math.Round(score, 2); 
                }

                result.Add(new DepartmentPerformance
                {
                    departmentName = emp.DepartmentID[i],
                    performanceScore = performanceScore.ToList()
                });
            }
            return result;
        }
    }
} 