using MySql.Data.MySqlClient;
using RangoJaDatabaseAccess.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangoJaDatabaseAccess.MySQL
{
    public sealed class MySQLDbAccess
    {
        public static readonly string DATABASE = "server=remotemysql.com;User Id=cInQeeudyg;database=cInQeeudyg; password=ExQh72FJcz; SslMode=None";
        public static MySqlConnection Connection;

        static MySQLDbAccess()
        {
            try
            {
                Connection = new MySqlConnection(DATABASE);
            }
            catch(Exception)
            { }
        }

        #region GETS

        public async static Task<List<Ingredient>> GetAllIngredientsAsync()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            string sqlQuery = "SELECT * FROM Ingredients";

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            DbDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                ingredients.Add(new Ingredient() { Id = (int)reader["IdIngredient"], Name = (string)reader["Ingredient"] });
            }
            return ingredients;
        }
        
        public static List<Ingredient> GetAllIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            string sqlQuery = "SELECT * FROM Ingredients";
            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                ingredients.Add(new Ingredient() { Id = (int)reader["IdIngredient"], Name = (string)reader["Ingredient"] });
            }
            Connection.Close();
            return ingredients;
        }

        public static Ingredient GetIngredientByName(string name)
        {
            string sqlQuery = $"SELECT * FROM Ingredients WHERE Ingredients.Ingredient LIKE \"%{name}%\"";
            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            Ingredient ingredient = new Ingredient(){ Id = (int)reader["IdIngredient"], Name = (string)reader["Ingredient"] };
            Connection.Close();
            return ingredient;
        }

        public static Ingredient GetIngredientById(int id)
        {
            string sqlQuery = $"SELECT * FROM Ingredients WHERE IdIngredient = {id}";
            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            Ingredient ingredient = new Ingredient() { Id = (int)reader["IdIngredient"], Name = (string)reader["Ingredient"] };
            Connection.Close();
            return ingredient;
        }

        public static User GetUserById(int id)
        {
            string sqlQuery = $"SELECT * FROM Users WHERE IdUser = {id}";

            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            User user = new User() { Id = (int)reader["IdUser"], Name = (string)reader["Name"], Email = (string)reader["Email"], Password = (string)reader["Password"] };
            Connection.Close();
            return user;
        }

        public static RecipeType GetRecipeTypeById(int id)
        {
            string sqlQuery = $"SELECT * FROM Recipe_Type WHERE IdRecipeType = {id}";

            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            RecipeType recipeType = new RecipeType() { Id = (int)reader["IdRecipeType"], Name = (string)reader["Type"] };
            Connection.Close();
            return recipeType;
        }

        public static Unit GetUnitTypeById(int id)
        {
            string sqlQuery = $"SELECT * FROM Units WHERE IdUnitType = {id}";

            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            Unit recipeType = new Unit() { Id = (int)reader["IdUnitType"], Name = (string)reader["UnitType"] };
            Connection.Close();
            return recipeType;
        }

        public static List<IngredientInfo> GetIngredientInfosFromRecipe (int recipeId)
        {
            string sqlQueryIngredients = $"SELECT * FROM Recipe_Ingredients WHERE IdRecipe = {recipeId}";
            List<IngredientInfo> infos = new List<IngredientInfo>();
            List<int> ingredientsId = new List<int>();
            List<int> unitsId = new List<int>();
            List<string> amounts = new List<string>();

            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQueryIngredients;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ingredientsId.Add((int)reader["IdIngredient"]);
                unitsId.Add((int)reader["IdUnitType"]);
                amounts.Add((string)reader["Amount"]);
            }
            Connection.Close();

            for(int i  = 0; i < ingredientsId.Count; i++)
                infos.Add(new IngredientInfo() { Ingredient = GetIngredientById(ingredientsId[i]), Unit = GetUnitTypeById(unitsId[i]), Amount = amounts[i] });

            return infos;
        }

        public static Recipe GetRecipeById(int id)
        {
            string sqlQuery = "SELECT Recipe.Name, Recipe.Preparation, Recipe.Portion, Recipe.PreparationTime, Recipe.IdRecipeType, " +
                "Recipe_Type.Type, Recipe_Ingredients.IdIngredient, Ingredients.Ingredient, Recipe_Ingredients.Amount, " +
                "Units.IdUnitType, Units.UnitType, Appliance_Type.IdApplianceType, Appliance_Type.ApplianceName FROM Recipe " +
                "INNER JOIN Recipe_Ingredients ON(Recipe_Ingredients.IdRecipe = Recipe.IdRecipe) " +
                "INNER JOIN Units ON(Recipe_Ingredients.IdUnitType = Units.IdUnitType) " +
                "INNER JOIN Ingredients ON(Recipe_Ingredients.IdIngredient = Ingredients.IdIngredient) " +
                "INNER JOIN Appliance_Type ON(Appliance_Type.IdApplianceType = Recipe.IdApplianceType) " +
               $"INNER JOIN Recipe_Type ON(Recipe_Type.IdRecipeType = Recipe.IdRecipeType) WHERE Recipe.IdRecipe = {id}";

            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            Recipe recipe = new Recipe() { Id = id,
                Name = (string)reader["Name"], Portion = (string)reader["Portion"],
                PreparationMode = (string)reader["Preparation"], PreparationTime = (string)reader["PreparationTime"],
                RecipeType = new RecipeType((int)reader["IdRecipeType"], (string)reader["Type"]),
                Appliance = new ApplianceType((int)reader["IdApplianceType"], (string)reader["ApplianceName"])
            };

            recipe.Ingredients.Add(new IngredientInfo(new Ingredient((int)reader["IdIngredient"], (string)reader["Ingredient"]), //Primeiro ingrediente
                    new Unit((int)reader["IdUnitType"], (string)reader["UnitType"]), (string)reader["Amount"]));

            while (reader.Read()) //Restante dos ingredientes
                recipe.Ingredients.Add(new IngredientInfo(new Ingredient((int)reader["IdIngredient"], (string)reader["Ingredient"]),
                    new Unit((int)reader["IdUnitType"], (string)reader["UnitType"]), (string)reader["Amount"]));

            Connection.Close();
            return recipe;
        }

        public static List<int> GetRecipesIdsThatContainsIngredients(List<Ingredient> ingredients)
        {
            List<int> recipeIds = new List<int>();

            string sqlQuery = $"SELECT IdRecipe FROM Recipe_Ingredients";
            string sqlWhere = " WHERE IdIngredient IN";
            string sqlIN = string.Empty;

            for (int i = 0; i < ingredients.Count; i++)
                sqlIN += $"{ingredients[i].Id},";

            sqlQuery += sqlWhere + "(" + sqlIN.Remove(sqlIN.Length - 1, 1) + ")";
            sqlQuery += " group by IdRecipe";

            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                recipeIds.Add((int)reader["IdRecipe"]);

            Connection.Close();

            return recipeIds;
        }

        public async static Task<List<ApplianceType>> GetAllApplianceTypesAsync()
        {
            List<ApplianceType> appliances = new List<ApplianceType>();
            string sqlQuery = "SELECT * FROM Appliance_Type";

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            DbDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
                appliances.Add(new ApplianceType() { Id = (int)reader["IdApplianceType"], Name = (string)reader["ApplianceName"] });

            return appliances;
        }

        public static List<ApplianceType> GetAllApplianceTypes()
        {
            List<ApplianceType> appliances = new List<ApplianceType>();
            string sqlQuery = "SELECT * FROM Appliance_Type";
            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                appliances.Add(new ApplianceType() { Id = (int)reader["IdApplianceType"], Name = (string)reader["ApplianceName"] });

            Connection.Close();
            return appliances;
        }

        public async static Task<List<Unit>> GetAllUnitsAsync()
        {
            List<Unit> units = new List<Unit>();
            string sqlQuery = "SELECT * FROM Units";

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            DbDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
                units.Add(new Unit() { Id = (int)reader["IdUnitType"], Name = (string)reader["UnitType"] });

            return units;
        }

        public static List<Unit> GetAllUnits()
        {
            List<Unit> units = new List<Unit>();
            string sqlQuery = "SELECT * FROM Units";
            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                units.Add(new Unit() { Id = (int)reader["IdUnitType"], Name = (string)reader["UnitType"] });

            Connection.Close();
            return units;
        }

        /// <summary>
        /// Async implemetation of GetAllRecipeTypesAsync(). Must call Connection.Open() first!
        /// </summary>
        /// <returns></returns>
        public async static Task<List<RecipeType>> GetAllRecipeTypesAsync()
        {
            List<RecipeType> recipeTypes = new List<RecipeType>();
            string sqlQuery = "SELECT * FROM Recipe_Type";

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            DbDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
                recipeTypes.Add(new RecipeType() { Id = (int)reader["IdRecipeType"], Name = (string)reader["Type"] });

            return recipeTypes;
        }

        public static List<RecipeType> GetAllRecipeTypes()
        {
            List<RecipeType> recipeTypes = new List<RecipeType>();
            string sqlQuery = "SELECT * FROM Recipe_Type";
            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                recipeTypes.Add(new RecipeType() { Id = (int)reader["IdRecipeType"], Name = (string)reader["Type"] });

            Connection.Close();
            return recipeTypes;
        }

        #endregion

        #region INSERTION
        public static void InsertRecipe(Recipe recipe)
        {
            string _null = "NULL";

            //First, we insert the recipe's info in the Recipe table
            string insertRecipeSQL = $"INSERT INTO Recipe (IdRecipe, IdUser, Name, Preparation, Image, " +
                $"IdRecipeType, Portion, PreparationTime, IdApplianceType) VALUES (" +
                $"NULL, 2, \"{recipe.Name}\", \"{recipe.PreparationMode}\", NULL, {(recipe.RecipeType != null ? recipe.RecipeType.Id.ToString() : _null) }, \"{recipe.Portion}\"," +
                $"\"{recipe.PreparationTime}\", {(recipe.Appliance != null ? recipe.Appliance.Id.ToString() : _null)})";

            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = insertRecipeSQL;
            cmd.ExecuteNonQuery();

            //Now, we need the id from the last
            MySqlCommand cmd2 = Connection.CreateCommand();
            cmd2.CommandText = "SELECT Recipe.IdRecipe FROM Recipe ORDER BY Recipe.IdRecipe DESC LIMIT 1";
            MySqlDataReader reader = cmd2.ExecuteReader();

            reader.Read();
            recipe.Id = (int)reader["IdRecipe"];
            reader.Close();

            //Now, we insert all the ingredients in the Recipe_Ingredients table
            string insertIngredientsSQL = $"INSERT INTO Recipe_Ingredients (IdRecipeIngredients, " +
                $"IdRecipe, IdIngredient, IdUnitType, Amount) VALUES ";

            string rowSql = string.Empty;

            foreach (var ing in recipe.Ingredients)
                rowSql += $"(NULL, {recipe.Id}, {ing.Ingredient.Id}, {ing.Unit.Id}, \"{ing.Amount}\"),";
            rowSql = rowSql.Remove(rowSql.Length - 1, 1);

            insertIngredientsSQL += rowSql;

            MySqlCommand cmd3 = Connection.CreateCommand();
            cmd3.CommandText = insertIngredientsSQL;
            cmd3.ExecuteNonQuery();

            Connection.Close();
        }
        #endregion
    }
}
