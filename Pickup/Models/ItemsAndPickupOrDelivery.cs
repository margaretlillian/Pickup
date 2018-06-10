using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class ItemsAndPickupOrDelivery
    {
        public int ItemID { get; set; }
        public ItemDonatedSold ItemsDonatedSold { get; set; }

        public int PickupDeliveryID { get; set; }
        public PickupOrDelivery PickupDelivery { get; set; }

        public int Quantity { get; set; } //for Pickups
        public string Description { get; set; } //for Deliveries
    }
}
