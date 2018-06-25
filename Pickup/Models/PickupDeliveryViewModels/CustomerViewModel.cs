using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.PickupDeliveryViewModels
{
    public class CustomerViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CustomerId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PickupID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Second Phone Number")]
        public string PhoneNumberTwo { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "FOT?")]
        public bool FOT { get; set; }

    }
}
