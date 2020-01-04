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

        private bool AddItemOnFavourites()
        {
            if(Singleton.GetInstance().OpenConnection()==true)
            {
                string query = "INSERT INTO User_Favorites (listingID, userID) VALUE ("+ListingId+", "+UserId+")";
               
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return true;
            }
            else
                return false;
        }
    }
}
