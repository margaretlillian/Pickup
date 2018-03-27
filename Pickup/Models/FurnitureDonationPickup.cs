using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class FurnitureDonationPickup
    {
        public int FurnitureID { get; set; }
        public Furniture Furniture { get; set; }

        public int DonationPickupID { get; set; }
        public DonationPickup DonationPickup { get; set; }
    }
}
