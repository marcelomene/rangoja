﻿using System;
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

        public Ingredient() { }

        public Ingredient(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
