using Hermes.Model;
using Hermes.Model.Models;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Windows;

namespace Hermes.View.mylistings
{
    class MyListingsPresenter
    {
        private readonly IMyListingsView _view;
        private readonly MyListingsRepository _repository;

        public MyListingsPresenter(IMyListingsView view)
        {
            _view = view;
            _repository = new MyListingsRepository();
        }

        public void GetListings(int activeListing)
        {
            User user = GetCurrentUser();

            List<Listing> list = _repository.GetListings(user.Id, activeListing);

            if (list != null && list.Count > 0)
            {
                _view.Listings = list;
            }
        }

        public void UpdateListing(int id, string title, float price, string description)
        {
            bool update = _repository.UpdateListing(id, title, price, description);

            if(update)
            {
                MessageBox.Show("Listing updated!", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("ERROR!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            GetListings(1);
        }

        public void DeactivateListing(int listingID) 
        {
            _repository.deleteListing(listingID);            
        }

        public User GetCurrentUser()
        {
            return (User)MemoryCache.Default["User"];
        }

        public void Logout()
        {
            ObjectCache Cache = MemoryCache.Default;
            if (Cache["User"] != null)
                Cache.Remove("User");
        }
    }
}

