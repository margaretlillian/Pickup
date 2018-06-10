using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class ItemDonatedSold
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int ItemCategoryID { get; set; }
        public  ItemCategory ItemCategory { get; set; }

        public IList<ItemsAndPickupOrDelivery> ItemsPickupsDeliveries { get; set; } = new List<ItemsAndPickupOrDelivery>();
    }
}
