﻿using RangoJaDatabaseAccess.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RangoJa.ViewModel
{
    public class RecipeInfoPageViewModel : BaseViewModel
    {
        public Recipe CurrentRecipe { get; set; }

        public string IngredientsText { get; set; }

        public RecipeInfoPageViewModel()
        {
            CurrentRecipe = new Recipe();
        }

        public void UpdateText()
        {
            foreach(var ing in CurrentRecipe.Ingredients)
            {
                if(ing.Unit.Name.Contains("gosto"))
                    IngredientsText += $"{ing.Ingredient.Name} a gosto.\n";
                else
                    IngredientsText += $"{ing.Amount}{ing.Unit.Name} {ing.Ingredient.Name}\n";
            }
            OnPropertyChanged(nameof(IngredientsText));
            OnPropertyChanged(nameof(CurrentRecipe));
        }
    }
}
