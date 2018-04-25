using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Models.DonationPickupViewModels
{
    public class AddressViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Street { get; set; }

        [Display(Name = "Apt.")]
        public string Apartment { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ZIP { get; set; }

        public string Neighborhood {get; set; }

        public AddressViewModel() { }

        public AddressViewModel(int donorId) { }
    }
}
