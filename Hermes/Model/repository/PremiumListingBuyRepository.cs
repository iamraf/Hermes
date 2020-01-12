using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.repository
{
    class PremiumListingBuyRepository
    {
        public Boolean addPremiumListings(int listings, int uid)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query =  " UPDATE User_Data "+
                                " SET premiumListingsAvailable =premiumListingsAvailable+"+listings+" "+
                                " WHERE userID = "+uid+";";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                cmd.ExecuteReader();
                Singleton.GetInstance().CloseConnection();
            }

            return false;
        }
    }
}
