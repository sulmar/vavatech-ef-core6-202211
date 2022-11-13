using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Models
{
    public abstract class Item : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        protected Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    public class Product : Item
    {
        public Product(string name, decimal price, string color, string size, float weigth) : base(name, price)
        {
            Color = color;
            Size = size;
            Weigth = weigth;
        }

        public string Color { get; set; }
        public string Size { get; set; }
        public float Weigth { get; }
        public float Weight { get; set; }


    }

    public class Service : Item
    {
        public Service(string name, decimal price, TimeSpan duration) : base(name, price)
        {
            Duration = duration;
        }

        public TimeSpan Duration { get; set; }
    }
}
