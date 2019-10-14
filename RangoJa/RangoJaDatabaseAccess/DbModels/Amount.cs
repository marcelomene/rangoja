using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangoJaDatabaseAccess.DbModels
{
    public class Amount
    {
        public virtual int Id { get; set; }
        public virtual string AmountOfIngredient { get; set; }
    }
}
