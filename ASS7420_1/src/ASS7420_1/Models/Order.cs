using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS7420_1.Models
{
    public enum Status {
        untreated,intransit,deliver
    }
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public Status? Status { get; set; }
        public float Subtotal { get; set; }
        public float GST { get; set; }
        public float Grandtotal { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
