using Hermes.Model;
using Hermes.Model.Models;
using System.Collections.Generic;

namespace Hermes.View.register
{
    class RegisterPresenter
    {
        private readonly IRegisterView _view;

        private readonly RegisterRepository _registerRepository;
        private readonly MyProfileRepository _profileRepository;

        public RegisterPresenter(IRegisterView view)
        {
            _view = view;

            _registerRepository = new RegisterRepository();
            _profileRepository = new MyProfileRepository();
        }

        public void RegisterUser(User user)
        {
            int result = _registerRepository.RegisterUser(user);
            
            if(result == -2)
            {
                _view.ErrorDialog = "A user with that username already exists.\nTry a new username.";
            }
            else if (result == -1)
            {
                _view.ErrorDialog = "Error ocured on registration";
            }
            else
            {
                _view.Navigate = true;
            }
        }

        public void GetLocations()
        {
            List<string> locationNames = GetOnlyLocationNames();

            if (locationNames != null && locationNames.Count > 0)
            {
                _view.Locations = locationNames;
            }
        }

        private List<string> GetOnlyLocationNames()
        {
            List<Location> locations = _profileRepository.GetLocations();
            List<string> locNames = new List<string>();

            foreach (Location loc in locations)
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
            List<Location> locations = _profileRepository.GetLocations();
            List<string> locTK = new List<string>();

            foreach (Location loc in locations)
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