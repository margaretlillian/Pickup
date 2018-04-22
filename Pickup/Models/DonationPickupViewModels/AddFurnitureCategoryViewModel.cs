using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.DonationPickupViewModels
{
    public class AddFurnitureCategoryViewModel
    {
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
