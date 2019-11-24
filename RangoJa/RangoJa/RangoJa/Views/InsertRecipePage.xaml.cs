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
    public partial class InsertRecipePage : ContentPage
    {
        InsertRecipeViewModel ViewModel;
        public InsertRecipePage()
        {
            InitializeComponent();
            ViewModel = new InsertRecipeViewModel();
            BindingContext = ViewModel;
            PickerQuantidade.Items.Add("un");
            PickerQuantidade.Items.Add("g");
            PickerQuantidade.Items.Add("kg");
            PickerQuantidade.Items.Add("ml");
            PickerQuantidade.Items.Add("l");
            PickerQuantidade.Items.Add("Colher de chá");
            PickerQuantidade.Items.Add("Colher de sopa");
            PickerQuantidade.Items.Add("a gosto");
            PickerQuantidade.Items.Add("dentes");
            PickerQuantidade.Items.Add("xícara");
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool include = await DisplayAlert("Aviso", "Incluir este ingrediente na receita?", "Sim", "Não");
            if (include)
                ViewModel.IncludeInSearch();
        }

        private void Ingrediente_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewModel.SearchQuery))
            {
                lsView.ItemsSource = Utils.AllIngridients.Where(x => x.Name.ToLower().StartsWith(ViewModel.SearchQuery.ToLower()));
            }
            else
                lsView.ItemsSource = null;
        }
    }
}
