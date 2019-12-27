using Hermes.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hermes.View
{
    public partial class RegisterPage : Form, IRegisterView
    {

        private readonly RegisterPresenter _presenter;

        public string TextBoxUsername
        {
            get { return txtboxRegisterUsername.Text; }
        }

        public string TextBoxPassword1
        {
            get { return txtboxRegisterPassword1.Text; }
        }

        public string TextBoxPassword2
        {
            get { return txtboxRegisterPassword2.Text; }
        }

        public string TextBoxEmail
        {
            get { return txtboxRegisterEmail.Text; }
        }

        public string TextBoxName
        {
            get { return txtboxRegisterName.Text; }
        }

        /*public string TextBoxSurname
        {
            get { return txtboxRegisterSurrname.Text; }
        }*/

        public string TextBoxPhoneNumber
        {
            get { return txtboxRegisterPhone.Text; }
        }

        //location?
        /*public string TextBoxLocation
        {
            get { return txtboxRegisterLocation.Text; }
        }*/

        public string TextBoxAddress
        {
            get { return txtboxRegisterAddress.Text; }
        }

        public string ErrorDialog
        {
            set
            {
                MessageBox.Show(value, "Could not register this user", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public RegisterPage()
        {
            InitializeComponent();
            _presenter = new RegisterPresenter(this);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            _presenter.RegisterUser();
        }


        
        private void txtboxRegisterAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxRegisterLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxRegisterPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxRegisterName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxRegisterEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblRegisterPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
