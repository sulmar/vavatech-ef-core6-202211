using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RawSql.Models
{
    public class TotalAmountCountry
    {
        public string Country { get; set; }

        public decimal TotalAmount { get; set; }
     
        public override string ToString() => $"{Country} {TotalAmount:C2}";
    }

    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }

    public class Country : BaseEntity
    {
        public string Name { get; set; }
    }

    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public Country Country { get; set; }
        public decimal Balance { get; set; }
    }
}
