using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeTracking.Models
{
    public abstract class BaseEntity : Base
    {
        public int Id { get; set; }
    }

    public class Order : BaseEntity
    {       
       public string Number { get; set; }
       public Customer Customer { get; set; }
       public ICollection<OrderDetail> Details { get; set; }
    }

    public class OrderDetail : BaseEntity
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}
