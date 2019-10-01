using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangoJaDatabaseAccess.DbModels
{
    public class Ingredient
    {
        public virtual int Id {get; set;}
        public virtual string Name { get; set; }
        public virtual int Amount { get; set; }

        public Ingredient() { }

        public Ingredient(int id, string name, int amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }
    }
}
