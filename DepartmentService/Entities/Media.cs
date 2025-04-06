using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.API.Entities
{
	public class Media
	{
		[Key]
		public Guid MediaID { get; set; }

		public Guid ArticleID { get; set; }

		[StringLength(20)]
		public string? MediaType { get; set; }

		
		public string? MediaUrl { get; set; }
		public string? MediaContent { get; set; }

		[ForeignKey("ArticleID")]
		public virtual Article? Article { get; set; }
	}
}
