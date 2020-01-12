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

        public int ListingId { get; }
        public int UserId { get; }

        public Favourite(int listingId, int userId)
        {
            ListingId = listingId;
            UserId = userId;

            //if (ItemAlreadyOnFavourites(userId, listingId)==false)
            //{
            //    if (AddItemOnFavourites() == true)
            //        Console.WriteLine("Item added on fav");
            //    else
            //        Console.WriteLine("Item could not be added on fav");
            //}
            //else
            //    Console.WriteLine("Item already on favourites");


        }

        //private bool AddItemOnFavourites()
        //{
        //    if(Singleton.GetInstance().OpenConnection()==true)
        //    {
        //        string query = "INSERT INTO User_Favorites (listingID, userID) VALUE ("+ListingId+", "+UserId+")";
               
        //        MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        dataReader.Close();

        //        Singleton.GetInstance().CloseConnection();

        //        return true;
        //    }
        //    else
        //        return false;
        //}

        //private bool ItemAlreadyOnFavourites(int uid, int listingid)
        //{
        //    if (Singleton.GetInstance().OpenConnection() == true)
        //    {
        //        bool result = false;
        //        string query = "SELECT EXISTS (SELECT 1 FROM 4G6ccccjnC.User_Favorites WHERE userID="+uid+" AND listingID="+listingid+")";

        //        MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        while (dataReader.Read())
        //        {
        //            result = dataReader.GetBoolean(0);
        //        }

        //        dataReader.Close();

        //        Singleton.GetInstance().CloseConnection();

        //        return result;
        //    }
        //    else
        //        return false;
        //}
    }
}
