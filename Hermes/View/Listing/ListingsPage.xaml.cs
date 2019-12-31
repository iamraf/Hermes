﻿using System;
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

            listviewListings.ItemsSource = new Model.Repository().GetListings();
        }

        private void listviewListings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Models.Listing listing = (Model.Models.Listing)listviewListings.SelectedItem;

            lblListingSelectedTitle.Content = listing.Name;
            tbListingSelectedDescription.Text = listing.Description;
        }
    }
}
