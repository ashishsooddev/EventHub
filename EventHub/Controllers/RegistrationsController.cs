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
    }
}
