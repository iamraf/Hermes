using Hermes.Model;
using Hermes.Model.Models;
using System.Collections.Generic;
using System.Runtime.Caching;
/* FavoritesPresenter connect view with model classes
 *  it gets data from the repositories 
 *  and pass them to view
 */

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
            
            _view.DeleteButtonEnable = false;

            if (list != null && list.Count > 0)
            {
                _view.Listings = list;
            }
            else
            {
                _view.Listings = null;
                _view.DeleteAllButtonEnable = false;
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

        public void Logout()
        {
            ObjectCache Cache = MemoryCache.Default;
            if (Cache["User"] != null)
                Cache.Remove("User");
        }

        public void DeleteThisFromFavorites()
        {
            _listingsRepository.DeleteFromFavorite(new Favourite(_view.SelectedListing.Id, GetCurrentUser().Id));
            GetListings();
        }

        public void DeleteAllFromFavorites()
        {
            _listingsRepository.DeleteAllFromFavorite(new Favourite(0, GetCurrentUser().Id));
            GetListings();
        }
    }
}
