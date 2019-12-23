using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace Hermes.Model
{
    class Login
    {
        private MySqlConnection _connection;
        private string _username;
        private string _password;
        public Login() 
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

        public string LoadName()
        {
            if (this.OpenConnection() == true)
            {
                string query = "SELECT count(*) FROM User_Data WHERE username = '"+_username+"'  and password = '"+ _password+"'" ;
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

    }
}
