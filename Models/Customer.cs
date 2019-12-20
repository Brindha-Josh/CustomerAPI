using System;
using System.Collections.Generic;

namespace CustomerMgmt.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
