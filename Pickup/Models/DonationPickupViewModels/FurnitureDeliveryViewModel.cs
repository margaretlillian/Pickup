using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.DonationPickupViewModels
{
    public class FurnitureDeliveryViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int DeliveryID { get; set; }

        public List<DescriptionList> FurnitureDelivered { get; set; }

        public FurnitureDeliveryViewModel()
        {
            FurnitureDelivered = new List<DescriptionList>();
        }
    }
}
