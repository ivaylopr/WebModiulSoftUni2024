using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TaskBoardApp.Data.Models
{
    [Comment("Board of the tasks")]
	public class Board
	{
		[Key]
        [Comment("Identifier")]
        public int Id { get; set; }
        [Required]
        [MaxLength(DataConstants.Board.NameMaxLenght)]
        [Comment("Name of the board")]
        public string Name { get; set; } = string.Empty;
        [Comment("Collection of tasks")]
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}

