using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerMgmt.Models
{
    public partial class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
       
        public int Age { get; set; }
       
        public string Address { get; set; }
    }
}
