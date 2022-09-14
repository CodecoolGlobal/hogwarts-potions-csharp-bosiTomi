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
        public static void Initialize(HogwartsContext context)
        {
            context.Database.EnsureCreated();
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var potions = new Potion[]
            {
                new Potion{Name = "Felix Felicis", Student = new Student(), Ingredients = new List<Ingredient>
                {
                    new Ingredient() {Name = "Mandrake"},
                    new Ingredient() {Name = "Unicorn blood"},
                    new Ingredient() {Name = "Dittany"},
                    new Ingredient() {Name = "Gillyweed"},
                    new Ingredient() {Name = "Bezoar"}
                }, Recipe = new Recipe(){Ingredients = new List<Ingredient>
                {
                    new Ingredient() {Name = "Mandrake"},
                    new Ingredient() {Name = "Unicorn blood"},
                    new Ingredient() {Name = "Dittany"},
                    new Ingredient() {Name = "Gillyweed"},
                    new Ingredient() {Name = "Bezoar"}
                }, Name="Recipe1", Student = new Student()}},

                new Potion{Name = "Polyjuice Potion", Student = new Student(), Ingredients = new List<Ingredient>
                {
                    new Ingredient() {Name = "Chizpurfle fang"},
                    new Ingredient() {Name = "Lavender"},
                    new Ingredient() {Name = "Leech"},
                    new Ingredient() {Name = "Deadlyius"},
                    new Ingredient() {Name = "Mackled Malaclaw tail"}
                }, Recipe = new Recipe(){Ingredients = new List<Ingredient>
                {
                    new Ingredient() {Name = "Chizpurfle fang"},
                    new Ingredient() {Name = "Lavender"},
                    new Ingredient() {Name = "Leech"},
                    new Ingredient() {Name = "Deadlyius"},
                    new Ingredient() {Name = "Mackled Malaclaw tail"}
                }, Name="Recipe2", Student = new Student()}}
            };
            foreach (var potion in potions)
            {
                context.SetBrewingStatus(potion);
                context.Potions.Add(potion);
            }

            var recipes = new Recipe[]
            {
                new Recipe()
                {
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient() {Name = "Cinnamon"},
                        new Ingredient() {Name = "Borage"},
                        new Ingredient() {Name = "Centaury"},
                        new Ingredient() {Name = "Bloodroot"},
                        new Ingredient() {Name = "Banana"}
                    },
                    Name = "Recipe3", Student = new Student()
                },
                new Recipe()
                {
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient() {Name = "Agrippa"},
                        new Ingredient() {Name = "Anjelica"},
                        new Ingredient() {Name = "Arnica"},
                        new Ingredient() {Name = "Armadillo bile"},
                        new Ingredient() {Name = "Betony"}
                    },
                    Name = "Recipe4", Student = new Student()
                }
            };
            foreach (var recipe in recipes)
            {
                
                context.Recipes.Add(recipe);
            }

            var ingredients = new Ingredient[]
            {
                new Ingredient {Name = "Abraxan hair"}, new Ingredient {Name = "Wolfsbane"},
                new Ingredient {Name = "Acromantula venom"}, new Ingredient {Name = "Adder's Fork"},
                new Ingredient {Name = "African Red Pepper"}, new Ingredient {Name = "African Sea Salt"},
                new Ingredient {Name = "Agrippa"}, new Ingredient {Name = "Alcohol"},
                new Ingredient {Name = "Alihotsy"}, new Ingredient {Name = "Angel's Trumpet"},
                new Ingredient {Name = "Anjelica"}, new Ingredient {Name = "Antimony"},
                new Ingredient {Name = "Armadillo bile"}, new Ingredient {Name = "Armotentia"},
                new Ingredient {Name = "Arnica"}, new Ingredient {Name = "Asian Dragon Hair"},
                new Ingredient {Name = "Ashwinder egg"}, new Ingredient {Name = "Asphodel"},
                new Ingredient {Name = "Avocado"}, new Ingredient {Name = "Balm"},
                new Ingredient {Name = "Banana"}, new Ingredient {Name = "Baneberry"},
                new Ingredient {Name = "Bat spleen"}, new Ingredient {Name = "Bat wing"},
                new Ingredient {Name = "Beetle Eye"}, new Ingredient {Name = "Belladonna"},
                new Ingredient {Name = "Betony"}, new Ingredient {Name = "Bezoar"},
                new Ingredient {Name = "Bicorn Horn"}, new Ingredient {Name = "Billywig sting"},
                new Ingredient {Name = "Billywig Sting Slime"}, new Ingredient {Name = "Billywig wings"},
                new Ingredient {Name = "Bitter root"}, new Ingredient {Name = "Blatta Pulvereus"},
                new Ingredient {Name = "Blind-worm's Sting"}, new Ingredient {Name = "Blood"},
                new Ingredient {Name = "Bloodroot"}, new Ingredient {Name = "Unicorn blood"},
                new Ingredient {Name = "Re'em blood"}, new Ingredient {Name = "Blowfly"},
                new Ingredient {Name = "Bone"}, new Ingredient {Name = "Boom Berry"},
                new Ingredient {Name = "Boomslang"}, new Ingredient {Name = "Boomslang Skin"},
                new Ingredient {Name = "Borage"}, new Ingredient {Name = "Bouncing Bulb"},
                new Ingredient {Name = "Bouncing Spider Juice"}, new Ingredient {Name = "Bubotuber pus"},
                new Ingredient {Name = "Bulbadox juice"}, new Ingredient {Name = "Bundimun Secretion"},
                new Ingredient {Name = "Bursting mushroom"}, new Ingredient {Name = "Butterscotch"},
                new Ingredient {Name = "Camphirated Spirit"}, new Ingredient {Name = "Castor oil"},
                new Ingredient {Name = "Cat Hair"}, new Ingredient {Name = "Caterpillar"},
                new Ingredient {Name = "Centaury"}, new Ingredient {Name = "Cheese"},
                new Ingredient {Name = "Chicken Lips"}, new Ingredient {Name = "Chinese Chomping Cabbage"},
                new Ingredient {Name = "Chizpurfle Carapace"}, new Ingredient {Name = "Chizpurfle fang"},
                new Ingredient {Name = "Cinnamon"}, new Ingredient {Name = "Cockroach"},
                new Ingredient {Name = "Corn starch"}, new Ingredient {Name = "Cowbane"},
                new Ingredient {Name = "Crocodile Heart"}, new Ingredient {Name = "Daisy"},
                new Ingredient {Name = "Dandelion root"}

            };
            foreach (Ingredient ingredient in ingredients)
            {
                context.Ingredients.Add(ingredient);

            }
            

            context.SaveChanges();

            var students = new Student[]
            {
                new Student
                {
                    HouseType = HouseType.Gryffindor,
                    Name = "Draco",
                    PetType = PetType.None,
                    Room = new Room()
                },
                new Student
                {
                    HouseType = HouseType.Gryffindor,
                    Name = "Ron",
                    PetType = PetType.Rat,
                    Room = new Room()
                },
                new Student{Name="Carson Alexander",HouseType = HouseType.Gryffindor, PetType = PetType.Cat, Room = new Room()},
                new Student{Name="Meredith Alonso",HouseType = HouseType.Hufflepuff, PetType = PetType.Owl, Room = new Room()},
                new Student{Name="Arturo Anand",HouseType = HouseType.Gryffindor, PetType = PetType.Rat, Room = new Room()},
                new Student{Name="Gytis Barzdukas",HouseType = HouseType.Ravenclaw, PetType = PetType.Cat, Room = new Room()},
                new Student{Name="Yan Li",HouseType = HouseType.Gryffindor, PetType = PetType.Rat, Room = new Room()},
                new Student{Name="Peggy Justice",HouseType = HouseType.Slytherin, PetType = PetType.Cat, Room = new Room()},
                new Student{Name="Laura Alexander",HouseType = HouseType.Hufflepuff, PetType = PetType.None, Room = new Room()},
                new Student{Name="Nino Alexander",HouseType = HouseType.Slytherin, PetType = PetType.Owl, Room = new Room()},
                new Student{Name="Arturo Olivetto",HouseType = HouseType.Ravenclaw, PetType = PetType.Owl, Room = new Room()},
                new Student{Name="Carson Norman",HouseType = HouseType.Slytherin, PetType = PetType.Cat, Room = new Room()},
            };
            foreach (Student student in students)
            {
                context.Students.Add(student);
            }
            context.SaveChanges();
        }
    }
}
