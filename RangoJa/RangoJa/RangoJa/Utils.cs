using RangoJaDatabaseAccess.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RangoJa
{
    public class Utils
    {
        public static async void DisplayDialog(string title, string msg)
            => await App.Current.MainPage.DisplayAlert(title, msg, "OK");

        public static List<Ingredient> AllIngridients { get; set; }
        public static List<Unit> AllUnits { get; set; }
        public static List<ApplianceType> AllAppliances { get; set; }
        public static List<RecipeType> AllRecipeTypes { get; set; }
    }
}
