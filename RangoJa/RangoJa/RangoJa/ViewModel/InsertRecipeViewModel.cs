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
    public class InsertRecipeViewModel : BaseViewModel
    {
        public string SearchQuery { get; set; }
        public ICommand InsertRecipeCommand { get; set; }
        public Ingredient SelectedIngredient { get; set; }
        public List<Ingredient> AllIngredients { get; set; }
        public List<Unit> AllUnits { get; set; }
        public List<ApplianceType> AllAppliances { get; set; }
        public List<RecipeType> AllRecipeTypes { get; set; }

        #region Recipe

        public Recipe RecipeToAdd { get; set; }
        public string Amount { get; set; }
        public string UnitType { get; set; }
        public bool IsStove { get; set; }
        public bool IsMicrowave { get; set; }
        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }
        private ObservableCollection<IngredientInfo> ingredientsToSearch;
        public ObservableCollection<IngredientInfo> IngredientsToSearch
        {
            get => ingredientsToSearch;
            set
            {
                ingredientsToSearch = value;
                OnPropertyChanged(nameof(IngredientsToSearch));
            }
        }
        #endregion

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

        public InsertRecipeViewModel()
        {
            IngredientsToSearch = new ObservableCollection<IngredientInfo>();
            ingredientsFound = new ObservableCollection<Ingredient>();
            InsertRecipeCommand = new Command(() => InsertRecipe(), () => true);
            RecipeToAdd = new Recipe();
            LoadAllIngredients();
            LoadAllUnits();
            LoadAllApliances();
            LoadAllRecipeTypes();
        }

        public void IncludeInSearch()
        {
            IngredientInfo ingInfo = new IngredientInfo();
            ingInfo.Ingredient = SelectedIngredient;
            ingInfo.Unit = AllUnits.FirstOrDefault(x => x.Name.Contains(UnitType));
            ingInfo.Amount = Amount;
            IngredientsToSearch.Add(ingInfo);
            SearchQuery = string.Empty;
        }

        public void LoadAllIngredients()
        {
            IsLoading = true;
            AllIngredients = MySQLDbAccess.GetAllIngredients();
            IsLoading = false;
        }

        public void LoadAllUnits()
        {
            IsLoading = true;
            AllUnits = MySQLDbAccess.GetAllUnits();
            IsLoading = false;
        }

        public void LoadAllApliances()
        {
            IsLoading = true;
            AllAppliances = MySQLDbAccess.GetAllApplianceTypes();
            IsLoading = false;
        }

        public void LoadAllRecipeTypes()
        {
            IsLoading = true;
            AllRecipeTypes = MySQLDbAccess.GetAllRecipeTypes();
            IsLoading = false;
        }

        public bool CanInsertRecipe()
            => (!string.IsNullOrEmpty(RecipeToAdd.Name)
            && !string.IsNullOrEmpty(RecipeToAdd.PreparationMode)
            && !string.IsNullOrEmpty(RecipeToAdd.PreparationTime)
            && !string.IsNullOrEmpty(RecipeToAdd.Portion)
            && !string.IsNullOrEmpty(Amount)
            && !string.IsNullOrEmpty(UnitType)
            && IngredientsToSearch.Any());

        public void InsertRecipe()
        { //GOOOOOOOOOO

            if(CanInsertRecipe())
            {
                if (IsStove)
                    RecipeToAdd.Appliance = AllAppliances.FirstOrDefault(x => x.Id == 7);
                if (IsMicrowave)
                    RecipeToAdd.Appliance = AllAppliances.FirstOrDefault(x => x.Id == 9);
                if (IsVegetarian)
                    RecipeToAdd.RecipeType = AllRecipeTypes.FirstOrDefault(x => x.Id == 3);
                if (IsVegan)
                    RecipeToAdd.RecipeType = AllRecipeTypes.FirstOrDefault(x => x.Id == 2);
                RecipeToAdd.Ingredients = IngredientsToSearch.ToList();
            }
            MySQLDbAccess.InsertRecipe(RecipeToAdd);
            NavigationProvider.NavigateBack();
        }
    }
}