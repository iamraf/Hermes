using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Model.Models;
using MySql.Data.MySqlClient;

namespace Hermes.Model
{
    class LoginRepository
    {
        private readonly MySqlConnection _connection;
        public LoginRepository()
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

        public bool UserExist(string username)
        {
            if(this.OpenConnection() == true)
            {
                string query = "SELECT * FROM User_Data WHERE username = '" + username + "'" ;

                bool exists = false;

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if(dataReader.HasRows)
                {
                    exists = true;
                }

                dataReader.Close();

                this.CloseConnection();

                return exists;
            }
            else
            {
                return false;
            }
        }

        public User LoginUser(string username, string password)
        {
            if (this.OpenConnection() == true)
            {
                User user = null;

                string query = "SELECT * FROM User_Data WHERE username = '" + username + "' and password = '" + password + "'";

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while(dataReader.Read())
                {
                    user = new User(dataReader.GetInt32("userID"), dataReader.GetString("username"), dataReader.GetString("password"), dataReader.GetString("name"), dataReader.GetString("surname"), dataReader.GetString("address"), dataReader.GetString("email"), dataReader.GetInt32("telephone"));
                }

                dataReader.Close();

                this.CloseConnection();

                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
