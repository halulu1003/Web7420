using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS7420_1.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Home { get; set; }
        public string Work { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
