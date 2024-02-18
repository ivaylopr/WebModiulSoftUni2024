using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Models;
using System.Globalization;
using System.Security.Claims;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext data;
        public SeminarController(SeminarHubDbContext context)
        {
            data = context;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var seminars = await data.Seminars
                .AsNoTracking()
                .Select(s => new SeminarViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    Organizer = s.Organizer.UserName,
                    DateAndTime = s.DateAndTime.ToString(DataConstants.DateFormat)
                }).ToListAsync();
            return View(seminars);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new SeminarFormViewModel();
            model.Categories = await GetCategoriesAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormViewModel model)
        {
            string userId = await GetUserIdAsync();
            DateTime start = DateTime.Now;
            if (!DateTime.TryParseExact(model.DateAndTime,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.DateAndTime),
                    $"Invalid date/time format! It must be: {DataConstants.DateFormat}");
            }
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();
                return View(model);
            }
            Seminar seminarToAdd = new Seminar()
            {
                Topic = model.Topic,
                Lecturer = model.Lecturer,
                Details = model.Details,
                OrganizerId = userId,
                DateAndTime = start,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
            };
            await data.Seminars.AddAsync(seminarToAdd);
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var seminarToJoin = await data.Seminars
                .Where(s => s.Id == id)
                .Include(s => s.SeminarsParticipants)
                .FirstOrDefaultAsync();
            string userId = await GetUserIdAsync();
            if (seminarToJoin is null)
            {
                return BadRequest();
            }
            if (!seminarToJoin.SeminarsParticipants
                .Any(sp => sp.ParticipantId == userId))
            {
                seminarToJoin.SeminarsParticipants
                    .Add(new SeminarParticipant
                    {
                        SeminarId = seminarToJoin.Id,
                        ParticipantId = userId,
                    });
                await data.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Joined));

        }
        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = await GetUserIdAsync();
            var model = await data.SeminarParticipants
                .Where(sp => sp.ParticipantId == userId)
                .Select(sp => new SeminarViewModel()
                {
                    Id = sp.SeminarId,
                    Topic = sp.Seminar.Topic,
                    Lecturer = sp.Seminar.Lecturer,
                    Category = sp.Seminar.Category.Name,
                    Organizer = sp.Seminar.Organizer.UserName,
                    DateAndTime = sp.Seminar.DateAndTime.ToString(DataConstants.DateFormat)
                }).ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var seminar = await data.Seminars
                .Include(s => s.SeminarsParticipants)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
            string userId = await GetUserIdAsync();
            if (seminar is null)
            {
                return BadRequest();
            }
            var seminarParticipant = seminar.SeminarsParticipants.FirstOrDefault(sp => sp.ParticipantId == userId);
            if (seminarParticipant is null)
            {
                return BadRequest();
            }
            seminar.SeminarsParticipants.Remove(seminarParticipant);
            await data.SaveChangesAsync();
            return RedirectToAction(nameof(Joined));

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var seminarToEdit = await data.Seminars.FindAsync(id);
            string userId = await GetUserIdAsync();
            if (seminarToEdit is null)
            {
                return BadRequest();
            }
            if (seminarToEdit.OrganizerId != userId)
            {
                return Unauthorized();
            }
            var model = new SeminarFormViewModel()
            {
                Topic = seminarToEdit.Topic,
                Lecturer = seminarToEdit.Lecturer,
                Details = seminarToEdit.Details,
                DateAndTime = seminarToEdit.DateAndTime.ToString(DataConstants.DateFormat),
                Duration = seminarToEdit.Duration,
                CategoryId = seminarToEdit.CategoryId
            };
            model.Categories = await GetCategoriesAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SeminarFormViewModel model, int id)
        {
            var seminar = await data.Seminars.FindAsync(id);
            string userId = await GetUserIdAsync();
            if (seminar is null)
            {
                return BadRequest();
            }
            if (seminar.OrganizerId != userId)
            {
                return Unauthorized();
            }
            DateTime start = DateTime.Now;
            if (!DateTime.TryParseExact(model.DateAndTime,
                DataConstants.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.DateAndTime),
                    $"Invalid date/time format! It must be: {DataConstants.DateFormat}");
            }
            if (!ModelState.IsValid)
            {
                model.Categories = await GetCategoriesAsync();
                return View(model);
            }

            seminar.Topic = model.Topic;
            seminar.Lecturer = model.Lecturer;
            seminar.Details = model.Details;
            seminar.DateAndTime = start;
            seminar.Duration = model.Duration;
            seminar.CategoryId = model.CategoryId;

            await data.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await data.Seminars
                .Where(s => s.Id == id)
                .Select(s => new SeminarDetailsViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Organizer = s.Organizer.UserName,
                    Category = s.Category.Name,
                    Details = s.Details,
                    Duration = s.Duration,
                    DateAndTime = s.DateAndTime.ToString(DataConstants.DateFormat)
                }).FirstOrDefaultAsync();
            if (model is null)
            {
                return BadRequest();
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = await GetUserIdAsync();
            var model = await data.Seminars
                .Where(s => s.Id == id && s.OrganizerId == userId)
                .Select(s => new SeminarDeleteViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime
                })
                .FirstOrDefaultAsync();
            if (model is null)
            {
                return BadRequest();
            }
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(SeminarDeleteViewModel model)
        {
            string userId = await GetUserIdAsync();
            var seminarToRemove = await data.Seminars
                                       .Where(s => s.Id == model.Id && s.OrganizerId == userId)
                                       .FirstOrDefaultAsync();
            var seminarParticipantsToRemove = await data.SeminarParticipants
                .Where(sp => sp.SeminarId == model.Id)
                .ToListAsync();
            if (seminarToRemove is null)
            {
                return BadRequest();
            }

            if (seminarParticipantsToRemove.Any())
            {
                foreach (var sp in seminarParticipantsToRemove)
                {
                    data.SeminarParticipants.Remove(sp);
                    await data.SaveChangesAsync();
                }
            }

            data.Seminars.Remove(seminarToRemove);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private async Task<string> GetUserIdAsync()
            => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        private async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            return await data.Categories.Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
        }
    }
}
