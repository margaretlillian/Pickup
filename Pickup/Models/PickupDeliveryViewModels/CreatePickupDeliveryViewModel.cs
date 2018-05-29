using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.PickupDeliveryViewModels
{
    public class CreatePickupDeliveryViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int PickupId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int AddressId { get; set; }

        [Display(Name = "Check here if Delivery")]
        public bool Delivery { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Pickup Date")]
        public DateTime PickupDate { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Pickup Time")]
        public DateTime PickupTime { get; set; }

        [Display(Name = "Call en route")]
        public bool CallEnRoute { get; set; }

        [Display(Name = "Special Instructions")]
        public string SpecialInstructions { get; set; }
    }
}
