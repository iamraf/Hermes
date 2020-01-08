using Hermes.Model;
using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View.home
{
    class HomePresenter
    {
        private readonly IHomeView _view;
        private readonly HomeRepository _repository;

        public HomePresenter(IHomeView view)
        {
            _view = view;
            _repository = new HomeRepository();

            GetPopular();
            GetRecommended();
        }

        public void GetPopular()
        {
            List<Listing> list = _repository.GetPopular();

            if (list != null && list.Count > 0)
            {
                _view.PopularListings = list;
            }
        }

        public void GetRecommended()
        {
            User user = GetCurrentUser();

            if(user != null)
            {
                List<Listing> list = _repository.GetRecommended(user);

                if (list != null && list.Count > 0)
                {
                    _view.RecommendedListings = list;
                }
            }
        }

        public User GetCurrentUser()
        {
            return (User) MemoryCache.Default["User"];
        }
    }
}
