using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    class History
    {
        public int ListingId { get; }
        public int UserId { get; }

        public History(int listingId, int userId)
        {
            ListingId = listingId;
            UserId = userId;
        }
    }
}
