using System.Collections.Generic;
using System.Linq;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.data
{
    public static class DbInitializer
    {
        public static void Initialize(HogwartsContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var rooms = new Room[]
            {
                new Room{Capacity = 5},
                new Room{Capacity = 5}
            };

            foreach (Room r in rooms)
            {
                context.Rooms.Add(r);
            }
            context.SaveChanges();
            var students = new Student[]
            {
                new Student{Name="Carson Alexander",HouseType = HouseType.Gryffindor, PetType = PetType.Cat, Room = rooms[0]},
                new Student{Name="Meredith Alonso",HouseType = HouseType.Gryffindor, PetType = PetType.Owl, Room = rooms[0]},
                new Student{Name="Arturo Anand",HouseType = HouseType.Gryffindor, PetType = PetType.Rat, Room = rooms[0]},
                new Student{Name="Gytis Barzdukas",HouseType = HouseType.Gryffindor, PetType = PetType.Cat, Room = rooms[0]},
                new Student{Name="Yan Li",HouseType = HouseType.Gryffindor, PetType = PetType.Rat, Room = rooms[0]},
                new Student{Name="Peggy Justice",HouseType = HouseType.Slytherin, PetType = PetType.Cat, Room = rooms[1]},
                new Student{Name="Laura Alexander",HouseType = HouseType.Slytherin, PetType = PetType.None, Room = rooms[1]},
                new Student{Name="Nino Alexander",HouseType = HouseType.Slytherin, PetType = PetType.Owl, Room = rooms[1]},
                new Student{Name="Arturo Olivetto",HouseType = HouseType.Slytherin, PetType = PetType.Owl, Room = rooms[1]},
                new Student{Name="Carson Norman",HouseType = HouseType.Slytherin, PetType = PetType.Cat, Room = rooms[1]},
            };

            foreach (Student s in students)
            {
                context.Students.Add(s);

            }
            context.SaveChanges();


            var ingredients = new Ingredient[]
{
                new Ingredient {Name = "Abraxan hair"}, new Ingredient {Name = "Acromantula venom"},
                new Ingredient {Name = "Adder's Fork"}, new Ingredient {Name = "African Red Pepper"}, new Ingredient {Name = "African Sea Salt"},
                new Ingredient {Name = "Agrippa"}, new Ingredient {Name = "Alcohol"}, new Ingredient {Name = "Alihotsy"},
                new Ingredient {Name = "Angel's Trumpet"}, new Ingredient {Name = "Anjelica"}, new Ingredient {Name = "Antimony"},
                new Ingredient {Name = "Armadillo bile"}, new Ingredient {Name = "Armotentia"}, new Ingredient {Name = "Arnica"},
                new Ingredient {Name = "Asian Dragon Hair"}, new Ingredient {Name = "Ashwinder egg"}, new Ingredient {Name = "Asphodel"},
                new Ingredient {Name = "Avocado"}, new Ingredient {Name = "Balm"}, new Ingredient {Name = "Banana"},
                new Ingredient {Name = "Baneberry"}, new Ingredient {Name = "Bat spleen"}, new Ingredient {Name = "Bat wing"},
                new Ingredient {Name = "Beetle Eye"}, new Ingredient {Name = "Belladonna"}, new Ingredient {Name = "Betony"},
                new Ingredient {Name = "Bezoar"}, new Ingredient {Name = "Bicorn Horn"}, new Ingredient {Name = "Billywig sting"},
                new Ingredient {Name = "Billywig Sting Slime"}, new Ingredient {Name = "Billywig wings"}, new Ingredient {Name = "Bitter root"},
                new Ingredient {Name = "Blatta Pulvereus"}, new Ingredient {Name = "Blind-worm's Sting"}, new Ingredient {Name = "Blood"},
                new Ingredient {Name = "Bloodroot"}, new Ingredient {Name = "Unicorn blood"}, new Ingredient {Name = "Re'em blood"},
                new Ingredient {Name = "Blowfly"}, new Ingredient {Name = "Bone"}, new Ingredient {Name = "Boom Berry"},
                new Ingredient {Name = "Boomslang"}, new Ingredient {Name = "Boomslang Skin"}, new Ingredient {Name = "Borage"},
                new Ingredient {Name = "Bouncing Bulb"}, new Ingredient {Name = "Bouncing Spider Juice"}, new Ingredient {Name = "Bubotuber pus"},
                new Ingredient {Name = "Bulbadox juice"}, new Ingredient {Name = "Bundimun Secretion"}, new Ingredient {Name = "Bursting mushroom"},
                new Ingredient {Name = "Butterscotch"}, new Ingredient {Name = "Camphirated Spirit"}, new Ingredient {Name = "Castor oil"},
                new Ingredient {Name = "Cat Hair"}, new Ingredient {Name = "Caterpillar"}, new Ingredient {Name = "Centaury"},
                new Ingredient {Name = "Cheese"}, new Ingredient {Name = "Chicken Lips"}, new Ingredient {Name = "Chinese Chomping Cabbage"},
                new Ingredient {Name = "Chizpurfle Carapace"}, new Ingredient {Name = "Chizpurfle fang"}, new Ingredient {Name = "Cinnamon"},
                new Ingredient {Name = "Cockroach"}, new Ingredient {Name = "Corn starch"}, new Ingredient {Name = "Cowbane"},
                new Ingredient {Name = "Crocodile Heart"}, new Ingredient {Name = "Daisy"}, new Ingredient {Name = "Dandelion root"},
                new Ingredient {Name = "Dandruff"}, new Ingredient {Name = "Deadlyius"}, new Ingredient {Name = "Death-Cap"},
                new Ingredient {Name = "Dittany"}, new Ingredient {Name = "Doxy egg"}, new Ingredient {Name = "Dragon blood"},
                new Ingredient {Name = "Dragon claw"}, new Ingredient {Name = "Dragon Claw Ooze"}, new Ingredient {Name = "Dragon dung"},
                new Ingredient {Name = "Dragon horn"}, new Ingredient {Name = "Dragon liver"}, new Ingredient {Name = "Dragonfly thorax"},
                new Ingredient {Name = "Eagle Owl Feather"}, new Ingredient {Name = "Eel eye"}, new Ingredient {Name = "Erumpent horn"},
                new Ingredient {Name = "Erumpent tail"}, new Ingredient {Name = "Essence of comfrey"}, new Ingredient {Name = "Essence of Daisyroot"},
                new Ingredient {Name = "Exploding Fluid"}, new Ingredient {Name = "Exploding Ginger Eyelash"}, new Ingredient {Name = "Eye of Newt"},
                new Ingredient {Name = "Eyeball"}, new Ingredient {Name = "Fairy Wing"}, new Ingredient {Name = "Fanged Geranium"},
                new Ingredient {Name = "Fillet of a Fenny Snake"}, new Ingredient {Name = "Fire"}, new Ingredient {Name = "Firefly"},
                new Ingredient {Name = "Fire Seed"}, new Ingredient {Name = "Flabberghasted Leech"}, new Ingredient {Name = "Flesh"},
                new Ingredient {Name = "Flitterby"}, new Ingredient {Name = "Flobberworm Mucus"}, new Ingredient {Name = "Flower head"},
                new Ingredient {Name = "Fluxweed"}, new Ingredient {Name = "Flying Seahorses"}, new Ingredient {Name = "Foxglove"},
                new Ingredient {Name = "Frog"}, new Ingredient {Name = "Frog brain"}, new Ingredient {Name = "Galanthus Nivalis"},
                new Ingredient {Name = "Giant Purple Toad Wart"}, new Ingredient {Name = "Ginger Root"}, new Ingredient {Name = "Gomas Barbadensis"},
                new Ingredient {Name = "Goosegrass"}, new Ingredient {Name = "Granian hair"}, new Ingredient {Name = "Graphorn horn"},
                new Ingredient {Name = "Gravy"}, new Ingredient {Name = "Griffin Claw"}, new Ingredient {Name = "Gillyweed"},
                new Ingredient {Name = "Gnat Heads"}, new Ingredient {Name = "Gulf"}, new Ingredient {Name = "Gurdyroot"},
                new Ingredient {Name = "Extract of Gurdyroot"}, new Ingredient {Name = "Haliwinkles"}, new Ingredient {Name = "Hellebore"},
                new Ingredient {Name = "Hemlock"}, new Ingredient {Name = "Herbaria"}, new Ingredient {Name = "Hermit crab shell"},
                new Ingredient {Name = "Honey"}, new Ingredient {Name = "Honeywater"}, new Ingredient {Name = "Horklump juice"},
                new Ingredient {Name = "Horned slug"}, new Ingredient {Name = "Horned toad"}, new Ingredient {Name = "Horse hair"},
                new Ingredient {Name = "Horseradish"}, new Ingredient {Name = "Howlet's Wing"}, new Ingredient {Name = "Iguana blood"},
                new Ingredient {Name = "Infusion of Wormwood"},new Ingredient {Name = "Jewelweed"},new Ingredient {Name = "Jobberknoll feather"},
                new Ingredient {Name = "Kelp"},new Ingredient {Name = "Knarl quills"},new Ingredient {Name = "Kneazle hair"},
                new Ingredient {Name = "Knotgrass"},new Ingredient {Name = "Lacewing Fly"},new Ingredient {Name = "Lady's Mantle"},
                new Ingredient {Name = "Lard"},new Ingredient {Name = "Lavender"},new Ingredient {Name = "Leech"},
                new Ingredient {Name = "Leech Juice"},new Ingredient {Name = "Left handed nazle powder"},new Ingredient {Name = "Lethe River Water"},
                new Ingredient {Name = "Lionfish"},new Ingredient {Name = "Lionfish Spine"},new Ingredient {Name = "Liver"},
                new Ingredient {Name = "Lizard's Leg"},new Ingredient {Name = "Lobalug Venom"},new Ingredient {Name = "Lovage"},
                new Ingredient {Name = "Mackled Malaclaw tail"},new Ingredient {Name = "Mallowsweet"},new Ingredient {Name = "Mandrake"},
                new Ingredient {Name = "Mandrake, stewed"},new Ingredient {Name = "Mandrake Root"},new Ingredient {Name = "Maw"},
                new Ingredient {Name = "Mercury and Mars"},new Ingredient {Name = "Mistletoe Berry"},new Ingredient {Name = "Mint"},
                new Ingredient {Name = "Moly"},new Ingredient {Name = "Moondew"},new Ingredient {Name = "Moonseed"},
                new Ingredient {Name = "Moonstone"},new Ingredient {Name = "Morning dew"},new Ingredient {Name = "Motherwort"},
                new Ingredient {Name = "Murtlap tentacle"},new Ingredient {Name = "Mushroom"},new Ingredient {Name = "Nagini's venom"},
                new Ingredient {Name = "Neem oil"},new Ingredient {Name = "Nettle"},new Ingredient {Name = "Newt"},
                new Ingredient {Name = "Newt spleen"},new Ingredient {Name = "Niffler's Fancy"},new Ingredient {Name = "Nightshade"},
                new Ingredient {Name = "Nux Myristica"},new Ingredient {Name = "Occamy egg"},new Ingredient {Name = "Octopus Powder"},
                new Ingredient {Name = "Onion juice"},new Ingredient {Name = "Peacock feather"},new Ingredient {Name = "Pearl Dust"},
                new Ingredient {Name = "Peppermint"},new Ingredient {Name = "Petroleum Jelly"},new Ingredient {Name = "Pickled Slugs"},
                new Ingredient {Name = "Plangentine"},new Ingredient {Name = "Plantain"},new Ingredient {Name = "Poison ivy"},
                new Ingredient {Name = "Polypody"},new Ingredient {Name = "Pomegranate juice"},new Ingredient {Name = "Pond Slime"},
                new Ingredient {Name = "Poppy head"},new Ingredient {Name = "Powder of vipers-flesh"},new Ingredient {Name = "Porcupine quill"},
                new Ingredient {Name = "Pritcher's Porritch"},new Ingredient {Name = "Ptolemy"},new Ingredient {Name = "Puffer-fish"},
                new Ingredient {Name = "Puffer-fish Eyes"},new Ingredient {Name = "Puffskein hair"},new Ingredient {Name = "Pungous Onion"},
                new Ingredient {Name = "Pus"},new Ingredient {Name = "Rat spleen"},new Ingredient {Name = "Rat tail"},
                new Ingredient {Name = "Re'em blood"},new Ingredient {Name = "Rose"},new Ingredient {Name = "Rose Petals"},
                new Ingredient {Name = "Rose thorn"},new Ingredient {Name = "Rose oil"},new Ingredient {Name = "Rotten egg"},
                new Ingredient {Name = "Rue"},new Ingredient {Name = "Runespoor egg"},new Ingredient {Name = "Russian's Dragon Nails"},
                new Ingredient {Name = "Sage"},new Ingredient {Name = "Sal Ammoniac"},new Ingredient {Name = "Salamander blood"},
                new Ingredient {Name = "Salpeter"},new Ingredient {Name = "Salt"},new Ingredient {Name = "Saltpetre"},
                new Ingredient {Name = "Salt water"},new Ingredient {Name = "Sardine"},new Ingredient {Name = "Scale of Dragon"},
                new Ingredient {Name = "Scarab beetle"},new Ingredient {Name = "Scurvy grass"},new Ingredient {Name = "Shrake spine"},
                new Ingredient {Name = "Shrivelfig"},new Ingredient {Name = "Silver"},new Ingredient {Name = "Silverweed"},
                new Ingredient {Name = "Sloth brain"},new Ingredient {Name = "Snake fang"},new Ingredient {Name = "Snakeweed"},
                new Ingredient {Name = "Sneezewort"},new Ingredient {Name = "Sopophorous bean"},new Ingredient {Name = "Sopophorous plant"},
                new Ingredient {Name = "Spiders"},new Ingredient {Name = "Spirit of Myrrh"},new Ingredient {Name = "Spleenwart"},
                new Ingredient {Name = "Squill"},new Ingredient {Name = "Squill bulb"},new Ingredient {Name = "St John's-wort"},
                new Ingredient {Name = "Staghorn"},new Ingredient {Name = "Standard Ingredient"},new Ingredient {Name = "Star Grass"},
                new Ingredient {Name = "Starthistle"},new Ingredient {Name = "Streeler shells"},new Ingredient {Name = "Sulphur Vive"},
                new Ingredient {Name = "Syrup of Arnica"},new Ingredient {Name = "Syrup of Hellebore"},new Ingredient {Name = "Tar"},
                new Ingredient {Name = "Thaumatagoria"},new Ingredient {Name = "Thyme"},new Ingredient {Name = "Tincture of Demiguise"},
                new Ingredient {Name = "Toe of Frog"},new Ingredient {Name = "Tongue of Dog"},new Ingredient {Name = "Tooth of Wolf"},
                new Ingredient {Name = "Tormentil"},new Ingredient {Name = "Tubeworm"},new Ingredient {Name = "Turtle Shell"},
                new Ingredient {Name = "Unicorn Blood"},new Ingredient {Name = "Unicorn Hair"},new Ingredient {Name = "Unicorn Horn"},
                new Ingredient {Name = "Urine"},new Ingredient {Name = "Valerian"},new Ingredient {Name = "Valerian root"},
                new Ingredient {Name = "Venomous Tentacula"},new Ingredient {Name = "Venomous Tentacula leaf"},new Ingredient {Name = "Vervain"},
                new Ingredient {Name = "Vervain infusion"},new Ingredient {Name = "Vinegar"},new Ingredient {Name = "Wartcap powder"},
                new Ingredient {Name = "Wartizome"},new Ingredient {Name = "Water"},new Ingredient {Name = "White spirit"},
                new Ingredient {Name = "White Wine"},new Ingredient {Name = "Wiggenbush"},new Ingredient {Name = "Wiggenbush bark"},
                new Ingredient {Name = "Wiggentree"},new Ingredient {Name = "Wiggentree bark"},new Ingredient {Name = "Witch's Ganglion"},
                new Ingredient {Name = "Witches' Mummy"}, new Ingredient {Name = "Wolfsbane"}, new Ingredient {Name = "Woodlice Extract 63"},
                new Ingredient {Name = "Wood louse"}, new Ingredient {Name = "Wool of Bat"},new Ingredient {Name = "Wormwood"},
                new Ingredient {Name = "Wormwood Essence"}


};

            foreach (Ingredient ingredient in ingredients)
            {
                context.Ingredients.Add(ingredient);

            }
            context.SaveChanges();
            var recipes = new Recipe[]
            {
                new Recipe{Name = "Mageblood Potion's Recipe",Ingredients = new List<Ingredient> { ingredients[0],
                    ingredients[1],
                    ingredients[2],
                    ingredients[3],
                    ingredients[4]}, Student = students[0]},
                new Recipe{Name = "Stoneskin Potion's Recipe",Ingredients = new List<Ingredient> {ingredients[5],
                    ingredients[6],
                    ingredients[7],
                    ingredients[8],
                    ingredients[9]}, Student = students[1]}
            };


            foreach (Recipe recipe in recipes)
            {
                context.Recipes.Add(recipe);

            }
            context.SaveChanges();



            var potions = new Potion[]
            {
                new Potion {Student = students[0], Ingredients = new List<Ingredient> { ingredients[0],
                    ingredients[1],
                    ingredients[2],
                    ingredients[3],
                    ingredients[4]}, BrewingStatus = BrewingStatus.Discovery,Recipe = context.Recipes.ToList()[0] , Name = "Mageblood Potion"},
                new Potion {Student = students[1],Ingredients = new List<Ingredient> { ingredients[0],
                    ingredients[1],
                    ingredients[2],
                    ingredients[3],
                    ingredients[4]}, BrewingStatus = BrewingStatus.Discovery,Recipe = context.Recipes.ToList()[1], Name = "Stoneskin Potion"}
            };

            foreach (Potion potion in potions)
            {
                context.Potions.Add(potion);

            }

            context.SaveChanges();
        }
    }
}