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
        private readonly ListingRepository _listingsRepository;
        private readonly FavoritesRepository _favouritesRepository;

        public ListingsPresenter(IListingsView view)
        {
            _view = view;

            _listingsRepository = new ListingRepository();
            _favouritesRepository = new FavoritesRepository();
        }

        public void GetListings()
        {
            List<Listing> list = _listingsRepository.GetListings(0, "creationDate");

            if(list != null && list.Count > 0)
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

        private String SortListing(int option)
        {
            return option switch
            {
                1 => "price",
                2 => "listViews DESC",
                _ => "creationDate DESC",
            };
        }

        public void AddToFavourites(int listingId)
        {
            User user = GetCurrentUser();

            if (user != null)
            {
                Favourite favourite = new Favourite(listingId, user.Id);

                _listingsRepository.AddToFavourite(favourite);
            }
        }

        public void GetSearchResults(string query)
        {
            List<Listing> list = _listingsRepository.GetSearchResult(query);

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public void GetFilteredListings(List<string> catIds, int category, int order)
        {
            List<Listing> list = _listingsRepository.FilteredListings(catIds, category, SortListing(order));

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public void PriceFilteredListings(List<string> catIds, int priceOption, int category, int order)
        {
            switch(priceOption)
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
            List<Listing> list = _listingsRepository.PriceFilteredListings(catIds, comparisonOperator, price, category, order);

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
            var date = dateOption switch
            {
                1 => "DAY",
                2 => "WEEK",
                3 => "MONTH",
                4 => "YEAR",
                _ => "HOUR",
            };

            return date;
        }
        private void GetDateAndPriceFilteredListings(List<string> catIds, string comparisonOperator, float price, string dateOption, int category, string order)
        {
            List<Listing> list = _listingsRepository.GetDateAndPriceFilteredListings(catIds, comparisonOperator, price, dateOption, category, order);
            
            if (list != null)
            {
                _view.Listings = list;
            }
        }

        private void GetDateFilteredListings(List<string> catIds, string dateOption, int category, string order)
        {
            List<Listing> list = _listingsRepository.GetDateFilteredListings(catIds, dateOption, category, order);

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public void IncreaseView(int id)
        {
            _listingsRepository.IncreaseView(id);
        }

        public void AddToHistory(int listingId)
        {
            User user = GetCurrentUser();

            if(user != null)
            {
                _listingsRepository.AddToHistory(listingId, user.Id);
            }
        }

        public void ChangeCategory(int category, int order)
        {
            List<Listing> list = _listingsRepository.GetListings(category, SortListing(order));

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public List<SubCategory> GetSubcategoriesFromSpecificCategory(int category)
        {
            if (category != 0)
            {
                List<SubCategory> subCategories = _listingsRepository.GetSubcategoriesFromSpecificCategory(category);

                return subCategories;
            }

            return null;
        }

        public void GetFavorites()
        {
            if (GetCurrentUser() != null)
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
}
