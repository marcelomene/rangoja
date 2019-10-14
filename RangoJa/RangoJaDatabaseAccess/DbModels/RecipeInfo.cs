using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangoJaDatabaseAccess.DbModels
{
    public class RecipeInfo
    {
        public virtual int Id { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual string Amount { get; set; }
    }
}
