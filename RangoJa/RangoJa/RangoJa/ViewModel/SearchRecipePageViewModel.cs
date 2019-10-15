using RangoJaDatabaseAccess.DbModels;
using RangoJaDatabaseAccess.MySQL;
using RangoJaDatabaseAccess.NHibernate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RangoJa.ViewModel
{
    public class SearchRecipePageViewModel : BaseViewModel
    {
        public string SearchQuery { get; set; }
        public ICommand SearchCommand { get; set; }

        public Ingredient SelectedIngredient { get; set; }

        private ObservableCollection<Ingredient> ingredientsToSearch;
        public ObservableCollection<Ingredient> IngredientsToSearch
        {   get => ingredientsToSearch;
            set
            {
                ingredientsToSearch = value;
                OnPropertyChanged(nameof(IngredientsToSearch));
            }
        }

        private ObservableCollection<Ingredient> ingredientsFound;
        public ObservableCollection<Ingredient> IngredientsFound
        {
            get => ingredientsFound;
            set
            {
                ingredientsFound = value;
                OnPropertyChanged(nameof(IngredientsFound));
            }
        }

        public SearchRecipePageViewModel()
        {
            IngredientsToSearch = new ObservableCollection<Ingredient>();
            ingredientsFound = new ObservableCollection<Ingredient>();
            SearchCommand = new Command(() => SearchIngredient(), () => true);
        }

        public void IncludeInSearch()
        => IngredientsToSearch.Add(SelectedIngredient);

        public void LoadAllIngredients()
        {
            List<Ingredient> ingredients = MySQLDbAccess.GetAllIngredients();
            foreach (var ing in ingredients)
                IngredientsToSearch.Add(ing);
        }

        public void SearchIngredient()
        {
            Ingredient ingredient = MySQLDbAccess.GetIngredientByName(SearchQuery);
            IngredientsFound.Clear();
            IngredientsFound.Add(ingredient);
        }


    }
}
