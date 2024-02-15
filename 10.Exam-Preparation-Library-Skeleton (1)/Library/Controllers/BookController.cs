using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext data;
        public BookController(LibraryDbContext context)
        {
            data = context;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await data.Books
                .Select(b => new BookViewModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Rating = b.Rating,
                    Category = b.Category.Name,
                    ImageUrl = b.ImageUrl,
                }).ToListAsync();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            string userId = await GetUserId();
            var model = await data.UserBooks
                .Where(ub => ub.CollectorId == userId)
                .Select(ub => new BookMineViewModel()
                {
                    Id = ub.BookId,
                    Title = ub.Book.Title,
                    Author = ub.Book.Author,
                    ImageUrl = ub.Book.ImageUrl,
                    Description = ub.Book.Description,
                    Category = ub.Book.Category.Name
                }).ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            string userId = await GetUserId();
            var alreadyAdded = await data.UserBooks.AnyAsync(ub => ub.CollectorId == userId && ub.BookId == id);
            if (!alreadyAdded)
            {
                var userBook = new UserBook()
                {
                    CollectorId = userId,
                    BookId = id,
                };
                await data.UserBooks.AddAsync(userBook);
                await data.SaveChangesAsync();
                return RedirectToAction(nameof(Mine));
            }
            return RedirectToAction(nameof(All));

        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var userId = await GetUserId();
            var bookToRemove = await data.UserBooks.FirstOrDefaultAsync(ub => ub.CollectorId == userId && ub.BookId == id);
            if (bookToRemove is not null)
            {
                data.Remove(bookToRemove);
                await data.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Mine));

        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new BookAddFormModel();
            model.Categories = await GetCategories();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(BookAddFormModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Categories=await GetCategories();
                return View(model);
            }
            var entity = new Book()
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                Rating = model.Rating,
                CategoryId = model.CategoryId,
                ImageUrl = model.Url
            };
            await data.Books.AddAsync(entity);
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));

        }
        private async Task<string> GetUserId()
            => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await data.Categories.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
        }
    }
}
