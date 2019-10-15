using RangoJaDatabaseAccess.DbModels;
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

        private ObservableCollection<Ingredient> ingredientsToSearch;
        public ObservableCollection<Ingredient> IngredientsToSearch
        {   get => ingredientsToSearch;
            set
            {
                ingredientsToSearch = value;
                OnPropertyChanged(nameof(IngredientsToSearch));
            }
        }

        public void LoadAllIngredients()
        {
            try
            {
                List<Ingredient> ingredients = new DatabaseOperations().GetAllObjects<Ingredient>() as List<Ingredient>;
            }
            catch (Exception e)
            {

            }
            //foreach (var ingredient in ingredients)
              //  IngredientsToSearch.Add(ingredient);
        }

        public SearchRecipePageViewModel()
        {
            IngredientsToSearch = new ObservableCollection<Ingredient>();
            SearchCommand = new Command(() => LoadAllIngredients(), () => true);
        }
    }
}
