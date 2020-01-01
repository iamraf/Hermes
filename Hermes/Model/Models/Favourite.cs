using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    class Favourite
    {
        private MySqlConnection _connection;
        public int ListingId { get; }
        public int UserId { get; }

        public Favourite(int listingId, int userId)
        {
            ListingId = listingId;
            UserId = userId;

            string connectionString = "SERVER=remotemysql.com;DATABASE=4G6ccccjnC;UID=4G6ccccjnC;PASSWORD=l0YkuReQwW;";
            _connection = new MySqlConnection(connectionString);

            if (AddItemOnFavourites() == true)
                //show done
                Console.WriteLine("Item added on fav");
            else
                //not done
                Console.WriteLine("Item could not be added on fav");
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

        private bool AddItemOnFavourites()
        {
            if(this.OpenConnection() == true)
            {
                string query = "INSERT INTO User_Favorites (listingID, userID) VALUE ("+ListingId+", "+UserId+")";
               
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read()) { }

                dataReader.Close();

                this.CloseConnection();

                return true;
            }
            else
                return false;
        }
    }
}
