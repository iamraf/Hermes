using Hermes.Model;
using Hermes.Model.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Windows;
/* MyListingsPresenter connect view with model classes
 *  it gets data from the repositories 
 *  and pass them to view
 */

namespace Hermes.View.mylistings
{
    class MyListingsPresenter
    {
        private readonly IMyListingsView _view;
        private readonly MyListingsRepository _repository;
        private readonly UploadRepository _uploadRepository;

        public MyListingsPresenter(IMyListingsView view)
        {
            _view = view;
            _repository = new MyListingsRepository();
            _uploadRepository = new UploadRepository();
        }

        public void GetListings(int activeListing)
        {
            User user = GetCurrentUser();

            List<Listing> list = _repository.GetListings(user.Id, activeListing);
            if (list != null && list.Count > 0)
            {
                _view.Listings = list;
            }
        }

        public void UpdateListing(int id, string title, float price, string description, string path)
        {
            bool update = _repository.UpdateListing(id, title, price, description);
            UploadImage(id, path);
            if (update)
            {
                MessageBox.Show("Listing updated!", "Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("ERROR!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            GetListings(1);
        }

        public void DeactivateListing(int listingID)
        {
            _repository.deleteListing(listingID);
        }

        public User GetCurrentUser()
        {
            return (User)MemoryCache.Default["User"];
        }

        public void Logout()
        {
            ObjectCache Cache = MemoryCache.Default;
            if (Cache["User"] != null)
                Cache.Remove("User");
        }

        public void UploadImage(int listingId, string imagePath)
        {
            if (imagePath != null)
            {
                FileStream fs;
                BinaryReader br;

                string FileName = imagePath;
                byte[] ImageData;
                fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                ImageData = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                if (_uploadRepository.UploadImage(listingId, ImageData) == false)
                {
                    MessageBox.Show("Could not upload image!", "Error");
                }
            }
            else
            {
                MessageBox.Show("Could not load image!", "Error");
            }

        }
    }
}