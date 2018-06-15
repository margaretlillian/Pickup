using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.ManageItemsViewModels
{
    public class AddItemViewModel
    {
        [Required]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public AddItemViewModel(IEnumerable<ItemCategory> categories)
        {
            Categories = categories.Select(category =>
                            new SelectListItem
                            {
                                Value = category.ID.ToString(),
                                Text = category.Name
                            }
                        ).ToList();

        }
        public AddItemViewModel()
        {
        }


    }
}
