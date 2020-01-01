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

namespace Hermes.View
{
    public partial class ListingsPage : Page
    {
        private ListingRepository _repository;

        public ListingsPage()
        {
            InitializeComponent();

            _repository = new ListingRepository();

            listviewListings.ItemsSource = _repository.GetListings();
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
            }
        }

        private void comboxListingsSortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //
        }
    }
}
