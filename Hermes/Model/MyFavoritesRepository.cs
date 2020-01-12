using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hermes.Model.Models
{
    class MyFavoritesRepository
    {

        public List<Listing> GetListings(int userId)
        {
            if (Singleton.GetInstance().OpenConnection() == true)
            {
                string query = "SELECT DISTINCT * FROM Listings L left outer join Listings_Icons on Listings_Icons.listingID=L.listingID JOIN User_Favorites UF ON L.listingID = UF.ListingID WHERE UF.userID=" + userId + " ORDER BY UF.date DESC";

                MySqlCommand cmd = new MySqlCommand(query, Singleton.GetInstance().GetConnection());
                MySqlDataReader dataReader = cmd.ExecuteReader();

                List<Listing> listing = new List<Listing>();

                while (dataReader.Read())
                {
                    Listing tmp = new Listing(dataReader.GetInt32("listingID"), dataReader.GetString("listingName"), dataReader.GetString("listingDescription"), Convert.ToBoolean(dataReader.GetInt32("activeListing")), dataReader.GetInt32("listingRegion"), dataReader.GetInt32("listViews"), dataReader.GetInt32("subCategoryListing"), Convert.ToBoolean(dataReader.GetInt16("premiumListing")), dataReader.GetDateTime("creationDate"), dataReader.GetInt32("price"), dataReader.GetBoolean("types"));

                    if (!dataReader.IsDBNull(12))
                    {
                        byte[] b = (byte[])dataReader.GetValue(12);

                        var bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(b);
                        bitmapImage.EndInit();

                        tmp.Image = bitmapImage;
                    }
                    else
                    {
                        tmp.Image = new BitmapImage(new Uri("pack://application:,,,/error.jpg"));
                    }

                    listing.Add(tmp);
                }

                dataReader.Close();

                Singleton.GetInstance().CloseConnection();

                return listing;
            }
            else
            {
                return null;
            }
        }

    }
}
