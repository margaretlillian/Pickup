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

        public IList<CategoryBlock> FurnitureList { get; set; }
    }

    public class CategoryBlock
    {
        public FurnitureCategory Category { get; set; }
        public List<QuantityList> Furniture { get; set; }
    }

}
