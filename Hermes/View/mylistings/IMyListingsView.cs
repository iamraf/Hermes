using System.Collections.Generic;
using Hermes.Model.Models;

/* IMyListingsView interface connect view presenter and view classes
*  it gets data from the repositories 
*  and pass them to view which is implementing this interface
*/
namespace Hermes.View.mylistings
{
    interface IMyListingsView
    {
        List<Listing> Listings { set; }
    }
}
