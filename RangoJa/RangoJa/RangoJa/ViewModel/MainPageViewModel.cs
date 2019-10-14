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

        public MainPageViewModel()
        {
            GoToSearchRecipeCommand = new Command(
                execute: () => NavigationProvider.NavigateTo(new SearchRecipePage()), 
                canExecute: () => true);
        }
    }
}
