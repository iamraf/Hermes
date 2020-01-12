using Hermes.Model.Models;
using System.Collections.Generic;

namespace Hermes.View.favourites
{
    interface IFavoritesView
    {
        List<Listing> Listings { set; }
    }
}
