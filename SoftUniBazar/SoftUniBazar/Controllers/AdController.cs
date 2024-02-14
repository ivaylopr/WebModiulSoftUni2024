using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Data;
using SoftUniBazar.Models;
using System.Security.Claims;

namespace SoftUniBazar.Controllers
{
    [Authorize]
    public class AdController : Controller
    {
        private readonly BazarDbContext data;
        public AdController(BazarDbContext context)
        {
            data = context;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await data.Ads.Select(a => new AdViewModel()
            {
                Id=a.Id,
                Name = a.Name,
                ImageUrl = a.ImageUrl,
                Description = a.Description,
                CreatedOn = a.CreatedOn.ToString(DataConstants.DateFormat),
                Category = a.Gategory.Name,
                Price = a.Price,
                Owner = a.Owner.UserName
            }).ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var ad = await data.Ads.Where(a => a.Id == id)
                            .Include(ab => ab.AdsBuyer)
                            .FirstOrDefaultAsync();
            string userId = await GetUserId();
            if (ad is null)
            {
                return BadRequest();
            }
            if (!(ad.AdsBuyer.Any(ab=>ab.BuyerId==userId && ab.AdId==id)))
            {
                ad.AdsBuyer.Add(new AdBuyer()
                {
                    BuyerId= userId,
                    AdId= ad.Id
                });
                await data.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Cart));   
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var ad = await data.Ads.Where(a => a.Id == id)
                            .Include(ab => ab.AdsBuyer)
                            .FirstOrDefaultAsync();
            string userId = await GetUserId();
            if (ad is null)
            {
                return BadRequest();
            }
            var aB = ad.AdsBuyer.FirstOrDefault(ab => ab.AdId == id && ab.BuyerId==userId);
            if (aB is null)
            {
                return BadRequest();
            }
            ad.AdsBuyer.Remove(aB);
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AdFormViewModel();
            model.Categories = await GetCategories();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AdFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }
           
            string userId =await GetUserId() ;
            
            var entity = new Ad()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CreatedOn = DateTime.Now,
                OwnerId = userId
            };
            await data.Ads.AddAsync(entity);
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(Cart));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await data.Ads
                .FirstOrDefaultAsync(a=>a.Id==id);

            if (e == null)
            {
                return BadRequest();
            }

            if (e.OwnerId !=await GetUserId())
            {
                return Unauthorized();
            }
            var model = new AdFormViewModel()
            {
                Name = e.Name,
                ImageUrl = e.ImageUrl,
                Description = e.Description,
                Price = e.Price,
                CategoryId = e.CategoryId
            };
            model.Categories = await GetCategories();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AdFormViewModel model, int id)
        {
            var e = await data.Ads.FindAsync(id);
            if (e is null)
            {
                return BadRequest();
            }
            if (e.OwnerId!=await GetUserId())
            {
                return Unauthorized();
            }
            DateTime edited = DateTime.Now;
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategories();
                return View(model);
            }
            e.Name= model.Name;
            e.Description= model.Description;
            e.Price= model.Price;
            e.CategoryId= model.CategoryId;
            e.CreatedOn = edited;
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            string userId = await GetUserId();
            var ads = await data.Ads
                .Include(a=>a.AdsBuyer)
                .Where(ab=>ab.AdsBuyer.Any(ab=>ab.BuyerId==userId))
                .AsNoTracking()
                .Select(a=>new AdCartViewModel()
            {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,    
                    Price = a.Price,
                    ImageUrl= a.ImageUrl,
                    Category = a.Gategory.Name,
                    Owner = a.Owner.UserName,
                    CreatedOn=a.CreatedOn.ToString(DataConstants.DateFormat)
            }).ToListAsync();
            return View(ads);
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
