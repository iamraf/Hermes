using Hermes.Model;
using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View.MyHistory
{
    class HistoryPresenter
    {
        private readonly IHistoryPage _view;
        private readonly MyHistoryRepository _MHRepository;
        private readonly ListingRepository _LRepository;

        public HistoryPresenter(IHistoryPage view)
        {
            _view = view;
            _MHRepository = new MyHistoryRepository();
            _LRepository = new ListingRepository();
        }

        public void GetListings()
        {
            List<Listing> list = _MHRepository.GetListings(GetCurrentUser().Id); ;

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
