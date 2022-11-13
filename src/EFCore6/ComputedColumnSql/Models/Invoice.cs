using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputedColumnSql.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Tax { get; set; }
        public decimal GrossAmount { get; set; }

        public Invoice(decimal netAmount, decimal tax)
        {
            NetAmount = netAmount;
            Tax = tax;
        }
    }
}
