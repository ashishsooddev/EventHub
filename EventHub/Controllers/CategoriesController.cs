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

    }
}
