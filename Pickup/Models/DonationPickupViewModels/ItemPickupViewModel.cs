using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pickup.Models;

namespace Pickup.Models.DonationPickupViewModels
{
    public class ItemPickupViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PickupID { get; set; }

        public List<QuantityList> FurnitureList { get; set; }

        public ItemPickupViewModel() {
            FurnitureList = new List<QuantityList>();
}

    }

}
