using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class DonationPickup
    {
        public int ID { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public DateTime PickupDateTime { get; set; }
        public bool CallEnRoute { get; set; }
        public string SpecialInstructions { get; set; }

        public int AddressID { get; set; }
        public Address Address { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public IList<FurnitureDonationPickup> FurnitureDonationPickups { get; set; }
    }
}
