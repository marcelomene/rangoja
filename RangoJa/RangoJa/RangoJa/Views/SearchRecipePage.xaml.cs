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

        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool include = await DisplayAlert("Aviso", "Incluir esta receita na busca?", "Sim", "Não");
            if (include)
                ViewModel.IncludeInSearch();
        }
    }
}