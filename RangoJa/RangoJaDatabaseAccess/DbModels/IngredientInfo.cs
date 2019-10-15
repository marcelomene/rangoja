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
    }
}
