using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.View;
using Hermes.Model.Models;
using Hermes.Model;
using MySql.Data.MySqlClient;
using System.Windows.Media.Imaging;
using System.IO;
/*
 * MyListings repository:
 * MyListings class is responsible for 
 * getting user's listings from the database
 */
namespace Hermes.Model
{
    class MyListingsRepository
    {
        public List<Listing> GetListings(int UserID, int activeListing)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "select * " +
                               "from Listings left outer join Listings_Icons on Listings_Icons.listingID=Listings.listingID join Owners_Listings OL on Listings.listingID = OL.listingID " +
                               "where OL.ownerID = "+UserID;
                if(activeListing!=-1)
                {
                    query += " and Listings.activeListing = " + activeListing;
                }

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while (dataReader.Read())
                {
                    Listing tmp = new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price"), dataReader.GetBoolean("types"));

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

        public Boolean UpdateListing(int listingID, string title, float price, string description)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "UPDATE Listings SET listingName='" +title+"' ,listingDescription='"+description+"', price="+price+
                               " WHERE listingID = " + listingID;

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                Singleton.GetInstance().CloseConnection();
                return true;                
            }
            else
            {
                return false;
            }
        }
        public void deleteListing(int listingID) 
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "UPDATE Listings SET activeListing = 0 "+
                               "WHERE listingID = "+listingID;

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                

                Singleton.GetInstance().CloseConnection();

                //return listing;
            }
            else
            {
                //return null;
            }
        }
    }
}
