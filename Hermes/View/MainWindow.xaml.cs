using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hermes.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnTopHome_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("View/home/HomeView.xaml", UriKind.RelativeOrAbsolute)); 
        }

        private void btnTopListings_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("View/listings/ListingsView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnTopUpload_Click(object sender, RoutedEventArgs e)
        {
            
            ObjectCache Cache = MemoryCache.Default;
            User user = (User)Cache["User"];
            if(user != null)
            {
                frameMain.Navigate(new Uri("View/Upload/UploadPage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                frameMain.Navigate(new Uri("View/Login/LoginPage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void btnTopLogin_Click(object sender, RoutedEventArgs e)
        {
            ObjectCache Cache = MemoryCache.Default;
            User user = (User)Cache["User"];
            if (user != null)
            {
                frameMain.Navigate(new Uri("View/ProfilePage.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                frameMain.Navigate(new Uri("View/Login/LoginPage.xaml", UriKind.RelativeOrAbsolute));
            }
            
        }

        private void frameMain_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ObjectCache Cache = MemoryCache.Default;
            User user = (User)Cache["User"];
            if (user != null)
            {
                btnTopLogin.Content = " ";
                expMyAccountDropdown.Visibility = Visibility.Visible;
            }
            else
            {
                btnTopLogin.Content = "Log in";
                expMyAccountDropdown.Visibility = Visibility.Collapsed;
            }
        }

        private void btnDropdownHistory_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("View/MyHistory/HistoryPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnDropdownFavorites_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("View/FavoritesPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnDropdownMyListings_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("View/MyListings/MyListingsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnTopSearch_Click(object sender, RoutedEventArgs e)
        {
            frameMain.NavigationService.Navigate(new ListingsView(txtboxTopSearch.Text));
        }

        private void btnTopHelp_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpwindow = new HelpWindow();
            helpwindow.Show();
        }

        private void btnTopClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
