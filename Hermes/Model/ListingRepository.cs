using Hermes.Model.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model
{
    class ListingRepository
    {
        public ListingRepository()
        {

        }

        public List<Listing> GetListings()
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT * FROM Listings";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while (dataReader.Read())
                {
                    listing.Add(new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing"))));
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

                if(dataReader.HasRows)
                {
                    user = new User(dataReader.GetInt32("userID"), dataReader.GetString("username"), dataReader.GetString("password"), dataReader.GetString("name"), dataReader.GetString("surname"), dataReader.GetString("address"), dataReader.GetString("email"), dataReader.GetInt32("telephone1"));
                }

                Singleton.GetInstance().CloseConnection();

                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
