using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;


namespace HogwartsPotions.Models

{
    public static class DbInitializer
    {
        public static async Task Initialize(HogwartsContext context)
        {
            await context.Database.EnsureCreatedAsync();

            var potions = new Potion[]
            {
                new Potion{name = "pot1", Student = new Student(), Ingredients = new List<Ingredient>
                {
                    new Ingredient() {name = "catnip"},
                    new Ingredient() {name = "randy"},
                    new Ingredient() {name = "marsh"},
                    new Ingredient() {name = "jayz"},
                    new Ingredient() {name = "bruh"}
                }, Recipe = new Recipe(){Ingredients = new List<Ingredient>
                {
                    new Ingredient() {name = "catnip"},
                    new Ingredient() {name = "fra"},
                    new Ingredient() {name = "er"},
                    new Ingredient() {name = "et"},
                    new Ingredient() {name = "bruh"}
                }, name="broncoRecipe", Student = new Student()}}
            };
            foreach (var potion in potions)
            {
                await context.SetBrewingStatus(potion);
                context.Potions.Add(potion);
            }

            await context.SaveChangesAsync();
            var recipes = new Recipe[]
            {
                new Recipe()
                {
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient() {name = "asd"},
                        new Ingredient() {name = "fra"},
                        new Ingredient() {name = "er"},
                        new Ingredient() {name = "et"},
                        new Ingredient() {name = "bruh"}
                    },
                    name = "broncoRecipe", Student = new Student()
                },
                new Recipe()
                {
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient() {name = "ok"},
                        new Ingredient() {name = "okay"},
                        new Ingredient() {name = "work"},
                        new Ingredient() {name = "please"},
                        new Ingredient() {name = "randy"}
                    },
                    name = "broncoRecipe", Student = new Student()
                }
            };
            foreach (var recipe in recipes)
            {
                
                context.Recipes.Add(recipe);
            }

            await context.SaveChangesAsync();

            var students = new Student()
            {
                HouseType = HouseType.Gryffindor, Name = "Draco", PetType = PetType.None, Room = new Room()
            };
            context.Students.Add(students);
            await context.SaveChangesAsync();
        }
    }
}
