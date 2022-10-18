using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace HogwartsPotions.Controllers
{
    public class PotionsController : Controller
    {
        private readonly HogwartsContext _context;

        public PotionsController(HogwartsContext context)
        {
            _context = context;
        }

        // GET: Potions
        public async Task<IActionResult> Index()
        {
              return View(await _context.Potions.ToListAsync());
        }

        // GET: Potions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Potions == null)
            {
                return NotFound();
            }

            var potion = await _context.Potions
                .Include(p => p.Ingredients)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            ViewBag.Ingredients = _context.Ingredients.ToList();
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
            var student = _context.GetStudent(username).Result;
            var potion = new Potion()
            {
                Ingredients = _context.GetIngredientlistByName(ingredientList.Ingredients),
                Recipe = new Recipe()
                {
                    Ingredients = _context.GetIngredientlistByName(ingredientList.Ingredients),
                    Student = _context.GetStudent(username).Result
                }
            };
            
            if (ModelState.IsValid)
            {
                await _context.CreateAPotion(potion, student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredientList);
        }

        // GET: Potions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Potions == null)
            {
                return NotFound();
            }

            var potion = await _context.Potions.FindAsync(id);
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
                    _context.Update(potion);
                    await _context.SaveChangesAsync();
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
            if (id == null || _context.Potions == null)
            {
                return NotFound();
            }

            var potion = await _context.Potions
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.Potions == null)
            {
                return Problem("Entity set 'HogwartsContext.Potions'  is null.");
            }
            var potion = await _context.Potions.FindAsync(id);
            if (potion != null)
            {
                _context.Potions.Remove(potion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PotionExists(long id)
        {
          return _context.Potions.Any(e => e.Id == id);
        }
    }
}
