using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using RangoJaDatabaseAccess.DbModels;
using RangoJaDatabaseAccess.MySQL;
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

        [TestMethod]
        public void MYSQLTests()
        {
            MySqlConnection mConn = null;
            try
            {
                mConn = new MySqlConnection("server=remotemysql.com;User Id=cInQeeudyg;database=cInQeeudyg; password=ExQh72FJcz");
                mConn.Open();

                MySqlCommand cmd = mConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Recipe";
                MySqlDataReader reader = cmd.ExecuteReader();
                Recipe recipe = new Recipe();
                reader.Read();
                recipe.Name = reader["Name"] as string;
            }
            catch (Exception e)
            {

            }
            finally
            {
                mConn.Close();
            }
        }

        [TestMethod]
        public void MYSQLDbAccessTests()
        {

            Recipe recipe = MySQLDbAccess.GetRecipeById(2);
            List<int> recipesIds = MySQLDbAccess.GetRecipesIdsThatContainsIngredients(new List<Ingredient>() { new Ingredient() { Id = 24, Name = "Banana" } });


            List<Ingredient> ingredients = MySQLDbAccess.GetAllIngredients();
            Ingredient ingredient = MySQLDbAccess.GetIngredientById(111);
            Ingredient ingredient2 = MySQLDbAccess.GetIngredientByName("Limão");
            User user = MySQLDbAccess.GetUserById(1);
            RecipeType recipeType = MySQLDbAccess.GetRecipeTypeById(1);
            Unit unit = MySQLDbAccess.GetUnitTypeById(1);
            List<IngredientInfo> infos = MySQLDbAccess.GetIngredientInfosFromRecipe(1);
        }
    }
}
