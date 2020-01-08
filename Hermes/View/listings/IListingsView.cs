using Hermes.Model.Models;
using System.Collections.Generic;

namespace Hermes.View.listings
{
    interface IListingsView
    {
        List<Listing> Listings { set; }
        bool Navigate { set; }
    }
}
