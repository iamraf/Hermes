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

        public ListingsPresenter(IListingsView view)
        {
            _view = view;
            _repository = new ListingRepository();
        }

        public void GetListings()
        {
            List<Listing> list = _repository.GetListings(0);

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

        public void GetSearchResults(string query)
        {
            List<Listing> list = _repository.GetSearchResult(query);

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public void GetFilteredListings(List<string> catIds, int category)
        {
            List<Listing> list = _repository.FilteredListings(catIds, category);

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public void PriceFilteredListings(List<string> catIds, int priceOption, int category)
        {
            switch (priceOption)
            {
                case 1:
                    GetPriceFilteredListings(catIds, "<=", 100, category);
                    break;
                case 2:
                    GetPriceFilteredListings(catIds, ">", 100, category);
                    break;
                default:
                    GetPriceFilteredListings(catIds, "=", 0, category);
                    break;
            }
        }

        public void DynamicPriceFilteredListings(List<string> catIds, float price, int category)
        {
            GetPriceFilteredListings(catIds, ">=", price, category);
        }

        private void GetPriceFilteredListings(List<string> catIds, string comparisonOperator, float price, int category)
        {
            List<Listing> list = _repository.PriceFilteredListings(catIds, comparisonOperator, price, category);

            if (list != null)
            {
                _view.Listings = list;
            }
        }

        public void DateFilteredListings(List<string> catIds, int dateOption, int category)
        {
            switch (dateOption)
            {
                case 1:
                    GetDateFilteredListings(catIds, "MONTH", category);
                    break;
                case 2:
                    GetDateFilteredListings(catIds, "YEAR", category);
                    break;
                default:
                    GetDateFilteredListings(catIds, "WEEK", category);
                    break;
            }
        }

        public void DateAndPriceFilteredListings(List<string> catIds, int priceOption, int dateOption, int category)
        {
            string date = "";

            switch (dateOption)
            {
                case 1:
                    date = "MONTH";
                    break;
                case 2:
                    date = "YEAR";
                    break;
                default:
                    date = "WEEK";
                    break;
            }
             
            switch (priceOption)
            {
                case 1:
                    GetDateAndPriceFilteredListings(catIds, "<=", 100, date, category);
                    break;
                case 2:
                    GetDateAndPriceFilteredListings(catIds, ">", 100, date, category);
                    break;
                default:
                    GetDateAndPriceFilteredListings(catIds, "=", 0, date, category);
                    break;
            }
        }

        private void GetDateAndPriceFilteredListings(List<string> catIds, string comparisonOperator, float price, string dateOption, int category)
        {
            List<Listing> list = _repository.GetDateAndPriceFilteredListings(catIds, comparisonOperator, price, dateOption, category);
            
            if (list != null)
            {
                _view.Listings = list;
            }
        }

        private void GetDateFilteredListings(List<string> catIds, string dateOption, int category)
        {
            List<Listing> list = _repository.GetDateFilteredListings(catIds, dateOption, category);

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

        public void ChangeCategory(int category)
        {
            List<Listing> list = _repository.GetListings(category);

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
    }
}
