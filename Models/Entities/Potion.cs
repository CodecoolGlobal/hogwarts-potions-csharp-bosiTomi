using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HogwartsPotions.Models.Enums;
using Enumerable = System.Linq.Enumerable;

namespace HogwartsPotions.Models.Entities
{
    public class Potion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }
        public Student Student { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public BrewingStatus BrewingStatus { get; set; }
        public Recipe Recipe { get; set; }
    }
}
