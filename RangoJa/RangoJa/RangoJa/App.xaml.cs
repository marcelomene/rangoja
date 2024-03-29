﻿using RangoJa.Views;
using RangoJaDatabaseAccess.MySQL;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#if !DEBUG
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
#endif
namespace RangoJa
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
#if DEBUG
            HotReloader.Current.Run(this);
#endif
            Utils.AllIngridients = MySQLDbAccess.GetAllIngredients();
            Utils.AllUnits = MySQLDbAccess.GetAllUnits();
            Utils.AllRecipeTypes = MySQLDbAccess.GetAllRecipeTypes();
            Utils.AllAppliances = MySQLDbAccess.GetAllApplianceTypes();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
