using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Data
{
    public class Ad
    {
        [Key]
        [Comment("Ad identifier")]
        public int Id { get; set; }
        [Required]
        [MaxLength(DataConstants.AdNameMaxLenght)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(DataConstants.AdDescriptionMaxLenght)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; }
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public int CategoryType { get; set; }
        public Category Gategory { get; set; }

    }
}
//⦁	Has Id – a unique integer, Primary Key
//⦁	Has Name – a string with min length 5 and max length 25 (required)
//⦁	Has Description – a string with min length 15 and max length 250 (required)
//⦁	Has Price – a decimal (required)
//⦁	Has OwnerId – a string (required)
//⦁	Has Owner – an IdentityUser (required)
//⦁	Has ImageUrl – a string (required)
//⦁	Has CreatedOn – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//⦁	Has CategoryId – an integer, foreign key (required)
//⦁	Has Category – a Category (required)
