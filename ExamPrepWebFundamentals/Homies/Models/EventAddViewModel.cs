namespace Homies.Models;
using Homies.Data;

using System.ComponentModel.DataAnnotations;
using static Homies.Data.DataConstants;

public class EventAddViewModel
{
    [Required(ErrorMessage = RequireMessage)]
    [StringLength(
        DataConstants.EventNameMaxLenght
        , MinimumLength = EventNameMinLenght
        , ErrorMessage = StringLenghtMessage)]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage =RequireMessage)]
    [StringLength(
        DataConstants.EventDescriptionMaxLenght,
        MinimumLength = EventDescriptionMinLenght, 
        ErrorMessage =StringLenghtMessage)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = RequireMessage)]
    public string Start { get; set; } = string.Empty;

    [Required(ErrorMessage = RequireMessage)]
    public string End { get; set; } = string.Empty;
    [Required(ErrorMessage = RequireMessage)]
    public int TypeId { get; set; }
    [Required(ErrorMessage = RequireMessage)]
    public IEnumerable<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
}
