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
        public List<QuantityListItem> FurnitureList { get; set; }
        public List<FurnitureCategory> Categories { get; set; }

        public int FurnitureID { get; set; }
        public string FurnitureName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PickupID { get; set; }
        
        public int Quantity { get; set; }

        public FurniturePickupViewModel(IEnumerable<FurnitureCategory> categories, IEnumerable<Furniture> furniture)
        {
            Categories = categories.Select(category =>
            new FurnitureCategory {
            Name = category.Name,
            ID = category.ID}).ToList();

                FurnitureList = furniture.Select(item =>
                     new QuantityListItem
                     {
                         ID = item.ID,
                         Name = item.Name
                     }
                 ).ToList();

                  
        }
        public FurniturePickupViewModel() { }

    }


    public class QuantityListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
