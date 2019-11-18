using RangoJa.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RangoJa.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand GoToSearchRecipeCommand { get; set; }

        public ICommand GoToIngredientsCommand { get; set; }

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
        
        public MainPageViewModel()
        {
            GoToSearchRecipeCommand = new Command(() => NavigationProvider.NavigateTo(new SearchRecipePage()));
            GoToIngredientsCommand = new Command(() => NavigationProvider.NavigateTo(new InsertRecipePage()));
        }
    }
}
