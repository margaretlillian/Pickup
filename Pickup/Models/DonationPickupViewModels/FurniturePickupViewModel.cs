using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pickup.Models;

namespace Pickup.Models.DonationPickupViewModels
{
    public class FurniturePickupViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PickupID { get; set; }

        public List<CheckBoxItem> FurnitureList { get; set; }

        public FurniturePickupViewModel() {
            FurnitureList = new List<CheckBoxItem>();
}

    }
}
