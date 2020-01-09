using Hermes.View.Register;
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

        public string TextBoxAddress
        {
            get { return txtboxRegisterAddress.Text; }
        }

        public string ErrorDialog
        {
            set
            {
                MessageBox.Show(value, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (_presenter.RegisterUser())
                this.NavigationService.Navigate(new LoginPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Image imageBackground = new Image();
            var UriSource = new Uri(@"/Hermes;component/View/Images/Background1.jpg", UriKind.RelativeOrAbsolute);
            imageBackground.Source = new BitmapImage(UriSource);
        }
    }
}