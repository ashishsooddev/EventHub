using Microsoft.AspNetCore.Mvc;
using EventHub.BLL.Services;
using EventHub.Models;
using Microsoft.AspNetCore.Authorization;

namespace EventHub.Controllers
{
    public class EventsController : Controller
    {
    private readonly EventService _eventService;

        public EventsController(EventService eventService)
        {
            _eventService = eventService;
        }

        [AllowAnonymous] // used so anyone can view event details.
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllAsync();
            return View(events);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var eventItem = await _eventService.GetByIdAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }
            return View(eventItem);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize]
        public async Task<IActionResult> Create(Event eventItem)
        {
            if (!ModelState.IsValid)
            {
                return View(eventItem);
            }

            await _eventService.CreateAsync(eventItem);
            return RedirectToAction(nameof(Index));
        }
    }
}
