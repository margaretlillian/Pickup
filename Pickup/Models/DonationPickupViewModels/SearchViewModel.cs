using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.DonationPickupViewModels
{
    public class SearchViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public IList<Donor> Donors { get; set; }
        public IList<Address> Addresses { get; set; }

    }
}
