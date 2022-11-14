using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relationships.Models
{
    public abstract class Base
    {

    }

    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }

    public class Order : BaseEntity
    {
        public string Number { get; set; }

        public int CustomerId { get; set; } // Shadow Property

        // public int PurchaserId { get; set; }
        public Customer Customer { get; set;  }  // Navigation Property
        public ICollection<OrderDetail> Details { get; set; }

    }

    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderDetail : BaseEntity
    {
        public Product Product { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }

    }

    
}
