using Microsoft.AspNetCore.Mvc;
using SoftUniBazar.Data;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace SoftUniBazar.Models
{
    public class AdFormViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = DataConstants.ErrorRequiredMessage)]
        [StringLength(DataConstants.AdNameMaxLenght,
            MinimumLength = DataConstants.AdNameMinLenght,
            ErrorMessage =DataConstants.ErrorStringLenghtMessage)]
        public string Name { get; set; } = string.Empty;
       
        [Required(ErrorMessage = DataConstants.ErrorRequiredMessage)]
        [StringLength(DataConstants.AdDescriptionMaxLenght, 
            MinimumLength =DataConstants.AdDescriptionMinLenght,
            ErrorMessage = DataConstants.ErrorStringLenghtMessage)]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = DataConstants.ErrorRequiredMessage)]
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        [Required(ErrorMessage =DataConstants.ErrorRequiredMessage)]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
//⦁	Has Name – a string with min length 5 and max length 25 (required)
//⦁	Has Description – a string with min length 15 and max length 250 (required)
//⦁	Has Price – a decimal (required)
//⦁	Has OwnerId – a string (required)
//⦁	Has Owner – an IdentityUser (required)
//⦁	Has ImageUrl – a string (required)
//⦁	Has CreatedOn – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
//⦁	Has CategoryId – an integer, foreign key (required)
//⦁	Has Category – a Category (required)
