using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    class Favourite
    {
        public int ListingId { get; }
        public int UserId { get; }

        public Favourite(int listingId, int userId)
        {
            ListingId = listingId;
            UserId = userId;
        }
    }
}
