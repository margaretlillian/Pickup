using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Pickup.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pickup.Models.DonationPickupViewModels
{
    public class PickupDeliveryViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int AddressId { get; set; }

        [Display(Name = "Check here if Delivery")]
        public bool Delivery { get; set; }

        [Required]
        [Display(Name = "Date/Time of Pickup *")]
        public DateTime PickupDateTime { get; set; } = DateTime.Now;

        [Display(Name = "Call en route")]
        public bool CallEnRoute { get; set; }

        [Display(Name = "Special Instructions")]
        public string SpecialInstructions { get; set; }
        

    }
}
