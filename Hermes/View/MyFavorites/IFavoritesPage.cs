using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View.MyFavorites
{
    interface IFavoritesPage
    {

        List<Listing> Listings { set; }

    }
}
