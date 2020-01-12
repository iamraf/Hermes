using Hermes.Model.Models;
using System.Collections.Generic;
/* IHistoryView interface connect view presenter and view classes
*  it gets data from the repositories 
*  and pass them to view which is implementing this interface
*/
namespace Hermes.View.history
{
    interface IHistoryView
    {
        List<Listing> Listings { set; }
        Listing SelectedListing { get; }
        bool DeleteButtonEnable { set; }
    }
}
