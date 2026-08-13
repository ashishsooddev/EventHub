using Microsoft.AspNetCore.Mvc;
using EventHub.BLL.Services;
using EventHub.Models;
using Microsoft.AspNetCore.Authorization;

namespace EventHub.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryService _categoryService;
        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

    }
}
