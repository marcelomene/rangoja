using RangoJaDatabaseAccess.DbModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RangoJa.ViewModel
{
    public class SearchResultPageViewModel : BaseViewModel
    {
        public List<Recipe> RecipesToShow { get; set; }

        private ObservableCollection<Recipe> results;

        public Recipe SelectedRecipe { get; set; }

        public ObservableCollection<Recipe> Results
        {
            get => results;
            set
            {
                results = value;
                OnPropertyChanged(nameof(Results));
            }
        }

        public SearchResultPageViewModel()
        {
            RecipesToShow = new List<Recipe>();
            Results = new ObservableCollection<Recipe>();
        }

        public void UpdateResultList()
        {
            Results.Clear();

            foreach (var rec in RecipesToShow)
                Results.Add(rec);
        }
    }
}
