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
        public List<Category> Categories { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PickupID { get; set; }
        
        public FurniturePickupViewModel(IEnumerable<FurnitureCategory> categories, IEnumerable<Furniture> furniture)
        {
            Categories = categories.Select(category =>
            new Category {
            Name = category.Name,
            ID = category.ID,
            FurnitureLists = furniture.Select(item => 
            new FurnitureList {
                ID = item.ID,
                Name = item.Name,
                CategoryID = category.ID
            }).ToList()}).ToList();

            
                  
        }
        public FurniturePickupViewModel() { }

    }

    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<FurnitureList> FurnitureLists { get; set; }
    }

    public class FurnitureList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }

    }
}
