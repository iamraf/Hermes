using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
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

            ObjectCache Cache = MemoryCache.Default;
            User user = (User)Cache["User"];
            if (user != null)
            {
                txtblockNotLoggedIn.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtblockNotLoggedIn.Visibility = Visibility.Visible;
            }
        }

        //Constructor to show Help overlay after registering
        public HomeView(bool showoverlay)
        {
            InitializeComponent();

            if (showoverlay)
            {
                btnNoAction.Visibility = Visibility.Visible;
                btnCloseOverlay.Visibility = Visibility.Visible;
                rectangleOverlay.Visibility = Visibility.Visible;
                imgOverlay.Visibility = Visibility.Visible;
            } else
            {
                btnNoAction.Visibility = Visibility.Collapsed;
                btnCloseOverlay.Visibility = Visibility.Collapsed;
                rectangleOverlay.Visibility = Visibility.Collapsed;
                imgOverlay.Visibility = Visibility.Collapsed;
            }
            
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

        private void btnCloseOverlay_Click(object sender, RoutedEventArgs e)
        {
            btnNoAction.Visibility = Visibility.Collapsed;
            btnCloseOverlay.Visibility = Visibility.Collapsed;
            rectangleOverlay.Visibility = Visibility.Collapsed;
            imgOverlay.Visibility = Visibility.Collapsed;
        }
    }
}
