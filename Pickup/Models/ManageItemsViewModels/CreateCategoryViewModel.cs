﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.ManageItemsViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
    }
}
