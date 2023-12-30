using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeStoreWeb.Models
{
	[Table("categories", Schema = "production")]
	public class Category
	{
		[Key]
		[DisplayName("Category Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Range(1, 100, ErrorMessage = "Category Id must be between 1-100")]
        public int category_id { get; set; }
		[Required]
		[MaxLength(30)]
		[DisplayName("Category Name")]
		public string? category_name { get; set; }
	}
}
