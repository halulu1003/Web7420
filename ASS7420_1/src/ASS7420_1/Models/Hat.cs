﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ASS7420_1.Models
{
    public class Hat
    {
        public int HatID { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        [StringLength(10, ErrorMessage = "no longer than 10 characters.")]
        public string HatName { get; set; }
        public double Price { get; set; }
        [StringLength(10, ErrorMessage = "no longer than 10 characters.")]
        public string Description { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Category Category { get; set; }
    }
}
