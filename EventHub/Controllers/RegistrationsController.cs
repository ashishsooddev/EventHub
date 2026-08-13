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

        public async Task<IActionResult> Edit(int id)
        {
            var registration = await _registrationService.GetByIdAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Registration registration)
        {
            if (id != registration.RegistrationId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(registration);
            }

            await _registrationService.UpdateAsync(registration);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var registration = await _registrationService.GetByIdAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _registrationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
