using System.Threading.Tasks;
using HogwartsPotions.DataAccess;
using HogwartsPotions.Models;
using HogwartsPotions.Service;
using HogwartsPotions.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _recipeService.GetAllRecipes());
        }
    }
}
