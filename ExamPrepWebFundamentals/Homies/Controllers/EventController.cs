using Homies.Data;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext data;
        public EventController(HomiesDbContext context)
        {
            data = context;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var events = await data.Events.Select(e => new EventInfoViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Start = e.Start.ToString(DataConstants.DateFormat),
                Type = e.Type.Name,
                Organiser = e.Organiser.UserName
            }).ToListAsync();
            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var e = await data.Events.Where(e => e.Id == id).Include(e => e.EventParticipants).FirstOrDefaultAsync();
            
            if (e is null)
            {
                return BadRequest();
            }
            if (e.EventParticipants.Any(e => e.HelperId == GetUserId()))
            {
                return RedirectToAction(nameof(Joined));
            }
            if (!e.EventParticipants.Any(ep => ep.HelperId == GetUserId()))
            {
                e.EventParticipants.Add(new EventParticipant()
                {
                    EventId = e.Id,
                    HelperId = GetUserId()
                });
                await data.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Joined));
        }
        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            var userId = GetUserId();
            var model = await data.EventParticipants.Where(ep => ep.HelperId == userId)
                .AsNoTracking()
                .Select(eq => new EventInfoViewModel()
                {
                    Id = eq.EventId,
                    Name = eq.Event.Name,
                    Start = eq.Event.Start.ToString(DataConstants.DateFormat),
                    Type = eq.Event.Type.Name,
                    Organiser = eq.Event.Organiser.UserName
                }).ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var userId = GetUserId();
            var e = await data.Events
                 .Where(e => e.Id == id)
                 .Include(e => e.EventParticipants)
                 .FirstOrDefaultAsync();
            if (e is null)
            {
                return BadRequest();
            }
            var ep = e.EventParticipants.FirstOrDefault(ep => ep.HelperId == userId);
            if (ep == null)
            {
                return BadRequest();
            }
            data.EventParticipants.Remove(ep);
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventAddViewModel();
            model.Types = await GetTypes();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(EventAddViewModel model)
        {
            DateTime start = DateTime.Now;
            DateTime end=DateTime.Now;
            if (!DateTime.TryParseExact(
                model.Start,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.Start)
                    ,$"Invalid date format, it should be at format: {DataConstants.DateFormat}");
            } 
            if (!DateTime.TryParseExact(
                model.End,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None,
                out end))
            {
                ModelState.AddModelError(nameof(model.End)
                    ,$"Invalid date format, it should be at format: {DataConstants.DateFormat}");
            }
            if (start>end)
            {
                ModelState.AddModelError(nameof(model.Start)
                    , "Start date can not be after the end date!");
                return RedirectToAction(nameof(Add));
            }
            if (!ModelState.IsValid)
            {
                model.Types = await GetTypes();
                return View(model);
            }
            var entityToAdd = new Event()
            {
                Name = model.Name, 
                Description = model.Description,
                CreatedOn = DateTime.Now,
                Start = start,
                End = end,
                OrganiserId = GetUserId(),
                TypeId = model.TypeId

            };
            await data.Events.AddAsync(entityToAdd);
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));

        }
        [HttpGet]   
        public async Task<IActionResult> Edit(int id)
        {
            var e = await data.Events.FindAsync(id);
            if (e == null)
            {
                return BadRequest();
            }
            if (e.OrganiserId != GetUserId())
            {
                return Unauthorized();
            }
            var model = new EventAddViewModel()
            {
                Name = e.Name,
                Description = e.Description,
                Start = e.Start.ToString(DataConstants.DateFormat),
                End = e.End.ToString(DataConstants.DateFormat),
                TypeId = e.TypeId
            };
            model.Types = await GetTypes();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EventAddViewModel model, int id)
        {
            var e = await data.Events.FindAsync(id);
            if (e==null)
            {
                return BadRequest();
            }
            if (e.OrganiserId!=GetUserId())
            {
                return Unauthorized();
            }
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            if (!DateTime.TryParseExact(
                model.Start,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.Start),$"Invalid date format, it should be at format: {DataConstants.DateFormat}");
            }
            if (!DateTime.TryParseExact(
                model.End,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.End),$"Invalid date format, it should be at format: {DataConstants.DateFormat}");
            }
            if (start > end)
            {
                ModelState.AddModelError(nameof(model.Start)
                    , "Start date can not be after the end date!");
                return RedirectToAction(nameof(Add));
            }
            if (!ModelState.IsValid)
            {
                model.Types =await GetTypes();
                return View(model);
            }
            e.Start = start;
            e.End = end;
            e.Name = model.Name;
            e.Description = model.Description;
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All)); 

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await data.Events.Where(m=>m.Id==id)
                .Select(m=>new EventDetailsViewModel
            {
                Description=m.Description,
                Start = m.Start.ToString(DataConstants.DateFormat),
                End=m.End.ToString(DataConstants.DateFormat),  
                CreatedOn = m.CreatedOn.ToString(DataConstants.DateFormat),
                Organiser = m.Organiser.UserName,
                Type = m.Type.Name
            }).FirstOrDefaultAsync();
            if (model is null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return View(model);

        }
        private string GetUserId()
        => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? String.Empty;
        private async Task<IEnumerable<TypeViewModel>> GetTypes()
        {
            return await data.Types.AsNoTracking().Select(t => new TypeViewModel()
                    {
                        Id = t.Id,
                        Name = t.Name,
                    }).ToListAsync();
        }
    }
}
