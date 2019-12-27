using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hermes.Presenter;

namespace Hermes.View
{
    public partial class Loginpage : Form, ILoginpage
    {
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
                MessageBox.Show(value, "Login was unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        public Loginpage()
        {
            InitializeComponent();
            _presenter = new LoginPresenter(this);
        }

        private void btnRegisterPage_Click(object sender, EventArgs e)
        {
            RegisterPage form = new RegisterPage();
            form.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _presenter.UserExist();
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {

        }

        
    }
}
