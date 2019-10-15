﻿using MySql.Data.MySqlClient;
using RangoJaDatabaseAccess.DbModels;
using System;
using System.Collections.Generic;
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
            string sqlQuery = $"SELECT * FROM Recipe WHERE IdRecipe = {id}";
            
            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            Recipe recipe = new Recipe() { Id = (int)reader["IdRecipe"],
                Name = (string)reader["Name"], Portion = (string)reader["Portion"],
                PreparationMode = (string)reader["Preparation"], PreparationTime = (string)reader["PreparationTime"],
            };

            int idUser = (int)reader["IdUser"];
            int idRecipeType = (int)reader["IdRecipeType"];

            Connection.Close();

            recipe.User = GetUserById(idUser);
            recipe.RecipeType = GetRecipeTypeById(idRecipeType);
            recipe.Ingredients = GetIngredientInfosFromRecipe(recipe.Id);
            return recipe;
        }

        public static List<int> GetRecipesIdsThatContainsIngredients(List<Ingredient> ingredients)
        {
            List<int> recipeIds = new List<int>();

            string sqlQuery = $"SELECT IdRecipe FROM Recipe_Ingredients WHERE ";

            for(int i = 0; i < ingredients.Count - 1; i++ )
                sqlQuery += $"IdIngredient = {ingredients[i].Id} OR ";
            sqlQuery += $"IdIngredient = {ingredients[ingredients.Count - 1].Id}";

            Connection.Open();

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sqlQuery;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                recipeIds.Add((int)reader["IdRecipe"]);

            Connection.Close();

            return recipeIds;
        }
    }
}