using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskBoardAppDbContext data;
        public TaskController(TaskBoardAppDbContext _dbContext)
        {
            data = _dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel taskModel = new TaskFormModel()
            {
                Boards = GetBoards()
            };
            return View(taskModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel taskModel)
        {
            if (!GetBoards().Any(b=>b.Id==taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId),"Board doesn't exist!");
            }
            string currenUserId = GetUserId();
            if (!ModelState.IsValid)
            {
                taskModel.Boards = GetBoards();
                return View(taskModel);
            }
            Task task = new Task()
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
            };
            return View(taskModel);
        }

        private async Task<string> GetUserId()
        => await User.FindFirstValueAsync(ClaimTypes.NameIdentifier);

        private IEnumerable<TaskBoardModel> GetBoards()
        => data.Boards.Select(b => new TaskBoardModel()
        {
            Id = b.Id,
            Name = b.Name
        });
    }
}
