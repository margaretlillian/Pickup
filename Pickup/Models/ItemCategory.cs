using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models
{
    public class ItemCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<ItemDonatedSold> ItemsDonatedSold { get; set; }
    }
}
