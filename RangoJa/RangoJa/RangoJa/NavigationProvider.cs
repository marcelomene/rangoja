using System;
using System.Collections.Generic;
using System.Text;
using RangoJa.Views;
using Xamarin.Forms;

namespace RangoJa
{
    public static class NavigationProvider
    {
        public static async void NavigateTo(Page instance)
            => await App.Current.MainPage.Navigation.PushAsync(instance);

        public static async void NavigateBack()
            => await App.Current.MainPage.Navigation.PopAsync();

    
    }
}
