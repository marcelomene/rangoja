using RangoJa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RangoJa
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel ViewModel;
        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainPageViewModel();
            BindingContext = ViewModel;
        }
    }
}
