using Hermes.Model.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model
{
    class Repository
    {
        private MySqlConnection _connection;

        public Repository()
        {
            string connectionString = "SERVER=remotemysql.com;DATABASE=4G6ccccjnC;UID=4G6ccccjnC;PASSWORD=l0YkuReQwW;";

            _connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                _connection.Open();

                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                _connection.Close();

                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public List<User> GetUsers()
        {
            if (this.OpenConnection() == true)
            {
                string query = "SELECT * FROM User_Data";

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<User> users = new List<User>();

                while(dataReader.Read())
                {
                    users.Add(new User(dataReader.GetInt32("userID"), dataReader.GetString("username"), dataReader.GetString("password"), dataReader.GetString("name"), dataReader.GetString("surname"), dataReader.GetString("address"), dataReader.GetString("email"), dataReader.GetInt32("telephone1")));
                }

                dataReader.Close();

                this.CloseConnection();

                return users;
            }
            else
            {
                return null;
            }
        }

        public List<Listing> GetListings()
        {
            if (this.OpenConnection() == true)
            {
                string query = "SELECT * FROM Listings";

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while (dataReader.Read())
                {
                    listing.Add(new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing"))));
                }

                dataReader.Close();

                this.CloseConnection();

                return listing;
            }
            else
            {
                return null;
            }
        }
    }
}
