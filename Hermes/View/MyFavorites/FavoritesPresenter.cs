using Hermes.Model;
using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View.MyFavorites
{
    class FavoritesPresenter
    {

        private readonly IFavoritesPage _view;
        private readonly MyFavoritesRepository _MFRepository;
        private readonly ListingRepository _LRepository;

        public FavoritesPresenter(IFavoritesPage view)
        {
            _view = view;
            _MFRepository = new MyFavoritesRepository();
            _LRepository = new ListingRepository();
        }

        public void GetListings()
        {
            List<Listing> list = _MFRepository.GetListings(GetCurrentUser().Id); ;

            if (list != null && list.Count > 0)
            {
                _view.Listings = list;
            }
        }

        public User GetUploader(int id)
        {
            return _LRepository.GetUploader(id);
        }

        public User GetCurrentUser()
        {
            return (User)MemoryCache.Default["User"];
        }

        public void AddToFavourites(int listingId)
        {
            User user = GetCurrentUser();

            Favourite favourite = new Favourite(listingId, user.Id);

            _LRepository.AddToFavourite(favourite);
        }

        public void IncreaseView(int id)
        {
            _LRepository.IncreaseView(id);
        }

        public void AddToHistory(int listingId)
        {
            User user = GetCurrentUser();

            if (user != null)
            {
                _LRepository.AddToHistory(listingId, user.Id);
            }
        }

    }
}
