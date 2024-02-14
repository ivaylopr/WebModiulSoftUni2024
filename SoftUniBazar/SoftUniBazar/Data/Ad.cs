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
        [Comment("Name of Ad")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(DataConstants.AdDescriptionMaxLenght)]
        [Comment("Description of the Ad")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Comment("Price of the Ad")]
        public decimal Price { get; set; } = default(decimal);
        [Required]
        [Comment("User who is owner and authoticated for the Ad")]
        public string OwnerId { get; set; } = string.Empty;
        [Required]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;
        [Required]
        [Comment("Image of the Ad")]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        [Comment("Date and time of creating the Ad")]
        public DateTime CreatedOn { get; set; }
        [Required]
        [Comment("Category of the Ad")]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Gategory { get; set; } = null!;

        public IList<AdBuyer> AdsBuyer = new List<AdBuyer>();
    }
}
