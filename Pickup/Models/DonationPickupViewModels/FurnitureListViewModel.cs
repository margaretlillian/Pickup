using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pickup.Models;

namespace Pickup.Models.DonationPickupViewModels
{
    public class FurnitureListViewModel
    {
        public List<Furniture> FurnitureList { get; set; }
        public int Quantity { get; set; }
    }
}
