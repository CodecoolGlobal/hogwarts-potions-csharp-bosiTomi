using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HogwartsPotions.Models.Entities;

public class Ingredient
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }
    public string name { get; set; }
    [JsonIgnore]
    public HashSet<Potion> Potions { get; set; } = new();
    [JsonIgnore]
    public HashSet<Recipe> Recipes { get; set; } = new();
}