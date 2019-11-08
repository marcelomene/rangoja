using RangoJa.Views;
using RangoJaDatabaseAccess.DbModels;
using RangoJaDatabaseAccess.MySQL;
using RangoJaDatabaseAccess.NHibernate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        
        public List<Ingredient> AllIngredients { get; set; }

        private bool isLoading { get; set; }
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

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
            SearchCommand = new Command(() => SearchRecipes(), () => true);
            LoadAllIngredients();
        }

        public void IncludeInSearch()
        {
            IngredientsToSearch.Add(SelectedIngredient);
            SearchQuery = string.Empty;
        }

        public void LoadAllIngredients()
        {
            IsLoading = true;
            AllIngredients = MySQLDbAccess.GetAllIngredients();
            IsLoading = false;
        }

        public void SearchRecipes()
        {
            List<Ingredient> listSurrogate = new List<Ingredient>();
            List<Recipe> recipes = new List<Recipe>();

            foreach (var ing in IngredientsToSearch)
                listSurrogate.Add(ing);

            List<int> recipeIds = MySQLDbAccess.GetRecipesIdsThatContainsIngredients(listSurrogate);

            foreach (var id in recipeIds.Distinct())
                recipes.Add(MySQLDbAccess.GetRecipeById(id));

            NavigationProvider.NavigateTo(new SearchResultPage(recipes));
        }
    }
}
