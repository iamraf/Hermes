using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View.MyHistory
{
    interface IHistoryPage
    {
        List<Listing> Listings { set; }
    }
}
