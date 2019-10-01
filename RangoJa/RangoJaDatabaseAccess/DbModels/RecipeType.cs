﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangoJaDatabaseAccess.DbModels
{
    public class RecipeType
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        
        public RecipeType() { }

        public RecipeType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
