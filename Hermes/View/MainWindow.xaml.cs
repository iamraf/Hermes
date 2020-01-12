using Hermes.Model.Models;
using Hermes.View.help;
using Hermes.View.listings;
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
using Hermes.View.buyPremium;
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
                frameMain.Navigate(new Uri("View/upload/UploadView.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                frameMain.Navigate(new Uri("View/login/LoginView.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void btnTopLogin_Click(object sender, RoutedEventArgs e)
        {
            ObjectCache Cache = MemoryCache.Default;
            User user = (User)Cache["User"];
            if (user != null)
            {
                //frameMain.Navigate(new Uri("View/profile/ProfileView.xaml", UriKind.RelativeOrAbsolute));
                buyPremiumWIndow window = new buyPremiumWIndow();
                window.ShowDialog();
            }
            else
            {
                frameMain.Navigate(new Uri("View/login/LoginView.xaml", UriKind.RelativeOrAbsolute));
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

            expMyAccountDropdown.IsExpanded = false;
        }

        private void btnDropdownHistory_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("View/history/HistoryView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnDropdownFavorites_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("View/favourites/FavoritesView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnDropdownMyListings_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("View/mylistings/MyListingsView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnTopSearch_Click(object sender, RoutedEventArgs e)
        {
            frameMain.NavigationService.Navigate(new ListingsView(txtboxTopSearch.Text));
        }

        private void btnTopHelp_Click(object sender, RoutedEventArgs e)
        {
            HelpView helpwindow = new HelpView();
            helpwindow.Show();
        }

        private void btnTopClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDropdownSignOut_Click(object sender, RoutedEventArgs e)
        {
            Logout();
            frameMain.Navigate(new Uri("View/login/LoginView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Logout()
        {
            ObjectCache Cache = MemoryCache.Default;
            if (Cache["User"] != null)
                Cache.Remove("User");
        }

        private void btnTopLogo_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("View/home/HomeView.xaml", UriKind.RelativeOrAbsolute));
        }

        private void txtboxTopSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                frameMain.NavigationService.Navigate(new ListingsView(txtboxTopSearch.Text));
            }
        }

        private void rectangleDrag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnDropdownPremium_Click(object sender, RoutedEventArgs e)
        {
            //premium page doesn't exist yet
            //frameMain.Navigate(new Uri("View/Premium/premiumpage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnDropdownProfile_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Navigate(new Uri("View/profile/ProfileView.xaml", UriKind.RelativeOrAbsolute));
        }

    }
}
