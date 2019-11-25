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
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public RecipeType RecipeTypeFilter { get; set; }

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
        }

        public void IncludeInSearch()
        {
            IngredientsToSearch.Add(SelectedIngredient);
            SearchQuery = string.Empty;
        }

        public void SearchRecipes()
        {
            IsLoading = true;

            List<Ingredient> listSurrogate = new List<Ingredient>();
            List<Recipe> recipes = new List<Recipe>();

            foreach (var ing in IngredientsToSearch)
                listSurrogate.Add(ing);

            if (IsVegan)
                RecipeTypeFilter = Utils.AllRecipeTypes.FirstOrDefault(x => x.Id == 2);
            else if (IsVegetarian)
                RecipeTypeFilter = Utils.AllRecipeTypes.FirstOrDefault(x => x.Id == 3);
            else
                RecipeTypeFilter = null;

            List<int> recipeIds = MySQLDbAccess.GetRecipesIdsThatContainsIngredients(listSurrogate, RecipeTypeFilter);

            if (recipeIds.Any())
            {
                foreach (var id in recipeIds.Distinct())
                    recipes.Add(MySQLDbAccess.GetRecipeById(id));
                OrderRecipes(recipes);

                IsLoading = false;
                NavigationProvider.NavigateTo(new SearchResultPage(recipes));
            }
            else
            {
                Utils.DisplayDialog("Aviso", "Desculpe, não encontramos nada! Talvez será necessário pedir um xis :D");
                NavigationProvider.NavigateBack();
            }
        }

        private void OrderRecipes(List<Recipe> recipes)
        {
            
        }
    }
}
