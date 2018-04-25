using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.DonationPickupViewModels
{
    public class AddFurnitureViewModel
    {
        [Required]
        [Display(Name = "Furniture Name")]
        public string Name { get; set; }
       
        [Required]
        [Display(Name = "Category")]
        public int FurnitureCategoryID { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public AddFurnitureViewModel(IEnumerable<FurnitureCategory> categories)
        {
            Categories = categories.Select(category =>
                            new SelectListItem
                            {
                                Value = category.ID.ToString(),
                                Text = category.Name
                            }
                        ).ToList();

        }
        public AddFurnitureViewModel()
        {
        }
    }
}
