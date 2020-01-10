using Hermes.Model;
using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View.Upload
{
    class UploadPresenter
    {
        private readonly UploadListingRepository _repository;
        private readonly IUploadPage _view;
        private List<Category> _categories;
        private List<SubCategory> _subCategories;
        private List<Location> _locations;
        private readonly ObjectCache Cache;

        public UploadPresenter(IUploadPage view)
        {
            _view = view;
            _repository = new UploadListingRepository();
            Cache = MemoryCache.Default;
            User user = (User)Cache["User"];
            GetCategories();
            GetLocations();           

        }

        public void GetCategories()
        {
            List<Category> list = _repository.GetCategories();
            if(list!=null && list.Count > 0)
            {
                _categories = list;
            }
        }

        public void GetLocations()
        {
            List<Location> list = _repository.GetLocations();
            if (list != null && list.Count > 0)
            {
                _locations = list;
            }
        }

        public List<string> GetCategoryNames()
        {
            List<string> categoryNames = new List<string>();
            foreach (Category cat in _categories)
            {
                categoryNames.Add(cat.Name);
            }
            return categoryNames;
        }

        public List<string> GetLocationNames()
        {
            List<string> locationNames = _repository.GetLocationDistinctNames();
            return locationNames;
        }

        public List<string> GetSubCategoryNames(int categoryIndex)
        {
            Category catSelected = (Category)_categories.ElementAt(categoryIndex);
            _subCategories = _repository.GetSubCategories(catSelected.Id);

            List<string> subCatNames = new List<string>();
            foreach (SubCategory subCat in _subCategories)
            {
                subCatNames.Add(subCat.Name);
            }

            return subCatNames;
        }

        public List<int> GetLocationTK(string locationNameSelected)
        {
            string locNameSelected = locationNameSelected;
            List<int> locSelectedTK = new List<int>();

            foreach (Location loc in _locations)
            {
                if (loc.Name == locNameSelected)
                {
                    locSelectedTK.Add(loc.Tk);
                }
            }

            return locSelectedTK;
        }

        public Location GetSelectedLocation(int selectedTK)
        {
            int TK = selectedTK;
            foreach (Location loc in _locations)
            {
                if (TK == loc.Tk)
                    return loc;
            }
            return null;
        }

        public SubCategory GetSelectedSubCategory(string categoryName)
        {
            string SC = categoryName;
            foreach (SubCategory subCat in _subCategories)
            {
                if (SC.Equals(subCat.Name))
                    return subCat;
            }
            return null;
        }

        public bool UploadListing(string name, float price, int location, string description, int subcategory, bool type)
        {
            int listingId = _repository.UploadListing(name, description, location, subcategory, false, price, type);

            if (listingId != -1)
            {
                User user = (User)Cache["User"];
                _repository.UpdateOwners(listingId, user.Id);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public Location GetMyHomeLocation()
        {
            User user = (User)Cache["User"];
            foreach(Location loc in _locations)
            {
                if (loc.Tk == int.Parse(user.Address))
                {
                    return loc;
                }
            }
            return null;
        }

    }


}
