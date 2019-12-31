using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    class ListingIcon
    {
        public int Id { get; }
        public string Picture { get; }

        public ListingIcon(int id, string picture)
        {
            Id = id;
            Picture = picture;
        }
    }
}
