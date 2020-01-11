using Hermes.View.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page, IRegisterPage
    {
        private readonly RegisterPresenter _presenter;

        public RegisterPage()
        {
            InitializeComponent();
            _presenter = new RegisterPresenter(this);
            _presenter.GetLocations();

            comboxRegisterLocation.SelectedIndex = 0;
            comboxRegisterLocationTK.IsEditable = false;
        }

        public string TextBoxUsername
        {
            get { return txtboxRegisterUsername2.Text; }
        }

        public string TextBoxPassword1
        {
            get { return txtboxRegisterPassword5.Password; }
        }

        public string TextBoxPassword2
        {
            get { return txtboxRegisterPassword6.Password; }
        }

        public string TextBoxEmail
        {
            get { return txtboxRegisterEmail.Text; }
        }

        public string TextBoxName
        {
            get { return txtboxRegisterName.Text; }
        }

        public string TextBoxSurname
        {
            get { return txtboxRegisterSurname.Text; }
        }

        public string TextBoxPhoneNumber
        {
            get { return txtboxRegisterPhone.Text; }
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

        public string SelectedLocation
        {
            get
            {
                if (comboxRegisterLocation.SelectedItem != null)
                {
                    return comboxRegisterLocation.SelectedItem.ToString();
                }
                else
                {
                    return null;
                }
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

        public string SelectedLocationTK
        {
            get
            {
                if (comboxRegisterLocationTK.SelectedItem != null)
                {
                    return comboxRegisterLocationTK.SelectedItem.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (_presenter.RegisterUser())
            {
                MessageBox.Show("Registration complete","Done",MessageBoxButton.OK);
                this.NavigationService.Navigate(new LoginPage());
            }
                
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Image imageBackground = new Image();
            var UriSource = new Uri(@"/Hermes;component/View/Images/Background1.jpg", UriKind.RelativeOrAbsolute);
            imageBackground.Source = new BitmapImage(UriSource);
        }

        private void comboxRegisterLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboxRegisterLocationTK.IsEnabled = true;
            _presenter.GetOnlyLocationTK((string)comboxRegisterLocation.SelectedItem);
            comboxRegisterLocationTK.SelectedIndex = 0;
        }

        private void txtboxLetterValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtboxNumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}