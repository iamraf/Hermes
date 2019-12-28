using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    class SubCategory
    {
        public int Id { get; }
        public int CategoryId { get; }
        public string Name { get; }

        public SubCategory(int id, int categoryId, string name)
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
        }
    }
}
