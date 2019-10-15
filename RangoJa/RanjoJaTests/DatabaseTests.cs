using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using RangoJaDatabaseAccess.DbModels;
using RangoJaDatabaseAccess.NHibernate;

namespace RanjoJaTests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void SaveObjectTest()
        {
            User user = new User("Marcelo", "mm@mm.com", "123456");
            new DatabaseOperations().SaveObject(user);
        }

        [TestMethod]
        public void GetObject()
        {
            DatabaseOperations dbOps = new DatabaseOperations();
            Recipe recipe = dbOps.GetObject<Recipe>(1);

            User creator = recipe.User;
            GetAllObjects();
        }

        [TestMethod]
        public void GetAllObjects()
        {
            DatabaseOperations dbOps = new DatabaseOperations();
            List<Ingredient> recipes = dbOps.GetAllObjects<Ingredient>() as List<Ingredient>;
        }
    }
}
