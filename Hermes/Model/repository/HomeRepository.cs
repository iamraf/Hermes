using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using Hermes.Model.Models;
using MySql.Data.MySqlClient;

/*
 * Home Repository:
 * HomeRepository class is responsible for 
 * getting everything homepage needs from the database
 */
namespace Hermes.Model
{
    class HomeRepository
    {
        public List<Listing> GetRecommended(User user)
        {
            List<HistoryList> history = GetViewHistory(user);

            int ViewedCat = history.Count;
            int[] count;

            if (ViewedCat <= 2)
            {
                count = new int[3];
                for (int i = 0; i < 2; i++)
                {
                    count[i] = history[i].Category;
                }
                for (int i = ViewedCat; i < 3; i++)
                {
                    count[i] = count[1];
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
                string query = "SELECT * FROM Listings left outer join Listings_Icons on Listings_Icons.listingID=Listings.listingID ORDER BY listViews DESC LIMIT 5";

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
                dataReader.Dispose();
                cmd.Dispose();

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
                string query = "select count(View_history.listingID)as view ,Listings.subCategoryListing as category " +
                                " from View_history " +
                                    " join Listings on View_history.listingID = Listings.listingID " +
                                    " join SubListing_Categories on SubListing_Categories.subcategoryID = Listings.subCategoryListing " +
                                " where userID = " + user.Id +
                                " group by Listings.subCategoryListing " +
                                " order by count(View_history.listingID) DESC ";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<HistoryList> history = new List<HistoryList>();

                while(dataReader.Read())
                {
                    history.Add(new HistoryList(dataReader.GetInt32("view"), dataReader.GetInt32("category")));
                }

                dataReader.Close();
                dataReader.Dispose();
                cmd.Dispose();

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
                listings = AddListings(listings, cat[i], i);
            }
          

            return listings;
        }

        private List<Listing> AddListings(List<Listing> listings, int index, int i)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "select  * " +
                                "from Listings left outer join Listings_Icons on Listings_Icons.listingID=Listings.listingID " +
                                "where Listings.subCategoryListing=" + index + " and Listings.premiumListing=1 and activeListing=1 " +
                                "order by creationDate DESC " +
                                "LIMIT " + (3 - i) + ";";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //TODO:DELETE this line  List<Listing> listing = new List<Listing>();

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

                    listings.Add(tmp);
                }

                dataReader.Close();
                dataReader.Dispose();
                cmd.Dispose();

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
