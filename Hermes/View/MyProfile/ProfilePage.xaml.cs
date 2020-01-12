using Hermes.Model.Models;
using Hermes.View.MyProfile;
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
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page, IProfilePage
    {
        private readonly ProfilePresenter _presenter;
        public ProfilePage()
        {
            InitializeComponent();
            _presenter = new ProfilePresenter(this);
            _presenter.GetLocations();
            _presenter.LoadFields();

            comboxRegisterAddress.SelectedIndex = 0;
            comboxRegisterAddressTK.IsEditable = false;
        }

        public string Username 
        {
            set
            {
                txtboxRegisterUsername2.Text = null;
                txtboxRegisterUsername2.Text = value;
            }
            get
            {
                return txtboxRegisterUsername2.Text;
            } 
        }
        public string Password1
        {
            set
            {
                txtboxRegisterPassword5.Text = null;
                txtboxRegisterPassword5.Text = value;
            }
            get
            {
                return txtboxRegisterPassword5.Text;
            }
        }
        public string Password2
        {
            set
            {
                txtboxRegisterPassword6.Text = null;
                txtboxRegisterPassword6.Text = value;
            }
            get
            {
                return txtboxRegisterPassword6.Text;
            }
        }
        public string Email
        {
            set
            {
                txtboxRegisterEmail.Text = null;
                txtboxRegisterEmail.Text = value;
            }
            get
            {
                return txtboxRegisterEmail.Text;
            }
        }
        public string Name
        {
            set
            {
                txtboxRegisterName.Text = null;
                txtboxRegisterName.Text = value;
            }
            get
            {
                return txtboxRegisterName.Text;
            }
        }
        public string Surname
        {
            set
            {
                txtboxRegisterSurname.Text = null;
                txtboxRegisterSurname.Text = value;
            }
            get
            {
                return txtboxRegisterSurname.Text;
            }
        }
        public string Telephone
        {
            set
            {
                txtboxRegisterPhone.Text = null;
                txtboxRegisterPhone.Text = value;
            }
            get
            {
                return txtboxRegisterPhone.Text;
            }
        }
        public string SetSelectedLocation
        {
            set
            {
                comboxRegisterAddress.SelectedItem = value;
            }
        }
        public List<string> Locations
        {
            set
            {
                comboxRegisterAddress.ItemsSource = null;
                comboxRegisterAddress.ItemsSource = value;
            }
        }

        public List<string> LocationsTK
        {
            set
            {
                comboxRegisterAddressTK.ItemsSource = null;
                comboxRegisterAddressTK.ItemsSource = value;
            }
        }

        public string SetSelectedLocationTK
        {
            set
            {
                comboxRegisterAddressTK.SelectedItem = value;
            }
        }

        public string SelectedLocationTK
        {
            get
            {
                if (comboxRegisterAddressTK.SelectedItem != null)
                {
                    return comboxRegisterAddressTK.SelectedItem.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        private void btnProfileHistory_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/MyHistory/HistoryPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileMyProfile_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/MyProfile/ProfilePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileFavorites_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/MyFavorites/FavoritesPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnProfileListings_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("View/MyListings/MyListingsPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void comboxRegisterAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboxRegisterAddressTK.IsEnabled = true;
            _presenter.GetOnlyLocationTK((string)comboxRegisterAddress.SelectedItem);
            comboxRegisterAddressTK.SelectedIndex = 0;
            //randomtext
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Username.ToString().Length > 6 && Password1.ToString().Length > 6)
            {
                if (Password1.Equals(Password2))
                {
                    if (comboxRegisterAddressTK.SelectedItem != null)
                    {
                        if(MessageBox.Show("You are about to change your personal data.\nYou will log out if you continue.\nAre you sure","Change of data", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            _presenter.EditUser();
                            Logout();
                            this.NavigationService.Navigate(new Uri("View/Login/LoginPage.xaml", UriKind.RelativeOrAbsolute));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a ZIP code.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Passwords dont match.", "Error");
                }
            }
            else
            {
                MessageBox.Show("Username or Password is too small.\nTry something bigger.", "Error");
            }
        }
        private void Logout()
        {
            ObjectCache Cache = MemoryCache.Default;
            if (Cache["User"] != null)
                Cache.Remove("User");
        }

        private void btnProfileSignout_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }


    }

}
