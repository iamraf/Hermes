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
        public ListingsPage()
        {
            InitializeComponent();

            listviewListings.ItemsSource = new Repository().GetListings();
        }

        private void listviewListings_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Listing listing = (Listing)listviewListings.SelectedItem;

            lblListingSelectedTitle.Content = listing.Name;
            tbListingSelectedDescription.Text = listing.Description;
        }
    }
}
