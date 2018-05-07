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

        public int FurnitureCategoryID { get; set; }
        public  FurnitureCategory FurnitureCategory { get; set; }

        public IList<FurniturePickupOrDelivery> FurnitureDonationPickups { get; set; } = new List<FurniturePickupOrDelivery>();
    }
}
