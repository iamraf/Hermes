using Hermes.Model;
using Hermes.Model.Models;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Hermes.View.listings
{
    class ListingsPresenter
    {
        private readonly IListingsView _view;
        private readonly ListingRepository _repository;

        public ListingsPresenter(IListingsView view)
        {
            _view = view;
            _repository = new ListingRepository();

            GetListings();
        }

        private void GetListings()
        {
            List<Listing> list = _repository.GetListings();

            if(list != null && list.Count > 0)
            {
                _view.Listings = list;
            }
        }

        private void GetSortListing(string field)
        {
            List<Listing> list = _repository.GetSortedListings(field);

            if(list != null && list.Count > 0)
            {
                _view.Listings = list;
            }
        }

        public User GetUploader(int id)
        {
            return _repository.GetUploader(id);
        }

        public User GetCurrentUser()
        {
            return (User) MemoryCache.Default["User"];
        }

        public void SortListing(int option)
        {
            switch(option)
            {
                case 1:
                    GetSortListing("price");
                    break;
                case 2:
                    GetSortListing("listViews");
                    break;
                default:
                    GetSortListing("creationDate");
                    break;
            }
        }

        public void AddToFavourites(int listingId)
        {
            User user = GetCurrentUser();

            if(user != null)
            {
                Favourite favourite = new Favourite(listingId, user.Id);

                _repository.AddToFavourite(favourite);
            }
            else
            {
                _view.Navigate = true;
            }
        }

        public void IncreaseView(int id)
        {
            _repository.IncreaseView(id);
        }
    }
}
