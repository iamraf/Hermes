using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Model.Models;

namespace Hermes.View
{
    interface IMyListingsPage
    {
        List<Listing> Listings { set; }
    }
}
