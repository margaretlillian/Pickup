using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class Blacklist
    {
        public int ID { get; set; }
        public string Reason { get; set; }

        public int DonorCustomerID { get; set; }
        public DonorCustomer DonorCustomer { get; set; }

    }
}
