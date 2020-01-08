using Hermes.Model;
using Hermes.Model.Models;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Windows;

namespace Hermes.View
{
    class MyListingsPresenter
    {
        private readonly IMyListingsPage  _view;
        private readonly MyListingsRepository _repository;
        private readonly ObjectCache Cache;

        public MyListingsPresenter(IMyListingsPage view)
        {
            _view = view;
            _repository = new MyListingsRepository();
           Cache = MemoryCache.Default;
        }

        public void GetListings(int activeListing)
        {
            User user = (User)Cache["User"];
            List<Listing> list = _repository.GetListings(user.Id, activeListing);

            if (list != null && list.Count > 0)
            {
                _view.Listings = list;
            }
        }

        public void UpdateListing(int id, string title, float price, string description)
        {
            bool update = _repository.UpdateListing(id, title, price, description);
            if (update)
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

    }
}

