using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.HomeViewModel
{
    public class ViewInformationViewModel
    {
        public DonorCustomer DonorCustomer { get; set; }
        public Address Address { get; set; }
        public PickupOrDelivery PickupOrDelivery { get; set; }
        public string Scheduler { get; set; }

        public List<FurnitureListing> Furniture { get; set; }

        public ViewInformationViewModel()
        {
            List<FurnitureListing> Furniture = new List<FurnitureListing>();
        }
    }
}
