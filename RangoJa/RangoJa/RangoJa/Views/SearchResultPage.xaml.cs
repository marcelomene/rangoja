using RangoJa.ViewModel;
using RangoJaDatabaseAccess.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RangoJa.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchResultPage : ContentPage
	{
        SearchResultPageViewModel ViewModel;
		public SearchResultPage ()
		{
			InitializeComponent ();
            ViewModel = new SearchResultPageViewModel();
            BindingContext = ViewModel;
		}

        public SearchResultPage(List<Recipe> recipes)
        {
            InitializeComponent();
            ViewModel = new SearchResultPageViewModel();
            BindingContext = ViewModel;
            ViewModel.RecipesToShow = recipes;
            ViewModel.UpdateResultList();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {           
            NavigationProvider.NavigateTo(new RecipeInfoPage(ViewModel.SelectedRecipe));
        }
    }
}