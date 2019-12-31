using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Hermes.Model.Models;

namespace Hermes.Model
{
    class LoginRepository
    {
        private MySqlConnection _connection;
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

        public string UserExist(string _username, string _password)
        {
            if (this.OpenConnection() == true)
            {
                string query = "SELECT count(*) AS Exist FROM User_Data WHERE username = '"+_username+"'  and password = '"+ _password+"'" ;
                string result = null;

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    result = dataReader["Exist"] + "";
                }

                dataReader.Close();

                this.CloseConnection();

                return result;
            }
            else
            {
                return null;
            }
        }

        public User GetUserData(string _username)
        {
            User LoggedInUser=null;

            if (this.OpenConnection() == true)
            {
                string query = "SELECT * FROM User_Data WHERE username = '" + _username +"'";

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    LoggedInUser = new User(dataReader.GetInt32("userID"), dataReader.GetString("username"), dataReader.GetString("password"), dataReader.GetString("name"), dataReader.GetString("surname"), dataReader.GetString("address"), dataReader.GetString("email"), dataReader.GetInt32("telephone"));
                }

                dataReader.Close();

                this.CloseConnection();

                return LoggedInUser;
            }
            else
            {
                return null;
            }

        }

    }
}
