using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Caching;
using System.Windows;
using System.Windows.Controls;

namespace Hermes.View.history
{
    public partial class HistoryView : Page, IHistoryView
    {
        private readonly HistoryPresenter _presenter;
        private List<Listing> favorites;

        public HistoryView()
        {
            InitializeComponent();

            _presenter = new HistoryPresenter(this);
            _presenter.GetListings();

            ButtonEnable(false);
        }

        public List<Listing> Listings
        {
            set
            {
                listviewListings.ItemsSource = null;
                listviewListings.ItemsSource = value;
            }
        }

        public List<Listing> Favorites
        {
            set
            {
                favorites = value;
            }
        }

        private void BtnListingSelectedFavorite_Click(object sender, RoutedEventArgs e)
        {
            _presenter.AddToFavourites(((Listing)listviewListings.SelectedItem).Id);
            btnListingSelectedFavorite.IsEnabled = false;
        }

        private void btnProfileMyProfile_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/profile/ProfileView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileHistory_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/history/HistoryView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileFavorites_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/favourites/FavoritesView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileListings_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/mylistings/MyListingsView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void listviewListings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnable(true);
            if (_presenter.GetCurrentUser() != null)
            {
                _presenter.GetFavorites();
                foreach (Listing lis in favorites)
                {
                    if ((listviewListings.SelectedItem as Listing).Id == lis.Id)
                    {
                        btnListingSelectedFavorite.IsEnabled = false;
                    }
                }
            }

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

        private void ButtonEnable(bool action)
        {
            btnListingSelectedFavorite.IsEnabled = action;
            btnListingSelectedContact.IsEnabled = action;
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
            Logout();
        }

        private void Logout()
        {
            ObjectCache Cache = MemoryCache.Default;
            if (Cache["User"] != null)
                Cache.Remove("User");
        }
    }
}
