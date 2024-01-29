using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Models
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Field {0} is requared")]
		[Display(Name = "Product Name")]
		[StringLength(100,MinimumLength=3, ErrorMessage ="Product {0} must be between {2} and {1}!")]
		public string Name { get; set; } = String.Empty;

	}
}
