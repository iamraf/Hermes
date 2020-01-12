using System;
using System.IO;
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
using System.Windows.Shapes;
using System.Net;
using System.Net.Mail;
using Hermes.Model;
using Hermes.Model.Models;

namespace Hermes.View.forgotpassword
{
    /// <summary>
    /// Interaction logic for forgotpasswordview.xaml
    /// </summary>
    public partial class forgotpasswordview : Window, IForgotPasswordView
    {
        private readonly ForgotPasswordviewPresenter _presenter;
        private User _user;
        public forgotpasswordview()
        {
            InitializeComponent();
            _presenter = new ForgotPasswordviewPresenter(this);
            _user = _presenter.GetCurrentUser();
            SendPassword();
            
        }

        public forgotpasswordview(string email)
        {
            InitializeComponent();
            _presenter = new ForgotPasswordviewPresenter(this);
            _user = _presenter.GetUserByEmail(email);
            SendPassword();
        }

        private String _confirmationCode;

        public string randomConfirmationCode()
        {
            string path = System.IO.Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path.Substring(0, 8); 
        }

        private void SendPassword()
        {
            _confirmationCode = randomConfirmationCode();

            MailMessage mailMessage = new MailMessage("hermeslistings@gmail.com", _user.Email);
            mailMessage.Subject = "Κωδικός Επαναφοράς Hermes Listings";
            mailMessage.Body = string.Format("Ο κωδικός επαναφοράς είναι: "+ _confirmationCode);
            mailMessage.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("hermeslistings@gmail.com", "hermeslistingssince2019");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mailMessage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtboxGetUsersInput.Text == _confirmationCode)
            {
                textboxPassword.IsEnabled = true;
                textboxPasswordConfirm.IsEnabled = true;
                btnChangePasswordDone.IsEnabled = true;
            }
            else
            {
                //labelConfirmationCodeFail.Text = "Enter the right confirmation code.";
            }
        }

        private void btnChangePasswordDone_Click(object sender, RoutedEventArgs e)
        {
            if (textboxPassword.Text.Length > 8)
            {
                if(textboxPassword.Text==textboxPasswordConfirm.Text)
                {
                    _presenter.UpdateUsersPassword(textboxPassword.Text);
                    this.Close();
                }
                else
                {
                    //labelChangePassword.Text = "The passwords don't match.";
                }
            }
            else
            {
                //labelChangePassword.Text = "Minimum password length 8 characters.";
            }
        }
    }
}
