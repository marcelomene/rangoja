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
        }

        [TestMethod]
        public void GetAllObjects()
        {
            DatabaseOperations dbOps = new DatabaseOperations();
            List<RecipeInfo> recipes = dbOps.GetAllObjects<RecipeInfo>() as List<RecipeInfo>;
            var query = recipes.GroupBy(x => x.Recipe.Id).ToList();
            List<Recipe> recipesFull = new List<Recipe>();
            
            foreach(var value in query)
            {
                Recipe rec = new Recipe();
                rec.Id = value.ElementAt(0).Id;
            }
        }
    }
}
