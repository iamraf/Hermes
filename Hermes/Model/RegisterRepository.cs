using MySql.Data.MySqlClient;

namespace Hermes.Model
{
    class RegisterRepository
    {

        private string _username, _password, _name, _surname, _address, _email, _telephone;

        public RegisterRepository(string username, string password, string name, string surname, string address, string email, string telephone)
        {
            _username = username;
            _password = password;
            _name = name;
            _surname = surname;
            _address = address;
            _email = email;
            _telephone = telephone;

        }

        public int RegisterQuery()
        {
            if (Singleton.GetInstance().OpenConnection()==true)
            {
                if (UserExists(_username))
                    return -2; //user already exists

                string query = "INSERT INTO User_Data (username, password, name, surname, address, email, telephone1, telephone) VALUE ('" + _username + "', '" + _password + "', '" + _name + "', '" + _surname + "', '" + _address + "', '" + _email + "', 0, '" + _telephone + "')";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //todo: check if user is registered

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return 0;
            }
            else
            {
                return -1;
            }
        }

        private bool UserExists(string name)
        {

            bool result = false;
            string query = "SELECT EXISTS(SELECT 1 FROM User_Data WHERE username='" + name + "')";

            MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                result = dataReader.GetBoolean(0);
            }

            dataReader.Close();

            return result;

        }


    }
}
