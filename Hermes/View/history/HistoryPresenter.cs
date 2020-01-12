using Hermes.Model;
using Hermes.Model.Models;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Hermes.View.history
{
    class HistoryPresenter
    {
        private readonly IHistoryView _view;

        private readonly HistoryRepository _historyRepository;
        private readonly ListingRepository _listingRepository;
        private readonly FavoritesRepository _favouritesRepository;

        public HistoryPresenter(IHistoryView view)
        {
            _view = view;

            _historyRepository = new HistoryRepository();
            _listingRepository = new ListingRepository();
            _favouritesRepository = new FavoritesRepository();
        }

        public void GetListings()
        {
            List<Listing> list = _historyRepository.GetUserHistory(GetCurrentUser().Id);

            if (list != null && list.Count > 0)
            {
                _view.Listings = list;
            }
        }

        public User GetUploader(int id)
        {
            return _listingRepository.GetUploader(id);
        }

        public User GetCurrentUser()
        {
            return (User) MemoryCache.Default["User"];
        }

        public void AddToFavourites(int listingId)
        {
            User user = GetCurrentUser();

            Favourite favourite = new Favourite(listingId, user.Id);

            _listingRepository.AddToFavourite(favourite);
        }

        public void IncreaseView(int id)
        {
            _listingRepository.IncreaseView(id);
        }

        public void AddToHistory(int listingId)
        {
            User user = GetCurrentUser();

            if (user != null)
            {
                _listingRepository.AddToHistory(listingId, user.Id);
            }
        }

        public void GetFavorites()
        {
            List<Listing> favorites = _favouritesRepository.GetFavouriteListings(GetCurrentUser().Id);

            if (favorites != null)
            {
                _view.Favorites = favorites;
            }
            else
            {
                _view.Favorites = null;
            }
        }
    }
}
