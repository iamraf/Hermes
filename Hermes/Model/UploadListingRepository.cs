using Hermes.Model.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model
{
    class UploadListingRepository
    {

        public UploadListingRepository()
        {

        }

        public List<Category> GetCategories()
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT * FROM Listing_Categories";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Category> category = new List<Category>();

                while (dataReader.Read())
                {
                    category.Add(new Category(dataReader.GetInt32("categoryID"), dataReader.GetString("categoryName")));
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return category;

            }
            else
            {
                return null;
            }
        }

        public List<SubCategory> GetSubCategories(int categoryID)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT * FROM SubListing_Categories WHERE CategoryID="+categoryID;

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<SubCategory> subcategory = new List<SubCategory>();

                while (dataReader.Read())
                {
                    subcategory.Add(new SubCategory(dataReader.GetInt32("subcategoryID"), dataReader.GetInt32("categoryID"), dataReader.GetString("subcategoryName")));
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return subcategory;

            }
            else
            {
                return null;
            }
        }

        public List<Location> GetLocations()
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT * FROM Location_List";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Location> location = new List<Location>();

                while (dataReader.Read())
                {
                    location.Add(new Location(dataReader.GetInt32("locationID"), dataReader.GetString("locationName"), dataReader.GetInt32("locationTK")));
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return location;

            }
            else
            {
                return null;
            }
        }

        public List<String> GetLocationDistinctNames()
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT DISTINCT locationName FROM Location_List ORDER BY locationName";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<string> location = new List<string>();

                while (dataReader.Read())
                {
                    location.Add((string)dataReader.GetString("locationName"));
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return location;

            }
            else
            {
                return null;
            }
        }

        public bool UploadListing(string listingName, string listingDescription, int listingRegion, int subCategoryListing, bool premiumListing, float price)
        {

            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "INSERT INTO Listings(listingName, listingDescription, activeListing, listingRegion, listViews, subCategoryListing, premiumListing, creationDate, price) VALUE ('"+ listingName + "', '"+ listingDescription + "', 1, '"+ listingRegion + "', 0, '"+ subCategoryListing + "', "+premiumListing+", now(), "+price+")";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return true;

            }
            else
            {
                return false;
            }
        }

    }



}
