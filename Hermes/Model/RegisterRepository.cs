using MySql.Data.MySqlClient;

namespace Hermes.Model
{
    class RegisterRepository
    {

        private MySqlConnection _connection;
        private string _username, _password, _name, _surname, _address, _email, _telephone;

        public RegisterRepository(string username, string password, string name, string surname, string address, string email, string telephone)
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
                if (CheckIfUserExists(_username))
                    throw new System.Exception("User already exists");

                string query = "INSERT INTO User_Data (username, password, name, surname, address, email, telephone1, telephone) VALUE ('" + _username + "', '" + _password + "', '" + _name + "', '" + _surname + "', '" + _address + "', '" + _email + "', 0, '" + _telephone + "')";

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read()) { }

                dataReader.Close();

                this.CloseConnection();

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckIfUserExists(string name)
        {
            bool result = false;
            string query = "SELECT EXISTS(SELECT 1 FROM User_Data WHERE username='" + name + "')";

            MySqlCommand cmd = new MySqlCommand(query, _connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                result = dataReader.GetBoolean(0);
            }

            dataReader.Close();

            return result;
        }

        private void LoadFromFields(string username, string password, string name, string surname, string address, string email, string telephone)
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
