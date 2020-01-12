using Hermes.Model.Models;
using Hermes.View.home;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Hermes.View.register
{
    public partial class RegisterView : Page, IRegisterView
    {
        private readonly RegisterPresenter _presenter;

        public RegisterView()
        {
            InitializeComponent();

            _presenter = new RegisterPresenter(this);
            _presenter.GetLocations();

            comboxRegisterLocation.SelectedIndex = 0;
            comboxRegisterLocationTK.IsEditable = false;
        }


        public string ErrorDialog
        {
            set
            {
                MessageBox.Show(value, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public List<string> Locations
        {
            set
            {
                comboxRegisterLocation.ItemsSource = null;
                comboxRegisterLocation.ItemsSource = value;
            }
        }

        public List<string> LocationsTK
        {
            set
            {
                comboxRegisterLocationTK.ItemsSource = null;
                comboxRegisterLocationTK.ItemsSource = value;
            }
        }

        public bool Navigate
        { 
            set
            {
                if(value)
                {
                    MessageBox.Show("Registration complete", "Done", MessageBoxButton.OK);

                    this.NavigationService.Navigate(new HomeView(true));
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if(txtboxRegisterUsername2.Text.Length == 0 || txtboxRegisterEmail.Text.Length == 0 || txtboxRegisterName.Text.Length == 0 
                || txtboxRegisterSurname.Text.Length == 0 || txtboxRegisterPhone.Text.Length == 0 || txtboxRegisterPassword5.Password.Length == 0
                || txtboxRegisterPassword6.Password.Length == 0 || txtboxRegisterEmail.Text.Length == 0 || comboxRegisterLocationTK.SelectedItem == null)
            {
                MessageBox.Show("Fields are missing", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(!txtboxRegisterPassword5.Password.Equals(txtboxRegisterPassword6.Password))
            {
                MessageBox.Show("Password do not match", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _presenter.RegisterUser(new User(-1, txtboxRegisterUsername2.Text, txtboxRegisterPassword5.Password, txtboxRegisterName.Text, txtboxRegisterSurname.Text,
                    comboxRegisterLocationTK.SelectedItem.ToString(), txtboxRegisterEmail.Text, txtboxRegisterPhone.Text));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Image imageBackground = new Image();
            var UriSource = new Uri(@"/Hermes;component/Images/Background1.jpg", UriKind.RelativeOrAbsolute);
            imageBackground.Source = new BitmapImage(UriSource);
        }

        private void comboxRegisterLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboxRegisterLocationTK.IsEnabled = true;
            _presenter.GetOnlyLocationTK((string)comboxRegisterLocation.SelectedItem);
            comboxRegisterLocationTK.SelectedIndex = 0;
        }

        //Limits textboxes to letters only
        private void txtboxLetterValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Limits textboxes to numbers only
        private void txtboxNumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}