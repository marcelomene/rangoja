using RangoJa.ViewModel;
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
    public partial class SearchRecipePage : ContentPage
    {
        SearchRecipePageViewModel ViewModel;
		public SearchRecipePage ()
		{
			InitializeComponent ();
            ViewModel = new SearchRecipePageViewModel();
            BindingContext = ViewModel;
		}

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewModel.SearchQuery))
            {
                lsView.ItemsSource = Utils.AllIngridients.Where(x => x.Name.ToLower().StartsWith(ViewModel.SearchQuery.ToLower()));
            }
            else
                lsView.ItemsSource = null;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool include = await DisplayAlert("Aviso", "Incluir este ingrediente na busca?", "Sim", "Não");
            if (include)
                ViewModel.IncludeInSearch();
        }
    }
}