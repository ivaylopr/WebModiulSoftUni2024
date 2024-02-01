using System.ComponentModel.DataAnnotations;
using static ForumApp24.Infrastructure.Constants.ValidationConstants;
namespace ForumApp24.Core.Models
{
    /// <summary>
    /// Post transfer model
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// Post identificator
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Post title
        /// </summary>
        [Required(ErrorMessage = ErrorMessegaRequared)]
        [StringLength(TitleMaxLenght,MinimumLength=TitleMinLenght,ErrorMessage = StringLenghtErrorMessage)]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Post content
        /// </summary>
        [Required(ErrorMessage =ErrorMessegaRequared)]
        [StringLength(ContentMaxLenght, MinimumLength = ContentMinLenght, ErrorMessage = StringLenghtErrorMessage)]
        public string Content { get; set; } = string.Empty;
    }
}
