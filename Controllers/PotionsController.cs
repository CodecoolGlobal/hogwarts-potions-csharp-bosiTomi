using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Service.Interface;
using Microsoft.AspNetCore.Http;

namespace HogwartsPotions.Controllers
{
    public class PotionsController : Controller
    {
        private readonly IPotionService _potionService;
        private readonly IIngredientService _ingredientService;
        private readonly IStudentService _studentService;

        public PotionsController(IPotionService potionService, IIngredientService ingredientService, IStudentService studentService)
        {
            _potionService = potionService;
            _ingredientService = ingredientService;
            _studentService = studentService;
        }

        // GET: Potions
        public async Task<IActionResult> Index()
        {
              return View(await _potionService.GetAllPotions());
        }

        // GET: Potions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || await _potionService.GetAllPotions() == null)
            {
                return NotFound();
            }

            var potion = await _potionService.GetPotion(id);
            if (potion == null)
            {
                return NotFound();
            }

            ViewBag.Ingredients = potion.Ingredients;
            return View(potion);
        }

        // GET: Potions/Create
        public IActionResult Create()
        {
            ViewBag.Ingredients = _ingredientService.GetAllIngredients().Result;
            //ViewBag.Ingredients = new MultiSelectList(_context.Ingredients.ToList(), "Name", "Name");
            ViewBag.Username = HttpContext.Session.GetString("username")?.Replace("\"", "");
            return View();
        }

        // POST: Potions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ingredients")] IngredientListView ingredientList)
        {
            var username = HttpContext.Session.GetString("username")?.Replace("\"", "");
            var student = _studentService.GetStudentByUsername(username).Result;
            var potion = new Potion()
            {
                Ingredients = _ingredientService.GetIngredientlistByName(ingredientList.Ingredients),
                Recipe = new Recipe()
                {
                    Ingredients = _ingredientService.GetIngredientlistByName(ingredientList.Ingredients),
                    Student = _studentService.GetStudentByUsername(username).Result
                }
            };
            
            if (ModelState.IsValid)
            {
                await _potionService.CreateAPotion(potion, student);
                return RedirectToAction(nameof(Index));
            }
            return View(ingredientList);
        }

        // GET: Potions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || await _potionService.GetAllPotions() == null)
            {
                return NotFound();
            }

            var potion = await _potionService.GetPotion(id);
            if (potion == null)
            {
                return NotFound();
            }
            return View(potion);
        }

        // POST: Potions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,BrewingStatus")] Potion potion)
        {
            if (id != potion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                { 
                    _potionService.Update(potion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PotionExists(potion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(potion);
        }

        // GET: Potions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || await _potionService.GetAllPotions() == null)
            {
                return NotFound();
            }

            var potion = await _potionService.GetPotion(id);
            if (potion == null)
            {
                return NotFound();
            }

            return View(potion);
        }

        // POST: Potions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (await _potionService.GetAllPotions() == null)
            {
                return Problem("Entity set 'HogwartsContext.Potions'  is null.");
            }
            var potion = await _potionService.GetPotion(id);
            if (potion != null)
            {
                await _potionService.DeletePotion(potion.Id);
            }

            _potionService.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PotionExists(long id)
        {
          return _potionService.GetAllPotions().Result.Any(p => p.Id == id);
        }
    }
}
