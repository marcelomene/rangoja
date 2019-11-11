using RangoJa.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RangoJa.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand GoToSearchRecipeCommand { get; set; }

        public ICommand GoToIngredientsCommand { get; set; }

        public MainPageViewModel()
        {
            GoToSearchRecipeCommand = new Command(
                execute: () => NavigationProvider.NavigateTo(new SearchRecipePage()),
                canExecute: () => true);

            GoToIngredientsCommand = new Command(
                 execute: () => NavigationProvider.NavigateTo(new IngredientsPage()),
                canExecute: () => true);
        }

      
    }
}
