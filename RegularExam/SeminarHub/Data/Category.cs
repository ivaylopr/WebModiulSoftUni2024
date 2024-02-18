using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Data
{
    [Comment("Category class for seminars")]
    public class Category
    {
        [Comment("Identifier")]
        public int Id { get; set; }
        [Required]
        [MaxLength(DataConstants.CategoryNameMaxLenght)]
        [Comment("Category name")]
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Seminar> Seminars { get; set; }=new List<Seminar>();
    }
}
