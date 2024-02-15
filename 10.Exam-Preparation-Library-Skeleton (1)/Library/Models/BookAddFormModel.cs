using Library.Data;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    [Authorize]
    public class BookAddFormModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = DataConstants.RequiredErrorMessage)]
        [StringLength(DataConstants.BookTitleMaxLenght
            ,MinimumLength = DataConstants.BookTitleMinLenght
            ,ErrorMessage =DataConstants.StringLenghtErrorMessage)]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = DataConstants.RequiredErrorMessage)]
        [StringLength(DataConstants.AuthorMaxLenght
            ,MinimumLength = DataConstants.AuthorMinLenght
            ,ErrorMessage = DataConstants.StringLenghtErrorMessage)]
        public string Author { get; set; } = string.Empty;
        [Required(ErrorMessage =DataConstants.RequiredErrorMessage)]
        public string Url { get; set; } = string.Empty;
        [Required(ErrorMessage =DataConstants.RequiredErrorMessage)]
        [StringLength(DataConstants.DescriptionMaxLenght
            , MinimumLength = DataConstants.DescriptionMinLenght
            ,ErrorMessage =DataConstants.StringLenghtErrorMessage)]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage =DataConstants.RequiredErrorMessage)]
        [Range(DataConstants.RatingMinRange,DataConstants.RatingMaxRange,ErrorMessage =DataConstants.RangeRatingErrorMessage)]
        public decimal Rating { get; set; }
        [Required(ErrorMessage =DataConstants.RequiredErrorMessage)]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
//• Has Title – a string with min length 10 and max length 50 (required)
//• Has Author – a string with min length 5 and max length 50 (required)
//• Has Description – a string with min length 5 and max length 5000 (required)
//• Has ImageUrl – a string (required)
//• Has Rating – a decimal with min value 0.00 and max value 10.00 (required)