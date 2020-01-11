using Hermes.Model;
using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Hermes.View.listings
{
    class ListingsPresenter
    {
        private readonly IListingsView _view;
        private readonly ListingRepository _repository;
        private readonly MyFavoritesRepository _MFRepository;

        public ListingsPresenter(IListingsView view)
        {
            _view = view;
            _repository = new ListingRepository();
            _MFRepository = new MyFavoritesRepository();
        }

        public void GetListings()
        {
            List<Listing> list = _repository.GetListings(0, "creationDate");

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

        private String SortListing(int option)
        {
            switch(option)
            {
                case 1:
                    return "price";
                    break;
                case 2:
                    return "listViews";
                    break;
                default:
                    return "creationDate";
                    break;
            }
        }

        public void AddToFavourites(int listingId)
        {
            User user = GetCurrentUser();

            if (user != null)
            {
                Favourite favourite = new Favourite(listingId, user.Id);

                _repository.AddToFavourite(favourite);
            }
        }

        public void GetSearchResults(string query)
        {
            List<Listing> list = _repository.GetSearchResult(query);

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public void GetFilteredListings(List<string> catIds, int category, int order)
        {
            List<Listing> list = _repository.FilteredListings(catIds, category, SortListing(order));

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public void PriceFilteredListings(List<string> catIds, int priceOption, int category, int order)
        {
            switch (priceOption)
            {
                case 1:
                    GetPriceFilteredListings(catIds, "<=", 100, category, SortListing(order));
                    break;
                case 2:
                    GetPriceFilteredListings(catIds, ">", 100, category, SortListing(order));
                    break;
                default:
                    GetPriceFilteredListings(catIds, "=", 0, category, SortListing(order));
                    break;
            }
        }

        public void DynamicPriceFilteredListings(List<string> catIds, int price, int category, int order)
        {
            
            GetPriceFilteredListings(catIds, ">=", price, category, SortListing(order));
        }

        private void GetPriceFilteredListings(List<string> catIds, string comparisonOperator, int price, int category, string order)
        {
            List<Listing> list = _repository.PriceFilteredListings(catIds, comparisonOperator, price, category, order);

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public void DateFilteredListings(List<string> catIds, int dateOption, int category, int order)
        {
            GetDateFilteredListings(catIds, GetDateChoice(dateOption), category, SortListing(order));
        }

        public void DateAndPriceFilteredListings(List<string> catIds, int priceOption, bool comboxPrice, int dateOption, int category, int order)
        {
            string date = GetDateChoice(dateOption);
            if (comboxPrice)
            {
                switch (priceOption)
                {
                    case 1:
                        GetDateAndPriceFilteredListings(catIds, "<=", 100, date, category, SortListing(order));
                        break;
                    case 2:
                        GetDateAndPriceFilteredListings(catIds, ">", 100, date, category, SortListing(order));
                        break;
                    default:
                        GetDateAndPriceFilteredListings(catIds, "=", 0, date, category, SortListing(order));
                        break;
                }
            }
            else
            {
                GetDateAndPriceFilteredListings(catIds, ">=", priceOption, date, category, SortListing(order));
            }
        }

        private string GetDateChoice(int dateOption)
        {
            string date = "";

            switch (dateOption)
            {
                case 1:
                    date = "DAY";
                    break;
                case 2:
                    date = "WEEK";
                    break;
                case 3:
                    date = "MONTH";
                    break;
                case 4:
                    date = "YEAR";
                    break;
                default:
                    date = "HOUR";
                    break;
            }

            return date;
        }
        private void GetDateAndPriceFilteredListings(List<string> catIds, string comparisonOperator, float price, string dateOption, int category, string order)
        {
            List<Listing> list = _repository.GetDateAndPriceFilteredListings(catIds, comparisonOperator, price, dateOption, category, order);
            
            if (list != null)
            {
                _view.Listings = list;
            }
        }

        private void GetDateFilteredListings(List<string> catIds, string dateOption, int category, string order)
        {
            List<Listing> list = _repository.GetDateFilteredListings(catIds, dateOption, category, order);

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public void IncreaseView(int id)
        {
            _repository.IncreaseView(id);
        }

        public void AddToHistory(int listingId)
        {
            User user = GetCurrentUser();

            if(user != null)
            {
                _repository.AddToHistory(listingId, user.Id);
            }
        }

        public void ChangeCategory(int category, int order)
        {
            List<Listing> list = _repository.GetListings(category, SortListing(order));

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public List<SubCategory> GetSubcategoriesFromSpecificCategory(int category)
        {
            if (category != 0)
            {
                List<SubCategory> subCategories = _repository.GetSubcategoriesFromSpecificCategory(category);

                return subCategories;
            }
            return null;
        }

        public void GetFavorites()
        {
            if (GetCurrentUser() != null)
            {
                List<Listing> favorites = _MFRepository.GetListings(GetCurrentUser().Id);
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
}
