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
        public void InsertRecipeTest()
        {
            Recipe recipe = new Recipe();
            recipe.Name = "Mousse de maracujá";
            recipe.PreparationMode = "Em um liquidificador, bata o creme de leite, o " +
                "leite condensado e o suco concentrado de maracujá." +
                "Em uma tigela, despeje a mistura e leve à geladeira por, no mínimo, 4 horas.";
            recipe.PreparationTime = "5 minutos";
            recipe.RecipeType = new RecipeType(1, "Regular");
            recipe.Portion = "6 porções";
            recipe.Appliance = new ApplianceType(12, "Liquidificador");
            recipe.Ingredients = new List<IngredientInfo>()
            {
                new IngredientInfo(new Ingredient(313, "Suco de maracujá"), new Unit(3, "ml"), "350"),
                new IngredientInfo(new Ingredient(311, "Leite condensado"), new Unit(10, "xícara"), "1"),
                new IngredientInfo(new Ingredient(312, "Creme de leite"), new Unit(10, "xícara"), "1"),
            };
            MySQLDbAccess.InsertRecipe(recipe);
        }

        [TestMethod]
        public void MYSQLDbAccessTests()
        {
            List<ApplianceType> appliances = MySQLDbAccess.GetAllApplianceTypes();
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
