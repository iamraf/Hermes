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
using Hermes.Model.Models;

namespace Hermes.View
{
    /// <summary>
    /// Interaction logic for MyListingsPage.xaml
    /// </summary>
    public partial class MyListingsPage : Page, IMyListingsPage
    {
        
        private readonly MyListingsPresenter _presenter;

        

        public MyListingsPage()
        {
            InitializeComponent();
            _presenter = new MyListingsPresenter(this);
            //TODO change this with radio button
            _RefreshListings(-1);

            radioboxAllListings.IsChecked = true;
        }

        private void _RefreshListings(int type) 
        {
            _presenter.GetListings(type);
        }

        public List<Listing> Listings {
            set 
            {
                listviewListings.ItemsSource = null; // Needed to reset any attached items
                listviewListings.ItemsSource = value;
            }
                
        }

        private void listviewListings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ButtonEnable(true);

            Listing listing = (Listing)listviewListings.SelectedItem;

            if (listing != null)
            {
                txtboxUploadTitle.Text = listing.Name;
                txtboxUploadPrice.Text = listing.Price.ToString();
                txtboxUploadDescription.Text = listing.Description;

                if (listing.Active)
                {
                    btnDeleteListing.IsEnabled = true;
                    btnUploadUpload.IsEnabled = true;
                }
                else
                {
                    btnDeleteListing.IsEnabled = false;
                    btnUploadUpload.IsEnabled = false;
                }
            }
        }


        //Button methods
        private void btnProfileMyProfile_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/ProfilePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileHistory_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/HistoryPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileFavorites_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/FavoritesPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileListings_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/MyListings/MyListingsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        
        
        private void btnDeleteListing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to deactivate this listing? \n this action can not be reverted!", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Information);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Listing listing = (Listing)listviewListings.SelectedItem;
                    _presenter.DeactivateListing(listing.Id);
                    //TODO change this with radio button
                    _RefreshListings(1);
                    break;
                case MessageBoxResult.No:
                    break;
                
            }
        }

        private void btnUploadUpload_Click(object sender, RoutedEventArgs e)
        {
            if (txtboxUploadTitle.Text == "")
            {
                MessageBox.Show("List must have a title!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else 
            {
                Listing listing = (Listing)listviewListings.SelectedItem;
                
                _presenter.UpdateListing(listing.Id ,txtboxUploadTitle.Text, float.Parse(txtboxUploadPrice.Text), txtboxUploadDescription.Text);
            }
        }

        private void radioboxActiveListings_Checked(object sender, RoutedEventArgs e)
        {
            _RefreshListings(1);
            btnDeleteListing.IsEnabled = false;
            btnUploadUpload.IsEnabled = false;
            resetListingInfo();
        }
        
        private void radioboxInactiveListings_Checked(object sender, RoutedEventArgs e)
        {
            _RefreshListings(0);
            btnDeleteListing.IsEnabled = false;
            btnUploadUpload.IsEnabled = false;
            resetListingInfo();
        }

        private void radioboxAllListings_Checked(object sender, RoutedEventArgs e)
        {
            _RefreshListings(-1);
            btnDeleteListing.IsEnabled = false;
            btnUploadUpload.IsEnabled = false;
            resetListingInfo();
        }

        private void resetListingInfo()
        {
            txtboxUploadTitle.Text = "";
            txtboxUploadPrice.Text = "";
            txtboxUploadDescription.Text = "";
            //add more..
        }
    }
}
