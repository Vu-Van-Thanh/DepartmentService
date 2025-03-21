using DepartmentService.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace DepartmentService.API.DTO
{
    public class DepartmentUpsertRequest
    {
        public string? DepartmentId { get; set; }

        public string? DepartmentName { get; set; } 
        public string? Manager { get; set; }

        public string? Description { get; set; }

        public string? Location { get; set; }
        public string? Contact { get; set; }

    }
    public class DepartmentInfo
    {       
       
        public string DepartmentName { get; set; } 
        public string Manager { get; set; } 
        public string Description { get; set; } 
        public string Location { get; set; }
        public string Contact { get; set; } 
    }

    public static class DepartmentDTOExtensions
    {
        public static DepartmentInfo ToDepartmentInfo(this Department department)
        {
            return new DepartmentInfo
            {
                DepartmentName = department.DepartmentName,
                Manager = department.Manager,
                Description = department.Description,
                Location = department.Location,
                Contact = department.Contact
            };
        }

        public static Department ToDepartment(this DepartmentUpsertRequest department)
        {
            return new Department
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                Manager = department.Manager,
                Description = department.Description,
                Location = department.Location,
                Contact = department.Contact
            };
        }
    }
}
