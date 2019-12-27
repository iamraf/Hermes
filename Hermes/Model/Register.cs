using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model
{
    class Register
    {
        private MySqlConnection _connection;
        private string _username, _password, _name, _surname, _address, _email; 
        private int _telephone;

        public Register(string username, string password, string name, string surname, string address, string email, int telephone)
        {
            string connectionString = "SERVER=remotemysql.com;DATABASE=4G6ccccjnC;UID=4G6ccccjnC;PASSWORD=l0YkuReQwW;";
            _connection = new MySqlConnection(connectionString);

            LoadFromFields(username, password, name, surname, address, email, telephone);

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

        public bool RegisterQuery()
        {
            if (this.OpenConnection() == true)
            {
                string query = "INSERT INTO User_Data (username, password, name, surname, address, email, telephone) VALUE ('"+_username+"', '"+_password+"', '"+_name+"', '"+_surname+"', '"+_address+"', '"+_email+"', "+_telephone+")";

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read()){}

                dataReader.Close();

                this.CloseConnection();

                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadFromFields(string username, string password, string name, string surname, string address, string email, int telephone)
        {
            _username = username;
            _password = password;
            _name = name;
            _surname = surname;
            _address = address;
            _email = email;
            _telephone = telephone;
        }

    }
}
