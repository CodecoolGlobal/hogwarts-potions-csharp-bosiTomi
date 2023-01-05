using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.DataAccess;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Service;

public class RecipeService : IRecipeService
{
    private readonly HogwartsContext _context;

    public RecipeService(HogwartsContext context)
    {
        _context = context;
    }

    private async Task<Recipe> GetReplica(Potion potion)
    {
        var recipes = await _context.Recipes
            .Include(recipe => recipe.Ingredients)
            .Include(recipe => recipe.Student)
            .Where(recipe => recipe.Student == potion.Student)
            .ToListAsync();

        return recipes.FirstOrDefault(recipe => recipe.Ingredients.Count == potion.Ingredients.Count &&
                                                CheckIngredients(recipe.Ingredients, potion.Ingredients));
    }


    private bool CheckIngredients(List<Ingredient> ingredients1, List<Ingredient> ingredients2)
    {
        return ingredients1.AsEnumerable().Select(ingredient => ingredient.Name)
            .All(ingredients2.Select(ingredient => ingredient.Name).Contains);
    }

    public async Task<Recipe> FindRecipe(Potion potion)
    {
        var findRecipe = await GetReplica(potion);
        var discoveryCount = await DiscoveryCount(potion);
        if (findRecipe != null)
        {
            await SetBrewingStatus(potion);
            potion.Name = $"{potion.Student.Name}'s Replica #{discoveryCount}";
            return findRecipe;
        }
        List<Ingredient> ingredients = new List<Ingredient>();
        potion.Ingredients.ForEach(ingredient =>
        {
            ingredients.Add(new Ingredient { Name = ingredient.Name });
        });
        await SetBrewingStatus(potion);
        Recipe recipe = new Recipe()
        {
            Ingredients = ingredients,
            Student = potion.Student,
            Name = $"{potion.Student.Name}'s Discovery #{discoveryCount}"
        };
        potion.Name = recipe.Name;
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
        return recipe;
    }

    private async Task<int> DiscoveryCount(Potion potion)
    {
        int discoveryCount = await _context.Recipes.Include(recipe => recipe.Student).CountAsync(r => r.Student == potion.Student) + 1;
        return discoveryCount;
    }

    private async Task SetBrewingStatus(Potion potion)
    {
        if (potion.Ingredients.Count < HogwartsContext.MaxIngredientsForPotions)
            potion.BrewingStatus = BrewingStatus.Brew;
        else
        {
            if (await GetReplica(potion) != null)
                potion.BrewingStatus = BrewingStatus.Replica;
            else
                potion.BrewingStatus = BrewingStatus.Discovery;
        }
    }

    public async Task<List<Recipe>> GetAllRecipes()
    {
        return await _context.Recipes.Include(p => p.Ingredients).Include(m => m.Student).ToListAsync();
    }
}