using Microsoft.AspNetCore.Mvc;
using EventHub.BLL.Services;
using EventHub.Models;
using Microsoft.AspNetCore.Authorization;

namespace EventHub.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly RegistrationService _registrationService;
        public RegistrationsController(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        public async Task<IActionResult> Index()
        {
            var registrations = await _registrationService.GetAllAsync();
            return View(registrations);
        }

        public async Task<IActionResult> Details(int id)
        {
            var registration = await _registrationService.GetByIdAsync(id);

            if (registration == null)
            {
                return NotFound();
            }
            return View(registration);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            registration.RegistrationDate = DateTime.Now;

            await _registrationService.CreateAsync(registration);
            return RedirectToAction(nameof(Index));
        }
    }
}
