using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class DonorCustomer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberTwo { get; set; }

        public IList<DonorCustomer> DonorsCustomers { get; set; }
    }
}
