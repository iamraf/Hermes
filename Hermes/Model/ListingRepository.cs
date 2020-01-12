using Hermes.Model.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Hermes.Model
{
    class ListingRepository
    {
        public List<Listing> GetListings(int category, string order)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT l.*, li.* FROM Listings l left outer join Listings_Icons li on li.listingID=l.listingID ";

                if (category != 0)
                {
                    query += " join SubListing_Categories sc on l.subCategoryListing=sc.subcategoryID " +
                        "WHERE sc.categoryID=" + category + "";
                }
                query += " order by " + order + "";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while(dataReader.Read())
                {
                    Listing tmp = new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price"));

                    if(!dataReader.IsDBNull(12))
                    {
                        byte[] b = (byte[])dataReader.GetValue(12);

                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(b);
                        bitmapImage.EndInit();

                        tmp.Image = bitmapImage;
                    }
                    else
                    {
                        tmp.Image = new BitmapImage(new Uri("pack://application:,,,/error.jpg"));
                    }

                    listing.Add(tmp);
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
            DeleteFromFavorite(favourite);
            if(Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "INSERT INTO User_Favorites (listingID, userID, date) VALUE ('" + favourite.ListingId + "', '" + favourite.UserId + "', now())";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();


            }
        }

        public void DeleteFromFavorite(Favourite favourite) //delete all repetitive rows
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "DELETE FROM User_Favorites WHERE userID=" + favourite.UserId + " AND listingID=" + favourite.ListingId;

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();
            }
        }

        public void DeleteAllFromFavorite(Favourite favourite) //delete all user history
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "DELETE FROM User_Favorites WHERE userID=" + favourite.UserId;

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
               
                string query = "SELECT l.*, li.* FROM Listings l left outer join Listings_Icons li on li.listingID=l.listingID WHERE l.listingName like @search or l.listingDescription like @search ";
                
                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@search", "%"+search+"%" );
                //cmd.Parameters.AddWithValue("@search2", "%" + search + "%");
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while (dataReader.Read())
                {
                    Listing tmp = new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price"));

                    if (!dataReader.IsDBNull(12))
                    {
                        byte[] b = (byte[])dataReader.GetValue(12);

                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(b);
                        bitmapImage.EndInit();

                        tmp.Image = bitmapImage;
                    }
                    else
                    {
                        tmp.Image = new BitmapImage(new Uri("pack://application:,,,/error.jpg"));
                    }

                    listing.Add(tmp);
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

        public List<Listing> FilteredListings(List<string> catIds, int category, string order)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT l.*, li.* FROM Listings l left outer join Listings_Icons li on li.listingID=l.listingID ";


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
                query += " order by " + order + "";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while (dataReader.Read())
                {
                    Listing tmp = new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price"));

                    if (!dataReader.IsDBNull(12))
                    {
                        byte[] b = (byte[])dataReader.GetValue(12);

                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(b);
                        bitmapImage.EndInit();

                        tmp.Image = bitmapImage;
                    }
                    else
                    {
                        tmp.Image = new BitmapImage(new Uri("pack://application:,,,/error.jpg"));
                    }

                    listing.Add(tmp);
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

        public List<Listing> PriceFilteredListings(List<string> catIds, string comparisonOperator, int price, int category, string order)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT l.*, li.* FROM Listings l left outer join Listings_Icons li on li.listingID=l.listingID ";

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
                query += " order by " + order + "";
                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while (dataReader.Read())
                {
                    Listing tmp = new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price"));

                    if (!dataReader.IsDBNull(12))
                    {
                        byte[] b = (byte[])dataReader.GetValue(12);

                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(b);
                        bitmapImage.EndInit();

                        tmp.Image = bitmapImage;
                    }
                    else
                    {
                        tmp.Image = new BitmapImage(new Uri("pack://application:,,,/error.jpg"));
                    }

                    listing.Add(tmp);
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

        public List<Listing> GetDateFilteredListings(List<string> catIds, string dateOption, int category, string order)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT l.*, li.* FROM Listings l left outer join Listings_Icons li on li.listingID=l.listingID ";

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
                query += " order by " + order + "";
                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while (dataReader.Read())
                {
                    Listing tmp = new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price"));

                    if (!dataReader.IsDBNull(12))
                    {
                        byte[] b = (byte[])dataReader.GetValue(12);

                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(b);
                        bitmapImage.EndInit();

                        tmp.Image = bitmapImage;
                    }
                    else
                    {
                        tmp.Image = new BitmapImage(new Uri("pack://application:,,,/error.jpg"));
                    }

                    listing.Add(tmp);
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

        public List<Listing> GetDateAndPriceFilteredListings(List<string> catIds, string comparisonOperator, float price, string dateOption, int category, string order)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT l.*, li.* FROM Listings l left outer join Listings_Icons li on li.listingID=l.listingID ";

                if (category!=0)
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
                query += " order by " + order + "";
                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while (dataReader.Read())
                {
                    Listing tmp = new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price"));

                    if (!dataReader.IsDBNull(12))
                    {
                        byte[] b = (byte[])dataReader.GetValue(12);

                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(b);
                        bitmapImage.EndInit();

                        tmp.Image = bitmapImage;
                    }
                    else
                    {
                        tmp.Image = new BitmapImage(new Uri("pack://application:,,,/error.jpg"));
                    }

                    listing.Add(tmp);
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
            DeleteFromHistory(listingId, userId);
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "INSERT INTO View_history (listingID, userID, date) VALUE ('" + listingId + "', '" + userId + "', now())";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();
            }
        }

        private void DeleteFromHistory(int listingId, int userId) //delete all repetitive rows
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "DELETE FROM View_history WHERE userID=" + userId + " AND listingID=" + listingId;

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();
            }
        }

        public void DeleteFromHistory(int userId) //delete all user history
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "DELETE FROM View_history WHERE userID=" + userId;

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();
            }
        }

        public List<SubCategory> GetSubcategoriesFromSpecificCategory(int category)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "select sc.subcategoryID, sc.subcategoryID, sc.subcategoryName  " +
                    "from Listing_Categories lc join SubListing_Categories sc on lc.categoryID=sc.categoryID " +
                    "where lc.categoryID = "+category;

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<SubCategory> subCategories = new List<SubCategory>();

                while (dataReader.Read())
                {
                    subCategories.Add(new SubCategory(dataReader.GetInt32("subcategoryID"), dataReader.GetInt32("subcategoryID"), dataReader.GetString("subcategoryName")));
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return subCategories;
            }
            else
            {
                return null;
            }
        }

        public Byte[] GetImage(int listingId)
        {
            if(Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT * FROM Listings_Icons WHERE listingID = " + listingId + " LIMIT 1";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
