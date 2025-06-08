using System.ComponentModel.DataAnnotations;

namespace DepartmentService.Entities
{
    public class AppliedCv
    {
        [Key]
        public Guid CVID { get; set; } = Guid.NewGuid();
        public string FromMail { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; } 
    }
}
