using Hermes.Model.Models;
using Hermes.View.listings;
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

            if(_presenter.GetCurrentUser() != null)
            {
                txtblockNotLoggedIn.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtblockNotLoggedIn.Visibility = Visibility.Visible;
            }
        }

        public HomeView(bool showoverlay)
        {
            InitializeComponent();

            _presenter = new HomePresenter(this);

            if (showoverlay)
            {
                btnNoAction.Visibility = Visibility.Visible;
                btnCloseOverlay.Visibility = Visibility.Visible;
                rectangleOverlay.Visibility = Visibility.Visible;
                imgOverlay.Visibility = Visibility.Visible;
            } 
            else
            {
                btnNoAction.Visibility = Visibility.Collapsed;
                btnCloseOverlay.Visibility = Visibility.Collapsed;
                rectangleOverlay.Visibility = Visibility.Collapsed;
                imgOverlay.Visibility = Visibility.Collapsed;
            }
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
            this.NavigationService.Navigate(new Uri("View/categories/CategoriesView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnCloseOverlay_Click(object sender, RoutedEventArgs e)
        {
            btnNoAction.Visibility = Visibility.Collapsed;
            btnCloseOverlay.Visibility = Visibility.Collapsed;
            rectangleOverlay.Visibility = Visibility.Collapsed;
            imgOverlay.Visibility = Visibility.Collapsed;
        }

        private void listviewPopular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Listing listing = (Listing)listviewPopular.SelectedItem;

            this.NavigationService.Navigate(new ListingsView(listing.Id));
        }

        private void listviewRecommended_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Listing listing = (Listing)listviewRecommended.SelectedItem;

            this.NavigationService.Navigate(new ListingsView(listing.Id));
        }

        private void btnAllItems_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(false));
        }

        private void btnViewPopular_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(true));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                        this.NavigationService.Navigate(new ListingsView(4,1));

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(4, 2));

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(4, 3));

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(4, 9));

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(4, 6));

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ListingsView(4, 7));

        }
    }
}
