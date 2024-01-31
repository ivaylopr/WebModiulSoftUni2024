using static ForumApp24.Infrastructure.Constants.PostConstants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ForumApp24.Infrastructure.Models
{
    [Comment("Post model responsive about the Db information")]
    public class Post
    {
        [Key]
        [Comment("Primary Identifier")]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(TitleMaxLenght)]
        [Comment("Post Title")]
        public string Title { get; set; } = String.Empty;
       
        [Required]
        [MaxLength(ContentMaxLenght)]
        [Comment("Post Content")]
        public string Content { get; set; }= String.Empty;
    }
}
