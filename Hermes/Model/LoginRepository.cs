﻿using Hermes.Model.Models;
using MySql.Data.MySqlClient;

namespace Hermes.Model
{
    class LoginRepository
    {
            
        public bool UserExist(string username)
        {
            if(Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT * FROM User_Data WHERE username = @username" ;

                bool exists = false;

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());

                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", username);
                //cmd.Prepare();
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if(dataReader.HasRows)
                {
                    exists = true;
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return exists;
            }
            else
            {
                return false;
            }
        }

        public User LoginUser(string username, string password)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                User user = null;

                string query = "SELECT * FROM User_Data WHERE username = @username and password = @password ";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while(dataReader.Read())
                {
                    user = new User(dataReader.GetInt32("userID"), dataReader.GetString("username"), dataReader.GetString("password"), dataReader.GetString("name"), dataReader.GetString("surname"), dataReader.GetString("address"), dataReader.GetString("email"), dataReader.GetString("telephone"));
                }

                dataReader.Close();

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
