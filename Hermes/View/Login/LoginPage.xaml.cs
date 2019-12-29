using System.Windows;
using System.Windows.Controls;
namespace Hermes.View
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, ILoginpage
    {
        private readonly LoginPresenter _presenter;

        public LoginPage()
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
