using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.BlacklistViewModels
{
    public class ViewBlacklistedViewModel
    {
        public DonorCustomer DonorCustomer { get; set; }
        public Address Address { get; set; }

        public string  Reason { get; set; }
    }
}
