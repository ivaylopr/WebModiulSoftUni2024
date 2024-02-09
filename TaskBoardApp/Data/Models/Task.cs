using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoardApp.Data.Models
{
    [Comment("Board Tasks")]
    public class Task
    {
        [Key]
        [Comment("Identifier")]
        public int Id { get; set; }
        [Required]
        [MaxLength(DataConstants.Task.TitleMaxLenght)]
        [Comment("Title of the task")]
        public string Title { get; set; } = String.Empty;
        [Required]
        [MaxLength(DataConstants.Task.DesctiptionMaxLenght)]
        [Comment("Description of the task")]
        public string Description { get; set; } = String.Empty;
        [Comment("Date when the task is created")]
        public DateTime? CreatedOn { get; set; }
        [Comment("Identifier of the board who is on the task")]
        public int? BoardId { get; set; }
        [Comment("Navigation prop of the board on the task")]
        public Board? Board { get; set; }
        [Required]
        [Comment("The user who owe the task")]
        public string OwnerId { get; set; } = string.Empty;
        
        [ForeignKey(nameof(OwnerId))]
        
        public IdentityUser Owner { get; set; } = null!;
    }
}
//⦁	Id – a unique integer, Primary Key
//⦁	Title – a string with min length 5 and max length 70 (required)
//⦁	Description – a string with min length 10 and max length 1000 (required)
//⦁	CreatedOn – date and time
//⦁	BoardId – an integer
//⦁	Board – a Board object
//⦁	OwnerId – an integer (required)
//⦁	Owner – an IdentityUser object
