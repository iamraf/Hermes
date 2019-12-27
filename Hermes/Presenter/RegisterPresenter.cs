using Hermes.Model;
using Hermes.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Presenter
{
    class RegisterPresenter
    {
        private IRegisterView _view;
        private Register _register;

        public RegisterPresenter(IRegisterView view)
        {
            _view = view;
            _register = null;
        }

        public Register FillFromFields()
        {
            string username = _view.TextBoxUsername;
            string password = _view.TextBoxPassword1;
            string email = _view.TextBoxEmail;
            string name = _view.TextBoxName;
            string surname = _view.TextBoxName; //_view.TextBoxSurname;
            string phonenumber = _view.TextBoxPhoneNumber;
            string address = _view.TextBoxAddress;

            if (username == "" || password == "" || email == "" || name == "" || surname == "" || phonenumber== "" || address == "")
                return null;
            else
                return new Register(username, password, name, surname, address, email, int.Parse(phonenumber));
        }

        public void RegisterUser()
        {
            if (_view.TextBoxPassword1.Equals(_view.TextBoxPassword2))
            {
                _register = FillFromFields();
                if (_register != null) { 

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
