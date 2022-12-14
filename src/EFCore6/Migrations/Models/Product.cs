using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public Size? Size { get; set; }
        public string BarCode { get; set; }

        public override string ToString() => $"{Id} {Name} {Price} {Description}";

        public Product(string name, decimal price)
        {            
            Name = name;
            Price = price;
        }
    }

    public enum Size
    {
        S,
        M,
        L,
        XL
    }
}
