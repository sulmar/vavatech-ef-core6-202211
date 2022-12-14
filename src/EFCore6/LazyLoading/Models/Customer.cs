using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyLoading.Models
{
    public class Employee : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public virtual Employee AccountManager { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

    public class Order : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public virtual Bill Bill { get; set; }
        public bool Paid { get; set; }
    }

    public class Bill : BaseEntity
    {
        public DateTime DateOfPayment { get; set; }
    }


}
