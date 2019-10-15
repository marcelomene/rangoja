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
        public virtual User User { get; set; }
        public virtual string PreparationMode { get; set; }
        public virtual RecipeType RecipeType { get; set; }
        public virtual string Portion { get; set; }
        public virtual string PreparationTime { get; set; }
        public Recipe() { }
    }
}
