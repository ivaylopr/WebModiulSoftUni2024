using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Board;

namespace TaskBoardApp.Models.Task
{
    public class TaskFormModel
    {
        [Required]
        [StringLength(DataConstants.Task.TitleMaxLenght
            ,MinimumLength = DataConstants.Task.TitleMinLenght
            ,ErrorMessage ="Title should be between {2} and {1} characters long")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(DataConstants.Task.DesctiptionMaxLenght,
            MinimumLength = DataConstants.Task.TitleMinLenght,
            ErrorMessage = "Description should be between {2} and {1} characters long")]
        public string Description { get; set; } = string.Empty;
        public int? BoardId { get; set; } 
        public IEnumerable<TaskBoardModel> Boards { get; set; } = new List<TaskBoardModel>();
    }
}
