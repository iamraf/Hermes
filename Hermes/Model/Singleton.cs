using MySql.Data.MySqlClient;

namespace Hermes.Model
{
    public class Singleton
    {
        private static Singleton Instance;
        private readonly MySqlConnection _connection;
        private Singleton()
        {
            string connectionString = "SERVER=remotemysql.com;DATABASE=4G6ccccjnC;UID=4G6ccccjnC;PASSWORD=l0YkuReQwW;Convert Zero Datetime=True";
            _connection = new MySqlConnection(connectionString);
        }

        public static Singleton GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Singleton();
            }

            return Instance;
        }

        public MySqlConnection GetConnection()
        {
            return _connection;
        }
        public bool OpenConnection()
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

        public bool CloseConnection()
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
    }
}
