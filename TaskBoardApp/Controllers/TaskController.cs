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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TaskFormModel taskModel = new TaskFormModel()
            {
                Boards = await GetBoards()
            };
            return View(taskModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel taskModel)
        {
            if (!(await GetBoards()).Any(b => b.Id == taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Board doesn't exist!");
            }
            string currenUserId = GetUserId();
            if (!ModelState.IsValid)
            {
                taskModel.Boards = await GetBoards();
                return View(taskModel);
            }
            var task = new Data.Models.Task()
            {
                BoardId=taskModel.BoardId,
                Description=taskModel.Description,
                Title=taskModel.Title,
                CreatedOn= DateTime.Now,
                OwnerId = currenUserId,
            };
            await data.AddAsync(task);
            await data.SaveChangesAsync();  
            return RedirectToAction("Index","Board");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var task = data.Tasks.Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel()
                {
                    Id = t.Id,
                    Description = t.Description,
                    Title = t.Title,
                    CreatedOn=t.CreatedOn.ToString(),
                    Board = t.Board.Name,
                    Owner = t.Owner.UserName
                });
            if (task==null)
            {
                return BadRequest();
            }
            return View(task);
        }

        private string GetUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private async Task<IEnumerable<TaskBoardModel>> GetBoards()
        => data.Boards.Select(b => new TaskBoardModel()
        {
            Id = b.Id,
            Name = b.Name
        });
    }
}
