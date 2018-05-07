using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class FurniturePickupOrDelivery
    {
        public int FurnitureID { get; set; }
        public Furniture Furniture { get; set; }

        public int DonationPickupID { get; set; }
        public PickupOrDelivery DonationPickup { get; set; }

        public int Quantity { get; set; } //for Pickups
        public string Description { get; set; } //for Deliveries
    }
}
