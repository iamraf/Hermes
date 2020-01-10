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
                //hash paswd 
                _password = PasswordHashing.hashPassword(_password);
                //prepare query
                string query = "INSERT INTO User_Data (username, password, name, surname, address, email, telephone1, telephone) VALUE (@username, @password , @name , @surname ,@address,@email, 0,@telephone)";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", _username);
                cmd.Parameters.AddWithValue("@password", _password);
                cmd.Parameters.AddWithValue("@name", _name);
                cmd.Parameters.AddWithValue("@surname", _surname);
                cmd.Parameters.AddWithValue("@address", _address);
                cmd.Parameters.AddWithValue("@email", _email);
                cmd.Parameters.AddWithValue("@telephone", _telephone);
              
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
            string query = "SELECT EXISTS(SELECT 1 FROM User_Data WHERE username=@name )";
            
            MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@name", name);
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
