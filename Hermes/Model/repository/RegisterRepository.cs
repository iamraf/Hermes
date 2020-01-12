using Hermes.Model.Models;
using Hermes.Util;
using MySql.Data.MySqlClient;

namespace Hermes.Model
{
    class RegisterRepository
    {
        public int RegisterUser(User user)
        {
            if (Singleton.GetInstance().OpenConnection()==true)
            {
                if (UserExists(user.Username))
                {
                    return -2;
                }

                string password = HashingHelper.HashPassword(user.Password);

                string query = "INSERT INTO User_Data (username, password, name, surname, address, email, telephone1, telephone) VALUE (@username, @password , @name , @surname ,@address,@email, 0,@telephone)";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@name", user.Name);
                cmd.Parameters.AddWithValue("@surname", user.Surname);
                cmd.Parameters.AddWithValue("@address", user.Address);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@telephone", user.Telephone);
              
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //TODO: check if user is registered

                dataReader.Close();
                dataReader.Dispose();
                cmd.Dispose();

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
            dataReader.Dispose();
            cmd.Dispose();

            Singleton.GetInstance().CloseConnection();

            return result;
        }
    }
}
