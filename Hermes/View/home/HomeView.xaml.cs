using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Hermes.View.home
{
    public partial class HomeView : Page, IHomeView
    {
        private readonly HomePresenter _presenter;

        public HomeView()
        {
            InitializeComponent();

            _presenter = new HomePresenter(this);
        }

        public List<Listing> PopularListings
        {
            set
            {
                listviewPopular.ItemsSource = value;
            }
        }

        public List<Listing> RecommendedListings
        {
            set
            {
               listviewRecommended.ItemsSource = value;
            }
        }

        private void btnAllCategories_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/CategoriesPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
