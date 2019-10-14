using RangoJaDatabaseAccess.DbModels;
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
        public ICommand ReturnPageCommand { get; set; }

        public string SearchQuery { get; set; }

        private ObservableCollection<Ingredient> ingredientsToSearch;
        public ObservableCollection<Ingredient> IngredientsToSearch
        {   get => ingredientsToSearch;
            set
            {
                ingredientsToSearch = value;
                OnPropertyChanged(nameof(IngredientsToSearch));
            }
        }

        public SearchRecipePageViewModel()
        {
            ReturnPageCommand = new Command(
                execute: () => NavigationProvider.NavigateBack(),
                canExecute: () => true);
            IngredientsToSearch = new ObservableCollection<Ingredient>();
        }
    }
}
