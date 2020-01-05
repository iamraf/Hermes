using Hermes.Model.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using Hermes.View.listings;

namespace Hermes.View
{
    public partial class ListingsView : Page, IListingsView
    {
        private readonly ListingsPresenter _presenter;

        public ListingsView()
        {
            InitializeComponent();

            ButtonEnable(false);

            _presenter = new ListingsPresenter(this);
        }

        public List<Listing> Listings
        {
            set 
            {
                listviewListings.ItemsSource = null; // Needed to reset any attached items
                listviewListings.ItemsSource = value;
            }
        }

        public bool Navigate
        {
            set
            {
                if (value)
                {
                    this.NavigationService.Navigate(new LoginPage());
                }
            }
        }

        private void listviewListings_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnable(true);

            Listing listing = (Listing) listviewListings.SelectedItem;

            if(listing != null)
            {
                User uploader = _presenter.GetUploader(listing.Id);

                lblListingSelectedTitle.Content = listing.Name;
                tbListingSelectedDescription.Text = listing.Description;

                if(uploader != null)
                {
                    lblListingSelectedUploader.Content = uploader.Name + " " + uploader.Surname;
                    lblListingSelectedContactInfoEmail1.Content = "Telephone: " + uploader.Telephone;
                    lblListingSelectedContactInfoEmail.Content = "Email: " + uploader.Email;
                }

                _presenter.IncreaseView(listing.Id);
            }
        }

        private void comboxListingsSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(_presenter != null)
            {
                _presenter.SortListing(comboxListingsSortBy.SelectedIndex);
            }
        }

        private void BtnListingSelectedFavorite_Click(object sender, RoutedEventArgs e)
        {
            _presenter.AddToFavourites(((Listing) listviewListings.SelectedItem).Id);
        }

        private void btnListingSelectedContact_Click(object sender, RoutedEventArgs e)
        {
            Listing listing = (Listing)listviewListings.SelectedItem;
            User uploader = _presenter.GetUploader(listing.Id);

            if (listing != null && uploader != null)
            {
                var url = "mailto:" + (string) uploader.Email + "?Subject=Intrested on this item: " + listing.Name;
                Process.Start(url);
            }
        }

        private void ButtonEnable(bool action)
        {
            btnListingSelectedFavorite.IsEnabled = action;
            btnListingSelectedContact.IsEnabled = action;
        }
    }
}