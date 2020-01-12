using Hermes.Model;
using Hermes.Model.Models;
using Hermes.Util;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Windows;

namespace Hermes.View.profile
{
    class ProfilePresenter
    {
        private readonly IProfileView _view;
        private readonly MyProfileRepository _repository;

        private List<Location> _locations;

        public ProfilePresenter(IProfileView view)
        {
            _view = view;
            _repository = new MyProfileRepository();
        }

        public void GetLocations()
        {
            _locations = _repository.GetLocations();

            List<string> locationNames = GetOnlyLocationNames();

            if(locationNames != null && locationNames.Count > 0)
            {
                _view.Locations = locationNames;
            }
        }

        public void LoadFields()
        {
            User user = GetCurrentUser();
            _view.Username = user.Username;
            _view.Password1 = user.Password;
            _view.Name = user.Name;
            _view.Email = user.Email;
            _view.Name = user.Name;
            _view.Surname = user.Surname;
            _view.Telephone = user.Telephone;
            _view.SetSelectedLocationTK = user.Address;
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

        public void EditUser()
        {
            if (HashingHelper.HashPassword(_view.Password1).Equals(GetCurrentUser().Password))
            {
                if (_view.Username.Length > 6)
                {
                    if (_view.SelectedLocationTK != null)
                    {
                        if (MessageBox.Show("You are about to change your personal data.\nYou will log out if you continue.\nAre you sure", "Change of data", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            User user = new User(GetCurrentUser().Id, _view.Username, GetCurrentUser().Password, _view.Name, _view.Surname, _view.SelectedLocationTK, _view.Email, _view.Telephone);
                            bool result = _repository.EditUser(user);
                            Logout();
                            ReLog(user);
                            //_view.Navigate = true;
                        }
                    }
                    else
                    {
                        _view.WarningDialog = "Please select a location and a ZIP code";
                    }
                }
                else
                {
                    _view.WarningDialog = "Username is too small.\nMust use atleast 6 characters.";
                }
            }
            else
            {
                _view.WarningDialog = "Password is incorect.";
            }
        }

        public void EditPassword()
        {

        }

        private User GetCurrentUser()
        {
            return (User)MemoryCache.Default["User"];
        }

        public void Logout()
        {
            ObjectCache Cache = MemoryCache.Default;
            if (Cache["User"] != null)
                Cache.Remove("User");
        }

        private void ReLog(User user)
        {
            ObjectCache Cache = MemoryCache.Default;
            Cache.Add("User", user, DateTime.Now.AddDays(30));
        }
    }
}
