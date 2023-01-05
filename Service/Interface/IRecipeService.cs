using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Service.Interface;

public interface IRecipeService
{
    Task<Recipe> FindRecipe(Potion potion);
    Task<List<Recipe>> GetAllRecipes();
}