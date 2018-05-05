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

    public List<SelectListItem> Furniture { get; set; }
        public string FurnitureName { get; set; }


        public FurniturePickupViewModel(IEnumerable<Furniture> furniture)
        {
            Furniture = furniture.Select(item =>
            new SelectListItem {
                Text = item.Name,
                Value = item.ID.ToString()

                
            }).ToList();

            
                  
        }
        public FurniturePickupViewModel() { }

    }

    
    public class FurnitureList
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public bool IsSeleced { get; set; }

    }
}
