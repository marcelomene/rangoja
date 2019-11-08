using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangoJaDatabaseAccess.DbModels
{
    public class IngredientInfo
    {
        public Ingredient Ingredient {get; set;}
        public Unit Unit { get; set;}
        public string Amount { get; set; }

        public IngredientInfo() { }

        public IngredientInfo(Ingredient ingredient, Unit unit, string amount)
        {
            Ingredient = ingredient;
            Unit = unit;
            Amount = amount;
        }
    }
}
