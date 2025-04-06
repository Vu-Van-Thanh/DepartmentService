
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DepartmentService.API.Entities
{
	public class Article
	{
		[Key]
		public Guid ArticleId { get; set; }
		public string? DepartmentId { get; set; }

		[StringLength(100)]
		public string? Title { get; set; }
		
		public string? Content { get; set; }

		public DateTime DateCreated { get; set; }
		public string Type { get; set; } = "Normal";

		public virtual ICollection<Media>? medias { get; set; }
		[ForeignKey("DepartmentId")]
		public virtual Department? Department { get; set; }
	}
}
