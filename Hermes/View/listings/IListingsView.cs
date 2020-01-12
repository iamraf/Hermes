using Hermes.Model.Models;
using System.Collections.Generic;

/* IListingsView interface connect view presenter and view classes
*  it gets data from the repositories 
*  and pass them to view which is implementing this interface
*/
namespace Hermes.View.listings
{
    interface IListingsView
    {
        List<Listing> Listings { set; }
        bool Navigate { set; }
    }
}
