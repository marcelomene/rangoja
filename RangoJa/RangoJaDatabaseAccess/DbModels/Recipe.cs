using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangoJaDatabaseAccess.DbModels
{
    public class Recipe
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int CookedCounter { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual User User { get; set; }
        public virtual string PreparationMode { get; set; }
        public virtual RecipeType RecipeType { get; set; }

        public Recipe(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Recipe(int id, string name, int cookedCounter, List<Ingredient> ingredients)
        {
            Id = id;
            Name = name;
            CookedCounter = cookedCounter;
            Ingredients = ingredients;
        }
    }
}
