using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    class Location
    {
        public int Id { get; }
        public string Name { get; }
        public int Tk { get; }

        public Location(int id, string name, int tk)
        {
            Id = id;
            Name = name;
            Tk = tk;
        }
    }
}
