using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.View;
using Hermes.Model.Models;
using Hermes.Model;
using MySql.Data.MySqlClient;
namespace Hermes.Model
{
    class MyListingsRepository
    {
        public List<Listing> GetListings(int UserID, int activeListing)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "select  * "+
                               "from Listings join Owners_Listings OL on Listings.listingID = OL.listingID "+
                               "where OL.ownerID = "+UserID+" and Listings.activeListing = "+activeListing;

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
