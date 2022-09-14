using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models
{
    public class HogwartsContext : DbContext
    {
        public const int MaxIngredientsForPotions = 5;

        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Potion> Potions { get; set; }
        public async Task AddRoom(Room room)
        {
            Rooms.Add(room);
            await SaveChangesAsync();
        }

        public Task<Room> GetRoom(long roomId)
        {
            return Rooms.Include(r => r.Residents).FirstOrDefaultAsync(room => room.ID == roomId);
        }

        public Task<List<Room>> GetAllRooms()
        {
            return Rooms.Include(r => r.Residents).ToListAsync();
        }

        public async Task UpdateRoom(Room room)
        {
            Rooms.Update(room);
            await SaveChangesAsync();
        }

        public async Task DeleteRoom(long id)
        {
            var roomToDelete = await GetRoom(id);
            if (roomToDelete != null)
            {
                Rooms.Remove(roomToDelete);
                await SaveChangesAsync();
            }
        }

        public Task<List<Room>> GetRoomsForRatOwners()
        {
            return Rooms.Include(r => r.Residents)
                .Where(room => room.Residents.Any(student => student.PetType != PetType.Cat || student.PetType != PetType.Owl))
                .ToListAsync();
        }

        public async Task SetBrewingStatus(Potion potion)
        {
            if (potion.Ingredients.Count < MaxIngredientsForPotions)
                potion.BrewingStatus = BrewingStatus.Brew;
            else
            {
                if (await GetReplica(potion) != null)
                    potion.BrewingStatus = BrewingStatus.Replica;
                else
                    potion.BrewingStatus = BrewingStatus.Discovery;
            }
        }

        private bool CheckIngredients(List<Ingredient> ingredients1, List<Ingredient> ingredients2)
        {
            return ingredients1.Select(ingredient => ingredient.Name)
                .All(ingredients2.Select(ingredient => ingredient.Name).Contains);
        }

        private async Task<Recipe> FindRecipe(Potion potion)
        {
            var findRecipe = await GetReplica(potion);
            if (findRecipe != null) return findRecipe;

            var discoveryCount = await DiscoveryCount(potion);

            List<Ingredient> ingredients = new List<Ingredient>();
            potion.Ingredients.ForEach(ingredient =>
            {
                ingredients.Add(new Ingredient{Name = ingredient.Name});
            });
            Recipe recipe = new Recipe()
            {
                Ingredients = ingredients,
                Student = potion.Student,
                Name = $"{potion.Student.Name}'s discovery #{discoveryCount}"
            };
            Recipes.Add(recipe);
            await SaveChangesAsync();
            return recipe;

        }

        private async Task<int> DiscoveryCount(Potion potion)
        {
            int discoveryCount = await Recipes.Include(recipe => recipe.Student).CountAsync(r => r.Student == potion.Student) + 1;
            return discoveryCount;
        }

        private async Task<Recipe> GetReplica(Potion potion)
        {
            var recipes = await Recipes.Include(recipe => recipe.Ingredients).ToListAsync();
            foreach (var recipe in recipes)
            {
                if (CheckIngredients(potion.Ingredients, potion.Recipe.Ingredients))
                {
                    return recipe;
                }
            }

            return null;
        }

        public async Task<List<Potion>> GetPotions()
        {
            return await Potions
                .Include(potion => potion.Ingredients)
                .Include(p => p.Student)
                .Include(p => p.Recipe)
                .ThenInclude(r => r.Student)
                .Include(p => p.Recipe)
                .ThenInclude(r => r.Ingredients)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Potion> CreateAPotion(Potion potion, Student student)
        {
            potion.Student = student;
            if (potion.Ingredients.Count >= MaxIngredientsForPotions)
            {
                potion.Recipe = await FindRecipe(potion);
                potion.Name = potion.Recipe.Name;
            }
            else
            {
                potion.Name = $"{potion.Student.Name}'s potion";
            }
            await (_ = SetBrewingStatus(potion));
            Potions.Add(potion);
            await SaveChangesAsync();
            return potion;
        }

        public async Task<Student> GetStudent(string username)
        {
            return await Students.FirstOrDefaultAsync(student => student.Name == username);
        }

        public async Task<List<Potion>> GetPotionsOfAStudent(Student student)
        {
            return await Potions.Where(p => p.Student == student).ToListAsync();
        }

        public async Task<Potion> FinishPotion(Potion potion, Student student)
        {
            potion.Student = student;
            potion.BrewingStatus = BrewingStatus.Brew;
            Potions.Add(potion);
            await SaveChangesAsync();
            return potion;
        }

        public async Task<Potion> AddIngredient(Ingredient ingredient, long potionId)
        {
            Potion potion = Potions.Include(potion => potion.Ingredients).Include(poti => poti.Student).First(p => p.Id == potionId);
            if (potion.Ingredients.Count >= MaxIngredientsForPotions)
            {
                await SetBrewingStatus(potion);
                return potion;
            }
            
            Ingredients.Add(ingredient);
            await SaveChangesAsync();
            potion.Ingredients.Add(ingredient);
            await SaveChangesAsync();

            if (potion.Ingredients.Count >= MaxIngredientsForPotions)
            {
                await SetBrewingStatus(potion);
                potion.Recipe = await FindRecipe(potion);
                potion.Name = potion.Recipe.Name + "potion";
                Potions.Update(potion);
                await SaveChangesAsync();
            }
            return potion;
        }

        public async Task<List<Recipe>> GetRecipes(long potionId)
        {
            Potion potion = await Potions.Include(potion => potion.Ingredients)
                .FirstAsync(potion => potion.Id == potionId);
            return Recipes.Include(recipe => recipe.Ingredients)
                .Include(r => r.Student).AsEnumerable()
                .Where(recipe => CheckIngredients(potion.Ingredients, recipe.Ingredients)).ToList();
        }
        public bool ValidateLogin(Student user)
        {
            return Students.Single(u => u.Name == user.Name && u.Password == user.Password).Name == user.Name;
        }
        private bool CheckRegistrationStatus(Student user)
        {
            var u = Students.FirstOrDefault(u => u.Name == user.Name);
            return u == null;
        }

        public bool Register(Student user)
        {
            if (CheckRegistrationStatus(user))
            {
                Students.Add(user);
                SaveChanges();
                return true;
            }

            return false;
        }
    }
}
