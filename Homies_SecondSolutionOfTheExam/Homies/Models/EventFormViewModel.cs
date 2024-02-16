using Homies.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace Homies.Models
{
    public class EventFormViewModel
    {
        [Required(ErrorMessage =DataConstants.RequiredErrorMessage)]
        [StringLength(DataConstants.EventNameMaxLenght,
            MinimumLength=DataConstants.EventNameMinLenght,
            ErrorMessage =DataConstants.StringLenghtErrorMessage)]
        [Comment("Name of the event")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage =DataConstants.RequiredErrorMessage)]
        [StringLength(DataConstants.EventDescriptionMaxLenght,
            MinimumLength=DataConstants.EventDescriptionMinLenght,
            ErrorMessage =DataConstants.StringLenghtErrorMessage)]
        [Comment("Description of the event")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = DataConstants.RequiredErrorMessage)]
        public string Start { get; set; } = string.Empty;
        [Required(ErrorMessage = DataConstants.RequiredErrorMessage)]
        public string End { get; set; } = string.Empty;
        [Required(ErrorMessage = DataConstants.RequiredErrorMessage)]
        public int TypeId { get; set; } 
        public IEnumerable<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
    }
}