using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hermes.Model.Models;
using Hermes.Model.repository;

 /* buyPremiumPresenter connect view with model classes
 *  it gets data from the repositories 
 *  and pass them to view
 */
namespace Hermes.View.buyPremium
{
    class buyPremiumPresenter
    {
        private readonly IbuyPremiumWindow _view;

        private readonly PremiumListingBuyRepository _repository;

        public buyPremiumPresenter(IbuyPremiumWindow view) 
        {
            _view = view;
            _repository = new PremiumListingBuyRepository();
        }
        public void addPremiumListings(int i) 
        { 
            i =(i+1)* 10;
            User user = GetCurrentUser();
            if(user!=null)
                _repository.addPremiumListings(i,user.Id);
            else
                MessageBox.Show("Unexpected error\n you are not logged in!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private User GetCurrentUser()
        {
            return (User)MemoryCache.Default["User"];
        }
    }
}
