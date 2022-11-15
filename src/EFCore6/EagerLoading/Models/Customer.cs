using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagerLoading.Models
{
    public class Vehicle : BaseEntity
    {
        public string Model { get; set; }
    }

    public class Employee : BaseEntity
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Vehicle Vehicle { get; set; }
    }

    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public Employee AccountManager { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    public class Order : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public Bill Bill { get; set; }
        public bool Paid { get; set; }
    }

    public class Bill : BaseEntity
    {
        public DateTime DateOfPayment { get; set; }
    }


}
