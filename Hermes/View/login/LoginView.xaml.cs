using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hermes.Model.Models;
using Hermes.View.register;

namespace Hermes.View.login
{
    public partial class LoginView : Page, ILoginView
    {
        private readonly LoginPresenter _presenter;

        public LoginView()
        {
            InitializeComponent();
            _presenter = new LoginPresenter(this);
        }

        public string LabelUsername
        {
            get { return txtboxUsername.Text; }
            set { txtboxUsername.Text = value; }
        }
        public string LabelPassword
        {
            get { return txtboxPassword.Password; }
            set { txtboxPassword.Password = value; }
        }

        public string ErrorDialog
        {
            set
            {
                MessageBox.Show(value, "Login was unsuccessful", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public User LoggedUser
        { 
            set
            {
                this.NavigationService.Navigate(new Uri("View/home/HomeView.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            _presenter.UserLogin();
        }

        private void btnRegisterPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterView());
        }

        private void txtboxUsername_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _presenter.UserLogin();
            }
        }

        private void txtboxPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _presenter.UserLogin();
            }
        }
    }
}
