using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Hermes.Model.Models
{
    public class Listing
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public bool Active { get; }
        public int Region { get; }
        public int Views { get; }
        public int Category { get; }
        public bool Premium { get; }
        public DateTime Creation { get; }
        public float Price { get; }
        public BitmapImage Image { get; set; }
        public bool Type { get; }
        public string TypeName { get; }


        public Listing(int id, string name, string description, bool active, int region, int views, int category, bool premium, DateTime creation, float price)
        {
            Id = id;
            Name = name;
            Description = description;
            Active = active;
            Region = region;
            Views = views;
            Category = category;
            Premium = premium;
            Creation = creation;
            Price = price;
        }

        public Listing(int id, string name, string description, bool active, int region, int views, int category, bool premium, DateTime creation, float price, bool type):
            this(id, name, description, active, region, views, category, premium, creation, price)
        {
            Type = type;
            if (type)
            {
                TypeName = "Looking for";
            }
            else
            {
                TypeName = "Buying";
            }
        }
    }
}
