using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    public class Category
    {
        public int Id { get; }
        public string Name { get; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
