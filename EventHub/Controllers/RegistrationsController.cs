using EventHub.BLL.Services;
using EventHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventHub.Controllers
{
    [Authorize]
    public class RegistrationsController : Controller
    {
        private readonly RegistrationService _registrationService;
        private readonly UserManager<IdentityUser> _userManager;

        public RegistrationsController(
            RegistrationService registrationService,
            UserManager<IdentityUser> userManager)
        {
            _registrationService = registrationService;
            _userManager = userManager;
        }

        // Logged-in users can see registrations
        public async Task<IActionResult> Index()
        {
            var registrations = await _registrationService.GetAllAsync();

            if (User.IsInRole("Admin"))
            {
                return View(registrations);
            }

            var userId = _userManager.GetUserId(User);

            registrations = registrations
                .Where(r => r.UserId == userId)
                .ToList();

            return View(registrations);
        }

        // Users can view their own registration.
        // Admins can view any registration.
        public async Task<IActionResult> Details(int id)
        {
            var registration = await _registrationService.GetByIdAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") &&
                registration.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            return View(registration);
        }

        // Create registration
        public async Task<IActionResult> Create()
        {
            ViewBag.EventId = new SelectList(
                await _registrationService.GetEventsAsync(),
                "EventId",
                "Title");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EventId = new SelectList(
                    await _registrationService.GetEventsAsync(),
                    "EventId",
                    "Title",
                    registration.EventId);

                return View(registration);
            }

            registration.UserId = _userManager.GetUserId(User)!;
            registration.RegistrationDate = DateTime.Now;

            await _registrationService.CreateAsync(registration);

            return RedirectToAction(nameof(Index));
        }

        // Edit own registration
        public async Task<IActionResult> Edit(int id)
        {
            var registration = await _registrationService.GetByIdAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") &&
                registration.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            ViewBag.EventId = new SelectList(
                await _registrationService.GetEventsAsync(),
                "EventId",
                "Title",
                registration.EventId);

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

            var existingRegistration =
                await _registrationService.GetByIdAsync(id);

            if (existingRegistration == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") &&
                existingRegistration.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.EventId = new SelectList(
                    await _registrationService.GetEventsAsync(),
                    "EventId",
                    "Title",
                    registration.EventId);

                return View(registration);
            }

            registration.UserId = existingRegistration.UserId;
            registration.RegistrationDate =
                existingRegistration.RegistrationDate;

            await _registrationService.UpdateAsync(registration);

            return RedirectToAction(nameof(Index));
        }

        // Only Admin can delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var registration =
                await _registrationService.GetByIdAsync(id);

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