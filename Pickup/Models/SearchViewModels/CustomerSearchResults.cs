using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.SearchViewModels
{
    public class CustomerSearchResults
    {
        public DonorCustomer DonorCustomer { get; set; }
        public IList<Address> Addresses { get; set; }
    }
}
