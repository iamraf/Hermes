using MySql.Data.MySqlClient;

namespace Hermes.Model
{
    class Repository
    {
        private MySqlConnection _connection;

        public Repository()
        {
            string connectionString = "SERVER=remotemysql.com;DATABASE=VaswDXa8SR;UID=VaswDXa8SR;PASSWORD=sTnFsjGaOs;";

            _connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                _connection.Open();

                return true;
            }
            catch(MySqlException)
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
            catch(MySqlException)
            {
                return false;
            }
        }

        public string LoadName()
        {
            if(this.OpenConnection() == true)
            {
                string query = "SELECT * FROM Test WHERE id = 1";
                string result = null;

                MySqlCommand cmd = new MySqlCommand(query, _connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while(dataReader.Read())
                {
                    result = dataReader["name"] + "";
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
