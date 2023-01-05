using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.DataAccess;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Service;

public class PotionService : IPotionService
{
    private readonly HogwartsContext _context;
    private readonly IRecipeService _recipeService;

    public PotionService(HogwartsContext context, IRecipeService recipeService)
    {
        _context = context;
        _recipeService = recipeService;
    }

    public async Task AddPotion(Potion potion)
    {
        var recipe = await _recipeService.FindRecipe(potion);
        potion.Recipe = recipe;
        _context.Potions.Add(potion);
        await _context.SaveChangesAsync();
    }

    public async Task<Potion> GetPotion(long? potionId)
    {
        return await _context.Potions.Include(potion => potion.Ingredients).FirstOrDefaultAsync(potion => potion.Id == potionId);
    }

    public async Task<List<Potion>> GetAllPotions()
    {
        return await _context.Potions.Include(potion => potion.Ingredients).ToListAsync();
    }

    public async Task UpdatePotion(Potion potion)
    {
        var recipe = await _recipeService.FindRecipe(potion);
        potion.Recipe = recipe;
        _context.Potions.Update(potion);
        await _context.SaveChangesAsync();
    }

    public void Update(Potion potion)
    {
        _context.Update(potion);
        _context.SaveChanges();
    }

    public async void SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeletePotion(long id)
    {
        var potionToDelete = await GetPotion(id);
        if (potionToDelete != null)
        {
            _context.Potions.Remove(potionToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public void RemovePotion(Potion potion)
    {
        _context.Potions.Remove(potion);
        _context.SaveChanges();
    }

    public async Task<Potion> CreateAPotion(Potion potion, Student student)
    {
        potion.Student = student;
        if (potion.Ingredients.Count >= 5)
        {
            potion.Recipe = await _recipeService.FindRecipe(potion);
        }
        else
        {
            potion.Name = $"{potion.Student.Name}'s potion";
        }
        _context.Potions.Add(potion);
        await _context.SaveChangesAsync();
        return potion;
    }
}