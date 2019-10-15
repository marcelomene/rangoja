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
	public partial class RecipeInfoPage : ContentPage
	{
        RecipeInfoPageViewModel ViewModel;
		public RecipeInfoPage ()
		{
			InitializeComponent ();
            ViewModel = new RecipeInfoPageViewModel();
            BindingContext = ViewModel;
		}

        public RecipeInfoPage(Recipe recipe)
        {
            InitializeComponent();
            ViewModel = new RecipeInfoPageViewModel();
            BindingContext = ViewModel;
            ViewModel.CurrentRecipe = recipe;
            ViewModel.UpdateText();
        }
    }
}