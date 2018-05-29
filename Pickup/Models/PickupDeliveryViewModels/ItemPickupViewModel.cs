﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.PickupDeliveryViewModels
{
    public class ItemPickupViewModel
    {
        [Microsoft.AspNetCore.Mvc.HiddenInput(DisplayValue = false)]
        public int PickupID { get; set; }

        public IList<CategoryBlock> FurnitureList { get; set; }
    }

    public class CategoryBlock
    {
        public FurnitureCategory Category { get; set; }
        public List<ItemQuantityList> Furniture { get; set; }
    }

}
