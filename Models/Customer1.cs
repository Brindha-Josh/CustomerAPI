using System;
using System.Collections.Generic;

namespace CUSTOMERAPISQL.Models
{
    public partial class Customer1
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string emailAdd { get; set; }
    }
}
