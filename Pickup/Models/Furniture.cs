using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class Furniture
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<FurnitureDonationPickup> FurnitureDonationPickups { get; set; } = new List<FurnitureDonationPickup>();
    }
}
