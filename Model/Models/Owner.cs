using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    class Owner
    {
        public int ListingId { get; }
        public int UserId { get; }

        public Owner(int listingId, int ownerId)
        {
            ListingId = listingId;
            UserId = ownerId;
        }
    }
}
