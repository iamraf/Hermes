using Hermes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View.Register
{
    class RegisterPresenter
    {

        private IRegisterPage _view;
        private RegisterRepository _register;

        public RegisterPresenter(IRegisterPage view)
        {
            _view = view;
            _register = null;
        }

        public RegisterRepository FillFromFields()
        {
            string username = _view.TextBoxUsername;
            string password = _view.TextBoxPassword1;
            string email = _view.TextBoxEmail;
            string name = _view.TextBoxName;
            string surname = _view.TextBoxSurname;
            string phonenumber = _view.TextBoxPhoneNumber;
            string address = _view.TextBoxAddress;

            if (username == "" || password == "" || email == "" || name == "" || surname == "" || phonenumber == "" || address == "")
                return null;
            else
                return new RegisterRepository(username, password, name, surname, address, email, phonenumber);
        }

        public void RegisterUser()
        {
            if (_view.TextBoxPassword1.Equals(_view.TextBoxPassword2))
            {
                _register = FillFromFields();
                if (_register != null)
                {

                    try { 
                        bool result = _register.RegisterQuery();

                        if (result == true)
                        {
                            // user is registered
                            _view.ErrorDialog = "DONE";
                        }
                        else
                        {
                            _view.ErrorDialog = "Could not register this user";
                        }
                    }
                    catch (Exception e)
                    {
                        _view.ErrorDialog = e.Message; //user already exists (username)
                    }                    
                }
                else
                {
                    _view.ErrorDialog = "Something's missing. Try filling all fields.";
                }
            }
            else
            {
                _view.ErrorDialog = "Passwords do not match";
            }

        }

    }
}
