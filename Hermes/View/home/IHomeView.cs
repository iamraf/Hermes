using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* IHomeView interface connect view presenter and view classes
*  it gets data from the repositories 
*  and pass them to view which is implementing this interface
*/
namespace Hermes.View.home
{
    interface IHomeView
    {
        List<Listing> PopularListings { set; }
        List<Listing> RecommendedListings { set; }
    }
}
