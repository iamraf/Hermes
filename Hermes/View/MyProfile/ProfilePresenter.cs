using Hermes.Model;
using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View.MyProfile
{
    class ProfilePresenter
    {
        private readonly IProfilePage _view;
        private readonly MyProfileRepository _repository;
        private List<Location> _locations;

        public ProfilePresenter(IProfilePage view)
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
            if(_view.Username!=null && _view.Password1!=null && _view.Password2 != null)
            {
                if (_view.Password1.Equals(_view.Password2))
                {
                    bool result = _repository.EditUser(new User(GetCurrentUser().Id, _view.Username, _view.Password1, _view.Name, _view.Surname, _view.SelectedLocationTK, _view.Email, _view.Telephone));    
                }
            }
        }

        private User GetCurrentUser()
        {
            return (User)MemoryCache.Default["User"];
        }

        

    }
}
