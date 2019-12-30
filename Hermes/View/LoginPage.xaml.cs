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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, ILoginpage
    {
        public LoginPage()
        {
            InitializeComponent();
            _presenter = new LoginPresenter(this);
        }

        private readonly LoginPresenter _presenter;

        public string LabelUsername
        {
            get { return txtboxUsername.Text; }
            set { txtboxUsername.Text = value; }
        }
        public string LabelPassword
        {
            get { return txtboxPassword.Text; }
            set { txtboxPassword.Text = value; }
        }

        public string ErrorDialog
        {
            set
            {
                MessageBox.Show(value, "Login was unsuccessful", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            _presenter.UserExist();
        }
    }
}
