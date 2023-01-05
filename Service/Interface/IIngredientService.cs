using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Service.Interface;

public interface IIngredientService
{
    Task AddIngredient(Ingredient ingredient);
    Task<Ingredient> GetIngredient(long ingredientId);
    Task<List<Ingredient>> GetAllIngredients();
    Task UpdateIngredient(Ingredient ingredient);
    Task DeleteIngredient(long id);
    List<Ingredient> GetIngredientlistByName(List<string> potionIngredients);
}