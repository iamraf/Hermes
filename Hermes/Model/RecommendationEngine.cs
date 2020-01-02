using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Model.Models;
using MySql.Data.MySqlClient;
namespace Hermes.Model
{
    class RecommendationEngine
    {
        private User user;

        public RecommendationEngine(User user)
        {
            this.user = user;
        }

        public List<Listing> getRecommendCat()
        {
            List<HistoryList> history = _viewHistory();
            int ViewedCat = history.Count;
            int[] count;
            if (ViewedCat <= 3)
            {
                count = new int[ViewedCat];
                for (int i = 0; i < ViewedCat; i++)
                {
                    count[i] = history[i].Category;
                }
            }
            else
            {
                count = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    count[i] = history[i].Category;
                }
            }

            List<Listing> returnedCat = new List<Listing>();
            returnedCat = _getListingsByCategories(count);
            //TODO na teleiwsw afto to kommati
            return returnedCat;
        }

        private List<HistoryList> _viewHistory()
        {
            
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "select  count(View_history.listingID)as view ,Listings.subCategoryListing as category" +
                                "from View_history" +
                                    "join Listings on View_history.listingID = Listings.listingID" +
                                    "join SubListing_Categories on SubListing_Categories.subcategoryID = Listings.subCategoryListing" +
                                "where userID = " + user.Id +
                                "group by Listings.subCategoryListing;" +
                                "order by count(View_history.listingID) DESC;";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<HistoryList> _arrayHistory= new List<HistoryList>();

                while (dataReader.Read())
                {
                    _arrayHistory.Add(new HistoryList(dataReader.GetInt32("view"), dataReader.GetInt32("category")));
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return _arrayHistory;
            }
            else
            {
                return null;
            }

        }

        private List<Listing> _getListingsByCategories( int[] cat) 
        {
            List<Listing> _listings = new List<Listing>();
            for (int i = 0; i < 3; i++) { _listings = addListings(_listings, cat[i]); }
            return _listings;
        }
        private List<Listing> addListings(List<Listing> _listings, int index) {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "select  *" +
                                "from Listings" +
                                "where Listings.subCategoryListing=" + index + " and Listings.premiumListing=1 and activeListing=1" +
                                "order by creationDate DESC" +
                                "LIMIT "+(3-index)+";";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    _listings.Add(new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price")));
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();
                return _listings;

            }
            else { return null; }
        }
    }
}
