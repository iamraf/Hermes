using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Caching;
using System.Windows;
using System.Windows.Controls;

namespace Hermes.View.favourites
{
    public partial class FavoritesView : Page, IFavoritesView
    {
        private readonly FavoritesPresenter _presenter;

        public FavoritesView()
        {
            InitializeComponent();

            _presenter = new FavoritesPresenter(this);
            _presenter.GetListings();

            btnListingSelectedContact.IsEnabled = false;
        }

        public List<Listing> Listings
        {
            set
            {
                listviewListings.ItemsSource = null;
                listviewListings.ItemsSource = value;
            }
        }

        private void btnProfileMyProfile_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/profile/ProfilePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileHistory_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/history/HistoryView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileFavorites_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/favorites/FavoritesView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileListings_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/mylistings/MyListingsView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void listviewListings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Listing listing = (Listing)listviewListings.SelectedItem;

            if (listing != null)
            {
                User uploader = _presenter.GetUploader(listing.Id);

                lblListingSelectedTitle.Content = listing.Name;
                tbListingSelectedDescription.Text = listing.Description;

                if (uploader != null)
                {
                    lblListingSelectedUploader.Content = uploader.Name + " " + uploader.Surname;
                    lblListingSelectedContactInfoEmail1.Content = "Telephone: " + uploader.Telephone;
                    lblListingSelectedContactInfoEmail.Content = "Email: " + uploader.Email;
                    btnListingSelectedContact.IsEnabled = true;
                }
                else
                {
                    lblListingSelectedUploader.Content = "-";
                    lblListingSelectedContactInfoEmail1.Content = "Telephone: - ";
                    lblListingSelectedContactInfoEmail.Content = "Email: - ";
                    btnListingSelectedContact.IsEnabled = false;
                }

                _presenter.IncreaseView(listing.Id);
                _presenter.AddToHistory(listing.Id);
            }
        }

        private void btnListingSelectedContact_Click(object sender, RoutedEventArgs e)
        {
            Listing listing = (Listing)listviewListings.SelectedItem;
            User uploader = _presenter.GetUploader(listing.Id);

            if (listing != null && uploader != null)
            {
                var url = "mailto:" + (string)uploader.Email + "?Subject=Interested on this item: " + listing.Name;
                Process.Start(url);
            }
        }

        private void btnProfileSignout_Click(object sender, RoutedEventArgs e)
        {
            _presenter.Logout();
        }

    }
}
