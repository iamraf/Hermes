using Hermes.Model.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Hermes.Model
{
    class MyProfileRepository
    {
        public List<Location> GetLocations()
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT * FROM Location_List ORDER BY locationName";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Location> location = new List<Location>();

                while (dataReader.Read())
                {
                    location.Add(new Location(dataReader.GetInt32("locationID"), dataReader.GetString("locationName"),dataReader.GetInt32("locationTK")));
                }

                dataReader.Close();
                dataReader.Dispose();
                cmd.Dispose();

                Singleton.GetInstance().CloseConnection();

                return location;
            }
            else
            {
                return null;
            }
        }

        public bool EditUser(User user)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "UPDATE User_Data "+
                    "SET username='"+user.Username+"', password='"+user.Password+"', name='"+user.Name+"', surname='"+user.Surname+"', address='"+user.Address+"', email='"+user.Email+"', telephone1=0, telephone='"+user.Telephone+"' "+
                    "WHERE userId="+ user.Id;

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();
                dataReader.Dispose();
                cmd.Dispose();

                Singleton.GetInstance().CloseConnection();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
