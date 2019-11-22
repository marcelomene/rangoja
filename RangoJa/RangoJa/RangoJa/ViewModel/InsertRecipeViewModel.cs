using RangoJa.Views;
using RangoJaDatabaseAccess.DbModels;
using RangoJaDatabaseAccess.MySQL;
using RangoJaDatabaseAccess.NHibernate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RangoJa.ViewModel
{
    public class InsertRecipeViewModel : BaseViewModel
    {
        public string SearchQuery { get; set; }
        public ICommand InsertRecipeCommand { get; set; }
        public Ingredient SelectedIngredient { get; set; }

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
        }

        public void IncludeInSearch()
        {
            if (Amount == null || UnitType == null)
                Utils.DisplayDialog("Erro", "Quantidade e tipo de unidade deste ingrediente não pode ficar em branco");
            else
            {
                IngredientInfo ingInfo = new IngredientInfo();
                ingInfo.Ingredient = SelectedIngredient;
                ingInfo.Unit = Utils.AllUnits.FirstOrDefault(x => x.Name.Contains(UnitType));
                ingInfo.Amount = Amount;
                IngredientsToSearch.Add(ingInfo);
                SearchQuery = string.Empty;
            }
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

            try
            {
                if (CanInsertRecipe())
                {
                    if (IsStove)
                        RecipeToAdd.Appliance = Utils.AllAppliances.FirstOrDefault(x => x.Id == 7);
                    if (IsMicrowave)
                        RecipeToAdd.Appliance = Utils.AllAppliances.FirstOrDefault(x => x.Id == 9);
                    if (IsVegetarian)
                        RecipeToAdd.RecipeType = Utils.AllRecipeTypes.FirstOrDefault(x => x.Id == 3);
                    if (IsVegan)
                        RecipeToAdd.RecipeType = Utils.AllRecipeTypes.FirstOrDefault(x => x.Id == 2);
                    RecipeToAdd.Ingredients = IngredientsToSearch.ToList();
                    MySQLDbAccess.InsertRecipe(RecipeToAdd);
                    Utils.DisplayDialog("Sucesso!", "Receita incluída com sucesso!");
                    NavigationProvider.NavigateBack();
                }
                else
                    Utils.DisplayDialog("Aviso", "Verifique se os campos foram preenchidos corretamente.");
            }
            catch(Exception e)
            {
#if DEBUG
                Utils.DisplayDialog(e.Source, e.Message);

#else
                Utils.DisplayDialog("Erro", "Ocorreu um erro ao tentar inserir esta receita.");
#endif
            }
        }
    }
}