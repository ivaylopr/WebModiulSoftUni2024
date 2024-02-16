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
            var model = await data.Events.AsNoTracking().Select(e => new EventInfoViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Start = e.Start.ToString(DataConstants.DateFormat),
                Organiser = e.Organiser.UserName,
                Type = e.Type.Name
            }).ToListAsync();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var e = await data.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();
            string userId = await GetUserId();
            if (e == null)
            {
                return BadRequest();
            }
            if (e.EventsParticipants.Any(ep => ep.HelperId == userId))
            {
                return RedirectToAction(nameof(Joined));
            }
            if (!e.EventsParticipants.Any(ep => ep.HelperId == userId))
            {
                e.EventsParticipants.Add(new EventParticipant()
                {
                    EventId = id,
                    HelperId = userId
                });
                await data.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Joined));
        }
        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = await GetUserId();
            var model = await data.EventsParticipants
                .Where(ep => ep.HelperId == userId)
                .Select(ep => new EventInfoViewModel
                {
                    Id = ep.EventId,
                    Name = ep.Event.Name,
                    Organiser = ep.Event.Organiser.UserName,
                    Start = ep.Event.Start.ToString(DataConstants.DateFormat),
                    Type = ep.Event.Type.Name
                })
                .ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            string userId = await GetUserId();
            var e = await data.Events
                .Include(e => e.EventsParticipants)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
            if (e is null)
            {
                return BadRequest();
            }
            var ep = e.EventsParticipants.FirstOrDefault(ep => ep.HelperId == userId);
            if (ep is null)
            {
                return BadRequest();
            }
            e.EventsParticipants.Remove(ep);
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new EventFormViewModel();
            model.Types = await GetTypes();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormViewModel model)
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            string userId = await GetUserId();
            if (!DateTime.TryParseExact(model.Start
                , DataConstants.DateFormat
                , CultureInfo.InvariantCulture
                , DateTimeStyles.None
                , out start))
            {
                ModelState.AddModelError(nameof(model.Start)
                    , $"Invalid date format, it should be {DataConstants.DateFormat}.");
            }
            if (!DateTime.TryParseExact(model.End
                , DataConstants.DateFormat
                , CultureInfo.InvariantCulture
                , DateTimeStyles.None
                , out end))
            {
                ModelState.AddModelError(nameof(model.End)
                    , $"Invalid date format, it should be {DataConstants.DateFormat}.");
            }
            if (start > end)
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
            Event eventToAdd = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                OrganiserId= userId,
                CreatedOn = DateTime.Now,
                Start = start,
                End = end,
                TypeId= model.TypeId,

            };
            await data.Events.AddAsync(eventToAdd);
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var e = await data.Events.FindAsync(id);
            string userId = await GetUserId();
            if (e is null)
            {
                return BadRequest();
            }
            if (e.OrganiserId!=userId)
            {
                return Unauthorized();
            }
            var model = new EventFormViewModel()
            {
                Name = e.Name,
                Description = e.Description,
                Start = e.Start.ToString(DataConstants.DateFormat),
                End = e.End.ToString(DataConstants.DateFormat),
                TypeId = e.TypeId,
            };
            model.Types = await GetTypes();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EventFormViewModel model, int id)
        {
            var entity = await data.Events.FindAsync(id);
            string userId = await GetUserId();
            if (entity is null)
            {
                return BadRequest();
            }
            if (entity.OrganiserId!=userId)
            {
                return Unauthorized();
            }
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            if (!DateTime.TryParseExact(model.Start
                , DataConstants.DateFormat
                , CultureInfo.InvariantCulture
                , DateTimeStyles.None
                , out start))
            {
                ModelState.AddModelError(nameof(model.Start)
                    , $"Invalid date format, it should be {DataConstants.DateFormat}.");
            }
            if (!DateTime.TryParseExact(model.End
                , DataConstants.DateFormat
                , CultureInfo.InvariantCulture
                , DateTimeStyles.None
                , out end))
            {
                ModelState.AddModelError(nameof(model.End)
                    , $"Invalid date format, it should be {DataConstants.DateFormat}.");
            }
            if (start > end)
            {
                ModelState.AddModelError(nameof(model.Start)
                    , "Start date can not be after the end date!");
                return RedirectToAction(nameof(Edit));
            }
            if (!ModelState.IsValid)
            {
                model.Types = await GetTypes();
                return View(model);
            }
            entity.Name= model.Name;
            entity.Description= model.Description;
            entity.Start= start;
            entity.End = end;
            entity.TypeId = model.TypeId;
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await data.Events.Where(e=>e.Id==id).Select(e => new EventDetailsModel()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Start = e.Start.ToString(DataConstants.DateFormat),
                End = e.End.ToString(DataConstants.DateFormat),
                CreatedOn = e.CreatedOn.ToString(DataConstants.DateFormat),
                Organiser = e.Organiser.UserName,
                Type = e.Type.Name
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

        private async Task<string> GetUserId()
            => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        private async Task<IEnumerable<TypeViewModel>> GetTypes()
        {
            return await data.Types.Select(c => new TypeViewModel()
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
        }
    }
}
