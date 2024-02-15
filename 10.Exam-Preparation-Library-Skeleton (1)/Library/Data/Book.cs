using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data
{
    public class Book
    {
        [Key]
        [Comment("Identifier")]
        public int Id { get; set; }
        [Required]
        [MaxLength(DataConstants.BookTitleMaxLenght)]
        [Comment("Title of the book")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(DataConstants.AuthorMaxLenght)]
        [Comment("Book Author")]
        public string Author { get; set; } = string.Empty;
        [Required]
        [MaxLength(DataConstants.DescriptionMaxLenght)]
        [Comment("Description of the book")]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        [Comment("Book rating")]
        public decimal Rating { get; set; }
        [Required]
        [Comment("Book category")]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public IList<UserBook> UsersBooks { get; set; } = new List<UserBook>();
    }
}
//Has Id – a unique integer, Primary Key
//• Has Title – a string with min length 10 and max length 50 (required)
//• Has Author – a string with min length 5 and max length 50 (required)
//• Has Description – a string with min length 5 and max length 5000 (required)
//• Has ImageUrl – a string (required)
//• Has Rating – a decimal with min value 0.00 and max value 10.00 (required)
//• Has CategoryId – an integer, foreign key (required)
//• Has Category – a Category (required)
//• Has UsersBooks – a collection of type IdentityUserBook
