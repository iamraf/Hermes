using System;
using System.Collections.Generic;
using Hermes.Model.Models;
using MySql.Data.MySqlClient;
namespace Hermes.Model
{
    class HomeRepository
    {
        public List<Listing> GetRecommended(User user)
        {
            List<HistoryList> history = GetViewHistory(user);

            int ViewedCat = history.Count;
            int[] count;

            if (ViewedCat <= 3)
            {
                count = new int[ViewedCat];

                for (int i = 0; i < ViewedCat; i++)
                {
                    count[i] = history[i].Category;
                }
            }
            else
            {
                count = new int[3];

                for (int i = 0; i < 3; i++)
                {
                    count[i] = history[i].Category;
                }
            }

            return GetListingsByCategories(count);
        }

        public List<Listing> GetPopular()
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT * FROM Listings";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while (dataReader.Read())
                {
                    listing.Add(new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price")));
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return listing;
            }
            else
            {
                return null;
            }
        }

        private List<HistoryList> GetViewHistory(User user)
        {
            if(Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "select count(View_history.listingID)as view ,Listings.subCategoryListing as category" +
                                " from View_history" +
                                    " join Listings on View_history.listingID = Listings.listingID" +
                                    " join SubListing_Categories on SubListing_Categories.subcategoryID = Listings.subCategoryListing" +
                                " where userID = " + user.Id +
                                " group by Listings.subCategoryListing;" +
                                " order by count(View_history.listingID) DESC;";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<HistoryList> history = new List<HistoryList>();

                while(dataReader.Read())
                {
                    history.Add(new HistoryList(dataReader.GetInt32("view"), dataReader.GetInt32("category")));
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return history;
            }
            else
            {
                return null;
            }
        }

        private List<Listing> GetListingsByCategories( int[] cat) 
        {
            List<Listing> listings = new List<Listing>();

            for(int i = 0; i < 3; i++)
            {
                listings = AddListings(listings, cat[i]);
            }

            return listings;
        }

        private List<Listing> AddListings(List<Listing> listings, int index)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "select  *" +
                                "from Listings" +
                                "where Listings.subCategoryListing=" + index + " and Listings.premiumListing=1 and activeListing=1" +
                                "order by creationDate DESC" +
                                "LIMIT "+(3-index)+";";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    listings.Add(new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price")));
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return listings;
            }
            else
            { 
                return null;
            }
        }
    }
}
