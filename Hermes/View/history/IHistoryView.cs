using Hermes.Model.Models;
using System.Collections.Generic;

namespace Hermes.View.history
{
    interface IHistoryView
    {
        List<Listing> Listings { set; }
    }
}
