using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                Boards = await GetBoardsAsync()
            };
            return View(taskModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel taskModel)
        {
            if (!(await GetBoardsAsync()).Any(b => b.Id == taskModel.BoardId))
            {
                ModelState.AddModelError(nameof(taskModel.BoardId), "Board doesn't exist!");
            }
            string currenUserId = GetUserId();
            if (!ModelState.IsValid)
            {
                taskModel.Boards = await GetBoardsAsync();
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
           var model = await data.Tasks.Where(t => t.Id == id).Select(t=>new TaskDetailsViewModel
           {
               Id = t.Id,
               Title = t.Title,
               Description = t.Description, 
               CreatedOn = t.CreatedOn.Value.ToString("dd/MM/yyyy hh:mm"),
               Board = t.Board.Name,
               Owner = t.Owner.UserName
           }).FirstOrDefaultAsync();
            if (model is null)
            {
                return BadRequest();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit (int id)
        {
            var task = await data.Tasks.FindAsync(id);
            if (task is null) 
            {
                return BadRequest();
            }
            string currentUserId = GetUserId();
            if (currentUserId!=task.OwnerId)
            {
                return Unauthorized();
            }
            TaskFormModel taskModel = new TaskFormModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = await GetBoardsAsync()
            };
            return View(taskModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,TaskFormModel model)
        {
            var task = await data.Tasks.FindAsync(id);
            if (task is null)
            {
                return BadRequest();
            }
            string currentUserId = GetUserId();
            if (currentUserId!=task.OwnerId)
            {
                return Unauthorized();
            }
            if (!(await GetBoardsAsync()).Any(b=>b.Id==model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board doesn't exist!");
            }
            if (!ModelState.IsValid)
            {
                model.Boards=await GetBoardsAsync();
                return View(model);
            }
            task.Title = model.Title;
            task.Description = model.Description;
            task.BoardId = model.BoardId;
            await data.SaveChangesAsync();
            return RedirectToAction("Index","Board");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await data.Tasks.FindAsync(id);
            if (model is null)
            {
                return BadRequest();
            }
            string currentUserId = GetUserId();
            if (currentUserId!=model.OwnerId)
            {
                return Unauthorized();
            }
            var taskModel = new TaskViewModel()
            {
                Id=model.Id,
                Title=model.Title,
                Description=model.Description
            };
            return View(taskModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel model)
        {
            var entity = await data.Tasks.FindAsync(model.Id);
            if (entity is null)
            {
                return BadRequest();
            }
            string currentUser = GetUserId();
            if (currentUser!=entity.OwnerId)
            {
                return Unauthorized();
            }
            data.Tasks.Remove(entity);
            await data.SaveChangesAsync();
            return RedirectToAction("Index","Board");
        }
        private string GetUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier);

        private async Task<IEnumerable<TaskBoardModel>> GetBoardsAsync()
        => data.Boards.Select(b => new TaskBoardModel()
        {
            Id = b.Id,
            Name = b.Name
        });
    }
}
