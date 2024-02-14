using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace SoftUniBazar.Data
{
    public class Category
    {
        [Key]
        [Comment("Category identifier")]
        public int Id { get; set; }
        [Required]
        [MaxLength(DataConstants.CategoryMaxLenght)]
        [Comment("Category name")]
        public string Name { get; set; } = string.Empty;
        public IList<Ad> Ads { get; set; }= new List<Ad>();
    }
}
