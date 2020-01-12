using Hermes.Model.Models;
using System.Collections.Generic;
/* IFavoritesView interface connect view presenter and view classes
*  it gets data from the repositories 
*  and pass them to view which is implementing this interface
*/
namespace Hermes.View.favourites
{
    interface IFavoritesView
    {
        List<Listing> Listings { set; }
        Listing SelectedListing { get; }
        bool DeleteButtonEnable { set; }
        bool DeleteAllButtonEnable { set; }
    }
}
