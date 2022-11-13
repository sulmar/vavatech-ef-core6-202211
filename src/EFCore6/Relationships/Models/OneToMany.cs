using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relationships.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public ICollection<OrderDetail> Details { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderDetail
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }

    }

    
}
