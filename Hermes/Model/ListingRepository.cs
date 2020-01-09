using Hermes.Model.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Hermes.Model
{
    class ListingRepository
    {
        public List<Listing> GetListings(int category)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT * FROM Listings l";
                
                if (category != 0)
                {
                    query += " join SubListing_Categories sc on l.subCategoryListing=sc.subcategoryID " +
                        "WHERE sc.categoryID=" + category + "";
                }

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

        public List<Listing> GetSortedListings(string field)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT * FROM Listings ORDER BY " + field;

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

        public User GetUploader(int listingId)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT ownerID FROM Owners_Listings WHERE listingID = '" + listingId + "'";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                int userId = -1;

                dataReader.Read();

                if (dataReader.HasRows)
                {
                    userId = dataReader.GetInt16("ownerID");
                }

                dataReader.Close();

                User user = null;

                query = "SELECT * FROM User_Data WHERE userID = '" + userId + "' LIMIT 1";

                cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                dataReader = cmd.ExecuteReader();

                dataReader.Read();

                if (dataReader.HasRows)
                {
                    user = new User(dataReader.GetInt32("userID"), dataReader.GetString("username"), dataReader.GetString("password"), dataReader.GetString("name"), dataReader.GetString("surname"), dataReader.GetString("address"), dataReader.GetString("email"), dataReader.GetString("telephone"));
                }

                Singleton.GetInstance().CloseConnection();

                return user;
            }
            else
            {
                return null;
            }
        }

        public void AddToFavourite(Favourite favourite)
        {
            if(Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "INSERT INTO User_Favorites (listingID, userID) VALUE ('" + favourite.ListingId + "', '" + favourite.UserId + "')";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();
            }
        }

        public void IncreaseView(int listingId)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "UPDATE Listings SET listViews = listViews + 1 WHERE listingID = '" + listingId + "'";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();
            }
        }

        public List<Listing> GetSearchResult(string search)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                //string joinedQuery = String.Join<char>("%", search);
                string joinedQuery = search;
                string query = "SELECT * FROM Listings WHERE listingName like '%"+ joinedQuery + "%' or listingDescription like '%" + joinedQuery + "%'";
                
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

        public List<Listing> FilteredListings(List<string> catIds, int category)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT l.* FROM Listings l";


                if (category != 0)
                {
                    query += " join SubListing_Categories sc on l.subCategoryListing=sc.subcategoryID " +
                        "WHERE sc.categoryID=" + category + "";
                    if(catIds.Any<string>())
                    {
                        query += " and ";
                    }
                }

                if (catIds.Any<string>())
                {
                    if(category==0)
                    {
                        query += " WHERE ";
                    }
                    string joinedCatIds = String.Join(",", catIds);
                    query += "l.subCategoryListing in (" + joinedCatIds + ")";
                }

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

        public List<Listing> PriceFilteredListings(List<string> catIds, string comparisonOperator, float price, int category)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT l.* FROM Listings l";

                if (category != 0)
                {
                    query += " join SubListing_Categories sc on l.subCategoryListing=sc.subcategoryID " +
                        "WHERE sc.categoryID=" + category + " and ";
                }
                else
                {
                    query += " WHERE ";
                }

                query += "l.price " + comparisonOperator + " " + price + " ";

                if (catIds.Any<string>())
                {
                    string joinedCatIds = String.Join(",", catIds);
                    query += "and l.subCategoryListing in (" + joinedCatIds + ")";
                }

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

        public List<Listing> GetDateFilteredListings(List<string> catIds, string dateOption, int category)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT l.* FROM Listings l"; 

                if (category != 0)
                {
                    query += " join SubListing_Categories sc on l.subCategoryListing=sc.subcategoryID " +
                        "WHERE sc.categoryID=" + category + " and ";
                }
                else
                {
                    query += " WHERE ";
                }

                query += "(l.creationDate between date_sub(now(),INTERVAL 1 " + dateOption + ") and now()) ";

                if (catIds.Any<string>())
                {
                    string joinedCatIds = String.Join(",", catIds);
                    query += "and l.subCategoryListing in (" + joinedCatIds + ") ";
                }
                query += "order by l.creationDate desc";

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

        public List<Listing> GetDateAndPriceFilteredListings(List<string> catIds, string comparisonOperator, float price, string dateOption, int category)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT l.* FROM Listings l";
                
                if(category!=0)
                {
                    query += " join SubListing_Categories sc on l.subCategoryListing=sc.subcategoryID " +
                        "WHERE sc.categoryID="+category+" and ";
                }
                else
                {
                    query += " WHERE ";
                }
                query+= "l.price " + comparisonOperator + " " + price + " " +
                                "and (l.creationDate between date_sub(now(),INTERVAL 1 " + dateOption + ") and now()) ";
                if (catIds.Any<string>())
                {
                    string joinedCatIds = String.Join(",", catIds);
                    query += "and l.subCategoryListing in (" + joinedCatIds + ") ";
                }

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

        public void AddToHistory(int listingId, int userId)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "INSERT INTO View_history (listingID, userID) VALUE ('" + listingId + "', '" + userId + "')";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();
            }
        }
    }
}
