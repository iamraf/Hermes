using Hermes.Model;
using Hermes.Model.Models;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Hermes.View.favourites
{
    class FavoritesPresenter
    {
        private readonly IFavoritesView _view;

        private readonly FavoritesRepository _favouritesRepository;
        private readonly ListingRepository _listingsRepository;

        public FavoritesPresenter(IFavoritesView view)
        {
            _view = view;
            _favouritesRepository = new FavoritesRepository();
            _listingsRepository = new ListingRepository();
        }

        public void GetListings()
        {
            List<Listing> list = _favouritesRepository.GetFavouriteListings(GetCurrentUser().Id);

            if (list != null && list.Count > 0)
            {
                _view.Listings = list;
            }
        }

        public User GetUploader(int id)
        {
            return _listingsRepository.GetUploader(id);
        }

        public User GetCurrentUser()
        {
            return (User) MemoryCache.Default["User"];
        }

        public void AddToFavourites(int listingId)
        {
            User user = GetCurrentUser();

            Favourite favourite = new Favourite(listingId, user.Id);

            _listingsRepository.AddToFavourite(favourite);
        }

        public void IncreaseView(int id)
        {
            _listingsRepository.IncreaseView(id);
        }

        public void AddToHistory(int listingId)
        {
            User user = GetCurrentUser();

            if (user != null)
            {
                _listingsRepository.AddToHistory(listingId, user.Id);
            }
        }
    }
}
