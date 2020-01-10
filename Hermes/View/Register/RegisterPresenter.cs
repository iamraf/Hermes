using Hermes.Model;
using Hermes.Model.Models;
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
        private readonly MyProfileRepository _repository;
        private List<Location> _locations;

        public RegisterPresenter(IRegisterPage view)
        {
            _view = view;
            _register = FillFromFields();
            _repository = new MyProfileRepository();
        }

        public RegisterRepository FillFromFields()
        {
            string username = _view.TextBoxUsername;
            string password = _view.TextBoxPassword1;
            string email = _view.TextBoxEmail;
            string name = _view.TextBoxName;
            string surname = _view.TextBoxSurname;
            string phonenumber = _view.TextBoxPhoneNumber;
            string address = _view.SelectedLocationTK;

            if (username == "" || password == "" || email == "" || name == "" || surname == "" || phonenumber == "" || address == "")
                return null;
            else
                return new RegisterRepository(username, password, name, surname, address, email, phonenumber);
        }

        public bool RegisterUser()
        {
            if (_view.TextBoxPassword1.Equals(_view.TextBoxPassword2)) //checks if passwords match
            {
                _register = FillFromFields();
                if (_register != null) //checks if a field is null/empty
                {
                    int result = _register.RegisterQuery();

                    if (result == -2) //username already exists
                        _view.ErrorDialog = "A user with that username already exists.\nTry a new username.";
                    else if (result == -1) //unkown connection error
                        _view.ErrorDialog = "Error ocured on registration";
                    else
                    {
                        return true;
                    }
                }
                else
                    _view.ErrorDialog = "Something's missing. Try filling all fields.";
            }
            else
                _view.ErrorDialog = "Passwords do not match";
            return false;
        }

        public void GetLocations()
        {
            _locations = _repository.GetLocations();

            List<string> locationNames = GetOnlyLocationNames();

            if (locationNames != null && locationNames.Count > 0)
            {
                _view.Locations = locationNames;
            }
        }

        private List<string> GetOnlyLocationNames()
        {
            List<string> locNames = new List<string>();
            foreach (Location loc in _locations)
            {
                if (!locNames.Contains(loc.Name))
                {
                    locNames.Add(loc.Name);
                }

            }

            return locNames;
        }

        public void GetOnlyLocationTK(string selectedLocationName)
        {
            List<string> locTK = new List<string>();
            foreach (Location loc in _locations)
            {
                if (loc.Name.Equals(selectedLocationName))
                {
                    if (!locTK.Contains(loc.Tk.ToString()))
                    {
                        locTK.Add(loc.Tk.ToString());
                    }

                }

            }

            _view.LocationsTK = locTK;
        }

    }
}