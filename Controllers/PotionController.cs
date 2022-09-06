using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    
    public class PotionController : Controller
    {
        private readonly HogwartsContext _context;
        public PotionController(HogwartsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Potions.ToList());
        }

        [HttpGet]
        public async Task<List<Potion>> GetPotions()
        {
            return await _context.GetPotions();
        }

        [HttpPost]
        public async Task<Potion> CreatePotion([FromBody]Potion potion)
        {
            return await _context.CreateAPotion(potion);
        }

        [HttpGet("/potions/{studentId:long}")]
        public async Task<List<Potion>> GetAllPotionsOfAStudent(long studentId)
        {
            return await _context.GetPotionsOfAStudent(await _context.GetStudent(studentId));
        }

        [HttpPost("/potions/brew")]
        public async Task<Potion> FinishPotion([FromBody] Potion potion)
        {
            return await _context.FinishPotion(potion);
        }

        [HttpPut("/potions/{potionId}/add")]
        public async Task<Potion> AddIngredient([FromBody] Ingredient ingredient, long potionId)
        {
            return await _context.AddIngredient(ingredient, potionId);
        }

        [HttpGet("/potions/{potionId}/help")]
        public async Task<List<Recipe>> GetRecipes(long potionId)
        {
            return await _context.GetRecipes(potionId);
        }
    }
}
