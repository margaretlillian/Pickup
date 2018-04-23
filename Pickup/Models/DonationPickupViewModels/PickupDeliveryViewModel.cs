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
        [DataType(DataType.Date)]
        [Display(Name = "Date of Pickup")]
        public DateTime PickupDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        public DateTime PickupTime { get; set; }

        [Display(Name = "Call en route")]
        public bool CallEnRoute { get; set; }

        [Display(Name = "Special Instructions")]
        public string SpecialInstructions { get; set; }
        

    }
}
