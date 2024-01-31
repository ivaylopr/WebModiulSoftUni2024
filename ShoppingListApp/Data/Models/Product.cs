using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Data.Models
{
    [Comment("Shopping list of products")]
	public class Product
	{
        [Key]
        [Comment("Primary Key")]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }=String.Empty;
        public virtual List<ProductNote> ProductNotes { get; set; } = new List<ProductNote>();
    }
}
