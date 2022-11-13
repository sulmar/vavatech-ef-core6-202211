﻿using System;
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
        // public string Description { get; set; }
        // public string Color { get; set; }

        public Product(string name, decimal price)
        {            
            Name = name;
            Price = price;
        }
    }
}
