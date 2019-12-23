using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Model;
using Hermes.View;

namespace Hermes.Presenter
{
    class LoginPresenter
    {
        private ILoginpage _view;
        private Login _login;

        public LoginPresenter(ILoginpage view)
        {
            _view = view;
            _login = new Login();
        }

        public void UserExist()
        {
            //TODO: change this to private 
            string _username=_view.LabelUsername;
            string _password=_view.LabelPassword;

            
            string result = _login.UserExist(_username,_password);
            //Convert strint to int
            int ResultInt = Int32.Parse(result);
         
            if (ResultInt == 1)
            {
                //TODO: Login approved pass data to next view
            }
            else
            {
                _view.ErrorDialog = "Username and password does not match";
            }
        }
    }
            

 }

    

