using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ForumApp24.Infrastructure.Constants.ValidationConstants;
namespace ForumApp24.Infrastructure.Data.Models
{
    [Comment("Db post model")]
    public class Post
    {
        [Key]
        [Comment("Identifire")]
        public int Id { get; set; }
        [Required]
        [MaxLength(TitleMaxLenght)]
        [Comment("Post title")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(ContentMaxLenght)]
        [Comment("Post content")]
        public string Content { get; set; } = string.Empty;

    }
}
