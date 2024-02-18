using SeminarHub.Data;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models
{
    public class SeminarFormViewModel
    {
        [Required(ErrorMessage = DataConstants.RequiredErrorMessage)]
        [StringLength(DataConstants.SeminarTopicMaxLenght,
            MinimumLength= DataConstants.SeminarTopicMinLenght,
            ErrorMessage = DataConstants.LenghtErrorMessage)]
        public string Topic { get; set; } = string.Empty;
        [Required(ErrorMessage = DataConstants.RequiredErrorMessage)]
        [StringLength(DataConstants.SeminarLecturerMaxLenght,
            MinimumLength = DataConstants.SeminarLecturerMinLenght,
            ErrorMessage = DataConstants.LenghtErrorMessage)]
        public string Lecturer { get; set; } = string.Empty;
        [Required(ErrorMessage = DataConstants.RequiredErrorMessage)]
        [StringLength(DataConstants.SeminarDetailsMaxLenght,
            MinimumLength = DataConstants.SeminarDetailsMinLenght,
            ErrorMessage = DataConstants.LenghtErrorMessage)]
        public string Details { get; set; } = string.Empty;
        [Required(ErrorMessage =DataConstants.RequiredErrorMessage)]
        public string DateAndTime { get; set; } = string.Empty;
        [Range(DataConstants.SeminarDurationMinRange,DataConstants.SeminarDurationMaxRange)]
        public int? Duration { get; set; }
        [Required(ErrorMessage = DataConstants.RequiredErrorMessage)]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
