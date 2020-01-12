using System.Collections.Generic;
using Hermes.Model.Models;

namespace Hermes.View.mylistings
{
    interface IMyListingsView
    {
        List<Listing> Listings { set; }
    }
}
