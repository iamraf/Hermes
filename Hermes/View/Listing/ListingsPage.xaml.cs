using Hermes.Model;
using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Caching;

namespace Hermes.View
{
    public partial class ListingsPage : Page
    {
        private ListingRepository _repository;

        private List<Listing> _listings;
        private Favourite _favourite;

        public ListingsPage()
        {
            InitializeComponent();

            _repository = new ListingRepository();

            _listings = _repository.GetListings();

            listviewListings.ItemsSource = _listings;
        }

        private void listviewListings_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Listing listing = (Listing) listviewListings.SelectedItem;

            User uploader = _repository.GetUploader(listing.Id);

            lblListingSelectedTitle.Content = listing.Name;
            tbListingSelectedDescription.Text = listing.Description;

            if(uploader != null)
            {
                lblListingSelectedUploader.Content = uploader.Name + " " + uploader.Surname;
                lblListingSelectedContactInfoEmail1.Content = "Telephone: " + uploader.Telephone;
                lblListingSelectedContactInfoEmail.Content = "Email: " + uploader.Email;
            }
        }

        private void comboxListingsSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int option = comboxListingsSortBy.SelectedIndex;

            if (_listings != null)
            {
                switch (option)
                {
                    case 1:
                        _listings.Sort((a, b) => a.Price.CompareTo(b.Price));
                        listviewListings.ItemsSource = null;
                        listviewListings.ItemsSource = _listings;
                        break;
                    case 2:
                        _listings.Sort((a, b) => a.Views.CompareTo(b.Views));
                        listviewListings.ItemsSource = null;
                        listviewListings.ItemsSource = _listings;
                        break;
                    default:
                        _listings.Sort((a, b) => a.Creation.CompareTo(b.Creation));
                        listviewListings.ItemsSource = null;
                        listviewListings.ItemsSource = _listings;
                        break;
                }
            }
        }

        private void BtnListingSelectedFavorite_Click(object sender, RoutedEventArgs e)
        {
            //TODO DELETE
            ObjectCache Cache = MemoryCache.Default;
            User user1 = (User)Cache["User"];
            Console.WriteLine(user1.Name);
        }

        private void btnListingSelectedFavorite_Click(object sender, RoutedEventArgs e)
        {
            //Listing listing = (Listing)listviewListings.SelectedItem;

            //_favourite = new Favourite(listing.Id,);
        }

        private void btnListingSelectedContact_Click(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}
