using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.DataAccess;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Service;

public class IngredientService : IIngredientService
{
    private readonly HogwartsContext _context;

    public IngredientService(HogwartsContext context)
    {
        _context = context;
    }

    public async Task AddIngredient(Ingredient ingredient)
    {
        _context.Ingredients.Add(ingredient);
        await _context.SaveChangesAsync();
    }

    public Task<Ingredient> GetIngredient(long ingredientId)
    {
        return _context.Ingredients.FirstOrDefaultAsync(ingredient => ingredient.Id == ingredientId);
    }

    public Task<List<Ingredient>> GetAllIngredients()
    {
        return _context.Ingredients.ToListAsync();
    }

    public async Task UpdateIngredient(Ingredient ingredient)
    {
        _context.Ingredients.Update(ingredient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteIngredient(long id)
    {
        var ingredientToDelete = await GetIngredient(id);
        if (ingredientToDelete != null)
        {
            _context.Ingredients.Remove(ingredientToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public List<Ingredient> GetIngredientlistByName(List<string> potionIngredients)
    {
        List<Ingredient> result = new List<Ingredient>();
        foreach (var ingredient in potionIngredients)
        {
            result.Add(_context.Ingredients.First(i => i.Name == ingredient));
        }

        return result;
    }
}