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
    public partial class IngredientsPage : ContentPage
    {
        IngredientsPageViewModel ViewModel;
        public IngredientsPage()
        {
            InitializeComponent();
            ViewModel = new IngredientsPageViewModel();
            BindingContext = ViewModel;
            PickerQuantidade.Items.Add("Unidade");
            PickerQuantidade.Items.Add("Colher");
            PickerQuantidade.Items.Add("Xicara");
            PickerQuantidade.Items.Add("mililitros");
        }



        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool include = await DisplayAlert("Aviso", "Incluir este ingrediente na busca?", "Sim", "Não");
            if (include)
                ViewModel.IncludeInSearch();
        }

        private void PickerQuantidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            var name = PickerQuantidade.Items[PickerQuantidade.SelectedIndex];
            DisplayAlert(name, "Selected value", "Ok");
        }



    }        

      

    }
